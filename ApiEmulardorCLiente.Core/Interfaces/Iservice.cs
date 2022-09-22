using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiEmulardorCLiente.Core.Interfaces;

public interface Iservice<T> where T : new()
{
    Task<List<T>> GetFull();
    Task<T> GetId(string id, string Sumi);
    Task<T> Add(T model);
    Task<T> Update(string id, T model);
    Task<bool> Remove(string id);
}