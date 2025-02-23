using Adres.Domain.Models;
using Adres.Domain.Services;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AdresDbContext>(options =>
    options.UseSqlite(connectionString)
);


//Add Services
builder.Services.AddScoped<IAdquisitionService, AdquisitionService>();
builder.Services.AddScoped<IAdminUnitService, AdminUnitService>();
builder.Services.AddScoped<IAssetServiceTypeService, AssetServiceTypeService>();
builder.Services.AddScoped<IProviderService, ProviderService>();
builder.Services.AddScoped<IAdquisitionHistoryService, AdquisitionHistoryService>();

// Add AutoMapper
builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var allowSpecificOrigins = "_allowSpecificOrigins";

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: allowSpecificOrigins,
        policy =>
        {
            policy.WithOrigins("http://localhost:4200") 
                  .AllowAnyHeader()
                  .AllowAnyMethod()
                  .AllowCredentials(); 
        });
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AdresDbContext>();

    if (!context.AdminUnits.Any())
    {
        context.AdminUnits.AddRange(
            new AdminUnit { Name = "Secretaría de Salud", ReferenceCode = "SS" },
            new AdminUnit { Name = "Dirección de Medicamentos y Tecnologías en Salud", ReferenceCode = "DMTS" },
            new AdminUnit { Name = "Hospital General", ReferenceCode = "HG" }
        );
        await context.SaveChangesAsync();
    }

    if (!context.Providers.Any())
    {
        context.Providers.AddRange(
            new Provider { Name = "Laboratorios Bayer S.A.", ReferenceCode = "LB" },
            new Provider { Name = "Proveedor B", ReferenceCode = "PB" },
            new Provider { Name = "Proveedor C", ReferenceCode = "PC" }
        );
        await context.SaveChangesAsync();
    }

    if (!context.AssetServiceTypes.Any())
    {
        context.AssetServiceTypes.AddRange(
            new AssetServiceType { Name = "Medicamentos", ReferenceCode = "MED" },
            new AssetServiceType { Name = "Insumos Médicos", ReferenceCode = "INS" },
            new AssetServiceType { Name = "Equipamiento", ReferenceCode = "EQP" }
        );
        await context.SaveChangesAsync();
    }

    if (!context.Adquisitions.Any())
    {
        var adminUnit = context.AdminUnits
            .FirstOrDefault(a => a.Name == "Dirección de Medicamentos y Tecnologías en Salud");
        var assetServiceType = context.AssetServiceTypes
            .FirstOrDefault(a => a.Name == "Medicamentos");
        var provider = context.Providers
            .FirstOrDefault(p => p.Name == "Laboratorios Bayer S.A.");

        if (adminUnit != null && assetServiceType != null && provider != null)
        {
            var newAdquisition = new Adquisition
            {
                AdminUnitID = adminUnit.AdminUnitID,
                AssetServiceTypeID = assetServiceType.AssetServiceTypeID,
                ProviderID = provider.ProviderID,
                Budget = 10000000000m,
                Quantity = 10000,
                UnitPrice = 1000m,
                TotalPrice = 10000000m,
                AdquisitionDate = new DateTime(2023, 7, 20),
                Documentation = "Orden de compra No. 2023-07-20-001, factura No. 2023-07-20-001",
                IsActive = true
            };

            context.Adquisitions.Add(newAdquisition);
            await context.SaveChangesAsync();

            var adquisitionData = new //anonymous object 
            {
                AdminUnitID = adminUnit.AdminUnitID,
                AssetServiceTypeID = assetServiceType.AssetServiceTypeID,
                ProviderID = provider.ProviderID,
                Budget = 10000000000m, 
                Quantity = 10000,      
                UnitPrice = 1000m,     
                TotalPrice = 10000000m,
                AdquisitionDate = new DateTime(2023, 7, 20),
                Documentation = "Orden de compra No. 2023-07-20-001, factura No. 2023-07-20-001",
                IsActive = true
            };

            string adquisitionJson = JsonSerializer.Serialize(adquisitionData);

            var adquisitionHistory = new AdquisitionHistory
            {
                AdquisitionID = newAdquisition.AdquisitionID,
                Operation = "Created",
                TimeStamp = DateTime.Now,
                Model = adquisitionJson
            };

            context.AdquisitionHistories.Add(adquisitionHistory);
            await context.SaveChangesAsync();
        }
    }
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowLocalhost");
app.UseCors(allowSpecificOrigins);

app.UseAuthorization();

app.MapControllers();

app.Run();
