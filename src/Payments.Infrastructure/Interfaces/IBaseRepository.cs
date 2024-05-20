namespace Payments.Infrastructure.Interfaces;

public interface IBaseRepository<TEntity>
{
    Task AddAsync(TEntity item);

    Task<TEntity?> FindOneAsync(Guid transactionId);
}