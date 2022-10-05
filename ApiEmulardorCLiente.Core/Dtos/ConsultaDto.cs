
using System.ComponentModel.DataAnnotations;

namespace ApiEmulardorCLiente.Core.Dtos;
public class ConsultaDto
{
    [Required]
    public string IdEmpresa { get; init; }
    [Required]
    public string Suministro { get; init; }
}
