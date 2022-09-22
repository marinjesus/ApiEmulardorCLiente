using ApiEmulardorCLiente.Core.Dtos;
using ApiEmulardorCLiente.Core.Entities;
using ApiEmulardorCLiente.Core.Helps;
using AutoMapper;
using Newtonsoft.Json;

namespace ApiEmulardorCLiente.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Transicion, TransicionDto>().ForMember(dest => dest.data, opt => opt.MapFrom(s => s.data.ToJson()));
        CreateMap<TransicionDto, Transicion>().ForMember(dest => dest.data, opt => opt.MapFrom(s => s.data.ToJson()));

    }
}
