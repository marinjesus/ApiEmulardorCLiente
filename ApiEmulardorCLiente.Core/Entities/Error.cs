using ApiEmulardorCLiente.Core.Helps;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ApiEmulardorCLiente.Core.Entities;
public class Error
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = string.Empty;

    public string metodo { get; set; } = string.Empty;

    public object mensaje { get; set; } = string.Empty;

    public string usuarioModificacion { get; set; } = string.Empty;

    public string fechaModificacion { get; set; } = string.Empty;
    public Error() => fechaModificacion = PeruDateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
}

