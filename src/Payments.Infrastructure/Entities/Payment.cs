using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Payments.Infrastructure.Enums;

namespace Payments.Infrastructure.Entities;

public class Payment 
{
    public Payment()
    {
        CreatedAt = DateTime.UtcNow;
    }

    public Guid Id { get; set; }

    [BsonElement("value")]
    public decimal Value { get; set; }

    [BsonElement("status")]
    [BsonRepresentation(MongoDB.Bson.BsonType.String)]
    public PaymentStatus Status { get; set; }

    [BsonElement("transactionId")]
    public Guid TransactionId { get; set; }

    [BsonElement("createdAt")]
    public DateTime CreatedAt { get; set; }

}