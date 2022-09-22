using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace ApiEmulardorCLiente.Repo
{
    public class Context
    {
        private readonly IMongoDatabase _database = null;

        public Context(IOptions<Core.Model.SettingsMongo> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            if (client != null)
                _database = client.GetDatabase(settings.Value.Database);
        }

        public IMongoCollection<T> Get<T>(String nombreObject)
        {
            if (string.IsNullOrWhiteSpace(nombreObject))
                throw new System.ArgumentException("Nombre del Object es null", nombreObject);

            return _database.GetCollection<T>(nombreObject);
        }

        public IMongoCollection<Core.Entities.Transicion> Maestro
        {
            get
            {
                return _database.GetCollection<Core.Entities.Transicion>("Transicion");
            }
        }

    }
}
