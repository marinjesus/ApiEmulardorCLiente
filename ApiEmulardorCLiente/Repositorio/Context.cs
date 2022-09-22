using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace ApiEmulardorCLiente.Repositorio;

public class Context
{
    private readonly IMongoDatabase _database;

    public Context(IOptions<Core.Model.SettingsMongo> settings)
    {
        var client = new MongoClient(settings.Value.ConnectionString);
        if (client != null)
            _database = client.GetDatabase(settings.Value.Database);

        if(_database == null)
            throw new System.ArgumentException("IMongoDatabase es null"); ;

    }

    public IMongoCollection<T> Get<T>(String nombreObject)
    {
        if (string.IsNullOrWhiteSpace(nombreObject))
            throw new System.ArgumentException("Nombre del Object es null", nombreObject);

        return _database.GetCollection<T>(nombreObject);
    }

    public IMongoCollection<Core.Entities.Transicion> Transicion
    {
        get
        {
            return _database.GetCollection<Core.Entities.Transicion>("Transicion");
        }
    }

    public IMongoCollection<Core.Entities.Transicion> LogMongo
    {
        get
        {
            return _database.GetCollection<Core.Entities.Transicion>("LogApi");
        }
    }
    public async Task AddErrorAsync(Exception ex)
    {
        try
        {
            var obj = new Core.Entities.Error();
            var s = new StackTrace(ex);
            var thisasm = Assembly.GetExecutingAssembly();
            obj.metodo = s.GetFrames().Select(f => f.GetMethod()).First(m => m.Module.Assembly == thisasm).Name;
            obj.mensaje = ex;
            await Get<Core.Entities.Error>("LogError").InsertOneAsync(obj);
        }
        catch { }
    }
}
