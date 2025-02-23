using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adres.Domain.Models
{
    public class AdresDbContext : DbContext
    {
        public AdresDbContext(DbContextOptions<AdresDbContext> options)
        : base(options) { }

        public DbSet<Adquisition> Adquisitions { get; set; }
        public DbSet<AdminUnit> AdminUnits { get; set; }
        public DbSet<AssetServiceType> AssetServiceTypes { get; set; }
        public DbSet<Provider> Providers { get; set; }
        public DbSet<AdquisitionHistory> AdquisitionHistories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //primary keys
            modelBuilder.Entity<Adquisition>().HasKey(a => a.AdquisitionID);
            modelBuilder.Entity<AdminUnit>().HasKey(au => au.AdminUnitID);
            modelBuilder.Entity<AssetServiceType>().HasKey(ast => ast.AssetServiceTypeID);
            modelBuilder.Entity<Provider>().HasKey(p => p.ProviderID);
            modelBuilder.Entity<AdquisitionHistory>().HasKey(ah => ah.AdquisitionHistoryID);

            //relationships 
            modelBuilder.Entity<Adquisition>()
                .HasOne(a => a.AdminUnit)
                .WithMany(au => au.Adquisitions)
                .HasForeignKey(a => a.AdminUnitID);

            modelBuilder.Entity<Adquisition>()
                .HasOne(a => a.AssetServiceType)
                .WithMany(ast => ast.Adquisitions)
                .HasForeignKey(a => a.AssetServiceTypeID);

            modelBuilder.Entity<Adquisition>()
                .HasOne(a => a.Provider)
                .WithMany(p => p.Adquisitions)
                .HasForeignKey(a => a.ProviderID);

            modelBuilder.Entity<AdquisitionHistory>()
                .HasOne(ah => ah.Adquisition)
                .WithMany(a => a.Histories)
                .HasForeignKey(ah => ah.AdquisitionID);
        }
    }
}
