using AutoMapper;
using ParkManager.API.DTOs;
using ParkManager.Domain;

namespace ParkManager.API.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Cliente mappings
            CreateMap<Cliente, ClienteDto>()
                .ForMember(dest => dest.Veiculos, opt => opt.MapFrom(src => src.Veiculos));
            CreateMap<ClienteCreateDto, Cliente>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Veiculos, opt => opt.Ignore());
            CreateMap<ClienteUpdateDto, Cliente>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Veiculos, opt => opt.Ignore());

            // Veiculo mappings
            CreateMap<Veiculo, VeiculoDto>()
                .ForMember(dest => dest.Cliente, opt => opt.MapFrom(src => src.Cliente));
            CreateMap<VeiculoCreateDto, Veiculo>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Cliente, opt => opt.Ignore());
            CreateMap<VeiculoUpdateDto, Veiculo>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Cliente, opt => opt.Ignore());

            // Mensalista mappings
            CreateMap<Mensalista, MensalistaDto>()
                .ForMember(dest => dest.Cliente, opt => opt.MapFrom(src => src.Cliente))
                .ForMember(dest => dest.Veiculo, opt => opt.MapFrom(src => src.Veiculo));
            CreateMap<MensalistaCreateDto, Mensalista>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Cliente, opt => opt.Ignore())
                .ForMember(dest => dest.Veiculo, opt => opt.Ignore());
            CreateMap<MensalistaUpdateDto, Mensalista>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Cliente, opt => opt.Ignore())
                .ForMember(dest => dest.Veiculo, opt => opt.Ignore());
        }
    }
}
