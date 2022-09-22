using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ApiEmulardorCLiente.Core.Entities;

public class Transicion : Dtos.TransicionDto
{

    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string IdTransicion { get; set; }


    public Transicion()
    {
        IdTransicion = ObjectId.GenerateNewId().ToString();
    }
}
