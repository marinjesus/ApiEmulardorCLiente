using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiEmulardorCLiente.Core.Dtos;

public class CancelarDto : ConsultaDto
{
    public string? NumeroFactura { get; init; }
}
