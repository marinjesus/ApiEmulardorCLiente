using ApiEmulardorCLiente.Controllers.Base;
using ApiEmulardorCLiente.Core.Dtos;
using ApiEmulardorCLiente.Core.Entities;
using ApiEmulardorCLiente.Core.Helps;
using ApiEmulardorCLiente.Services;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

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
            string data = Convert.ToString(ModelDto.data);
            Transicionmapper.Model = data;
            var Colletion = _Service.Add(Transicionmapper);
            TransicionDto resul = _mapper.Map<TransicionDto>(Colletion.Result);
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
            var Colletion = _Service.GetId(Model.IdEmpresa, Model.Suministro);
           var response = Colletion.Result;

            switch (response.error)
            {
                case "200":
                case "201":
                case "202":
                    return Ok(response);
                case "400":
                case "401":
                case "403":
                case "404":
                case "502":
                case "504":
                    return StatusCode(response.error.ToInteger());
                case "500":
                default:
                    return StatusCode(500, "Internal server error");
            }

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
