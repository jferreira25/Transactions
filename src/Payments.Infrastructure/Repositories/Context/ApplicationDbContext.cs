using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.InMemory.Infrastructure.Internal;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using Payments.Infrastructure.Entities;
using Payments.Infrastructure.Interfaces;

namespace Payments.Api.DependencyInjection;

public class ApplicationDbContext : DbContext, IBaseRepository<Payment>
{
    private readonly IMongoDatabase _mongoDatabase;
    private readonly bool _isInMemory;
    private readonly string _collectionName = "Payments";

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IConfiguration configuration)
        : base(options)
    {
        _isInMemory = options.Extensions.OfType<InMemoryOptionsExtension>().Any();

        if (!_isInMemory)
        {
            var client = new MongoClient(configuration["ConnectionsString:MongoDB"]);
            _mongoDatabase = client.GetDatabase(configuration["MongoDB:DatabaseName"]);
        }
    }

    public DbSet<Payment> Payments { get; set; }

    public IMongoCollection<Payment> Payment
        => _mongoDatabase.GetCollection<Payment>(_collectionName);

    public async Task AddAsync(Payment payment)
    {
        if (_isInMemory)
        {
            Payments.Local.Add(payment);
            await Payments.AddAsync(payment);
        }
        else
        {
            await Payment.InsertOneAsync(payment);
        }
    }

    public async Task<Payment?> FindOneAsync(Guid transactionId)
    {
        if (_isInMemory)
            return Payments.Local.FirstOrDefault(x => x.TransactionId == transactionId);

        return await Payment.Find(p => p.TransactionId == transactionId).FirstOrDefaultAsync();
    }
}