using Cinema.Application.Abstractions;
using Microsoft.EntityFrameworkCore.Storage;

namespace Cinema.Infrastructure;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;
    private IDbContextTransaction? _transaction;

    public UnitOfWork(AppDbContext context)
    {
        _context = context;
    }

    public async Task BeginAsync(CancellationToken token)
    {
        _transaction = await _context.Database.BeginTransactionAsync(token);
    }

    public async Task CommitAsync(CancellationToken token)
    {
        await _context.SaveChangesAsync(token);

        await _transaction!.CommitAsync();
    }

    public async Task RollbackAsync(CancellationToken token)
    {
        if (_transaction is not null)
        {
            await _transaction.RollbackAsync(token);
        }
    }
}
