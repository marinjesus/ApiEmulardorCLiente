
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace ApiEmulardorCLiente.Controllers.Base;
public class BaseApiController : ControllerBase
{
    protected readonly IConfiguration _configuration;
    protected readonly IMapper _mapper;

    public BaseApiController(IConfiguration configuration, IMapper mapper)
    {
        _configuration = configuration;
        _mapper = mapper;
    }
}


