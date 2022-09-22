using ApiEmulardorCLiente.Controllers.Base;
using ApiEmulardorCLiente.Core.Dtos;
using ApiEmulardorCLiente.Core.Entities;
using ApiEmulardorCLiente.Services;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiEmulardorCLiente.Controllers;

[Authorize]
[Route("api/Trans")]
[ApiController]
public class TransController : BaseApiController
{

    protected ServiceTransicion _Service;
    public TransController(ServiceTransicion Service, IConfiguration configuration, IMapper mapper) :
        base(configuration, mapper)
    {
        this._Service = Service;
    }



    [HttpPost("Create")]
    public async Task<IActionResult> crear(TransicionDto ModelDto)
    {
        try
        {
            Transicion Transicionmapper = _mapper.Map<Transicion>(ModelDto);
            var Colletion = _Service.Add(Transicionmapper);
            return Ok(Colletion.Result);
        }
        catch (Exception ex)
        {
            await _Service.ErrorLog(ex);
            throw;
        }
    }


    [HttpPost("Consulta")]
    public async Task<IActionResult> ConsultaAsync(ConsultaDto Model)
    {
        try
        {
            var Colletion = _Service.GetFull();
            return Ok(Colletion.Result);
        }
        catch (Exception ex)
        {
            await _Service.ErrorLog(ex);
            throw;
        }
    }

    [HttpPost("Pago")]
    public async Task<IActionResult> Pago(PagoDto Model)
    {
        try
        {
            return Ok();
        }
        catch (Exception ex)
        {
            await _Service.ErrorLog(ex);
            throw;
        }
    }

    [HttpPost("Cancelar")]
    public async Task<IActionResult> Cancelar(CancelarDto Model)
    {
        try
        {
            return Ok();
        }
        catch (Exception ex)
        {
            await _Service.ErrorLog(ex);
            throw;
        }
    }

}
