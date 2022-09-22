using ApiEmulardorCLiente.Core.Entities;
using ApiEmulardorCLiente.Core.Interfaces;
using Microsoft.Extensions.Options;
using System.Linq;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using System.Linq.Expressions;

namespace ApiEmulardorCLiente.Services;

public class ServiceTransicion : Iservice<Transicion>
{
    private readonly Repositorio.Context _db;
    public ServiceTransicion(IOptions<ApiEmulardorCLiente.Core.Model.SettingsMongo> settings)
    {
        _db = new Repositorio.Context(settings);
    }

    public async Task<Transicion> Add(Transicion model)
    {
        try
        {

            await _db.Transicion.InsertOneAsync(model);
            return model;
        }
        catch (Exception ex)
        {
            await _db.AddErrorAsync(ex);
            return new Transicion();
        }
    }

    public async Task<List<Transicion>> GetFull()
    {
        var Colletion = new List<Transicion>();
        try
        {
            var myObj = await _db.Transicion.Find(_ => true).ToListAsync();
            if (myObj.Count > 0)
                foreach (var item in myObj)
                    Colletion.Add(BsonSerializer.Deserialize<Transicion>(item.ToBsonDocument()));
        }
        catch (Exception ex)
        {
            await _db.AddErrorAsync(ex);
        }
        return Colletion;
    }

    public async Task<Transicion> GetId(string Id, string Sumi)
    {
        var Colletion = new Transicion();
        try
        {
            FilterDefinition<Transicion> filter = Builders<Transicion>.Filter.Empty;
            var builder = Builders<Transicion>.Filter;
            filter = builder.Regex("IdEmpresa", new BsonRegularExpression(Id, "i")) & builder.Regex("Suministro", new BsonRegularExpression(Sumi, "i"));
            var myObj = await _db.Transicion.Find(filter).FirstOrDefaultAsync();
            var bsonObject = myObj.ToBsonDocument();
            Colletion = BsonSerializer.Deserialize<Transicion>(bsonObject);
        }
        catch (Exception ex)
        {
            await _db.AddErrorAsync(ex);
        }
        return Colletion;
    }

    public async Task<Transicion> Update(string Id, Transicion model)
    {
        var Colletion = new Transicion();
        try
        {
            var filter = Builders<Transicion>.Filter.Where(x => x.IdTransicion == Id);
            var updateDefBuilder = Builders<Transicion>.Update;
            var updateDef = updateDefBuilder.Combine(
                new UpdateDefinition<Transicion>[]
                {
                    updateDefBuilder.Set(x => x.Suministro, model.Suministro),
                    updateDefBuilder.Set(x => x.IdEmpresa, model.IdEmpresa)
                });
            Colletion = await _db.Transicion.FindOneAndUpdateAsync(filter, updateDef);
        }
        catch (Exception ex)
        {
            await _db.AddErrorAsync(ex);
        }
        return Colletion;
    }

    public async Task<bool> Remove(string Id)
    {
        var resul = false;
        try
        {
            DeleteResult actionResult = await _db.Transicion.DeleteOneAsync(Builders<Transicion>.Filter.Eq(x => x.IdEmpresa, Id));
            if (actionResult.IsAcknowledged && actionResult.DeletedCount > 0) resul = true;
        }
        catch (Exception ex)
        {
            await _db.AddErrorAsync(ex);
        }
        return resul;
    }

    public async Task ErrorLog(Exception ex)
    {
        await _db.AddErrorAsync(ex);
    }


}
