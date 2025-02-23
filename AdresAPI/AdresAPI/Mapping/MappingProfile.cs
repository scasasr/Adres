using Adres.Domain.Models;
using AdresAPI.DTO;
using AutoMapper;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AdresAPI.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Adquisition, AdquisitionResponseDTO>();

            CreateMap<AdquisitionUpdateDTO,Adquisition>();

            CreateMap<AdquisitionInputDTO, Adquisition>()
                .ForMember(dest => dest.AdquisitionDate, opt => opt.Ignore());

            CreateMap<Provider, ProviderResponseDTO>();

            CreateMap<ProviderResponseDTO, Provider>();

            CreateMap<ProviderInputDTO, Provider>();

            CreateMap<AdminUnit, AdminUnitResponseDTO>();

            CreateMap<AdminUnitResponseDTO, AdminUnit>();

            CreateMap<AdminUnitInputDTO, AdminUnit>();

            CreateMap<AssetServiceType, AssetServiceTypeResponseDTO>();

            CreateMap<AssetServiceTypeResponseDTO, AssetServiceType>();

            CreateMap<AssetServiceTypeInputDTO, AssetServiceType>();

            CreateMap<AdquisitionHistory, AdquisitionHistoryResponseDTO>();

            CreateMap<AdquisitionHistoryUpdateDTO, AdquisitionHistory>();

            CreateMap<AdquisitionHistoryInputDTO, AdquisitionHistory>()
                .ForMember(dest => dest.TimeStamp, opt => opt.Ignore());

        }

    }
}



