using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ApiEmulardorCLiente.Core.Entities;

public class Transicion 
{

    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string IdTransicion { get; set; }
    public string IdEmpresa { get; init; }
    public string Suministro { get; init; }
    public string error { get; init; }

    [Newtonsoft.Json.JsonProperty(Order = 99)]
    public string Model { get; set; }

    public Transicion()
    {
        IdTransicion = ObjectId.GenerateNewId().ToString();
        IdEmpresa = string.Empty;
        Suministro = string.Empty;
        error = string.Empty;
        Model=string.Empty;
    }
}
