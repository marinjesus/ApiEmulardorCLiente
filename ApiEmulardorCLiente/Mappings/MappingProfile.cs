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
        // TransicionDto -> Transicion
        CreateMap<TransicionDto, Transicion>();
        // Transicion -> TransicionDto
        CreateMap<Transicion , TransicionDto>();

    }
}
