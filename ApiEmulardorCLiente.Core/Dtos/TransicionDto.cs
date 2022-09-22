using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiEmulardorCLiente.Core.Dtos;

public class TransicionDto
{
    public string IdEmpresa { get; init; }
    public string Suministro { get; init; }
    public string error { get; init; }
    public object[] data { get; init; }

    public TransicionDto()
    {
        IdEmpresa = string.Empty;
        Suministro = string.Empty;
        error = string.Empty;
        data = new object[0];
    }
}
