using Cinema.Application.Abstractions;
using Microsoft.EntityFrameworkCore.Storage;
using Cinema.Infrastructure.Exceptions;
using Microsoft.EntityFrameworkCore;
using Cinema.Application.Exceptions;

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
        try
        {
            await _context.SaveChangesAsync(token);
            await _transaction!.CommitAsync();
        }
        catch (DbUpdateConcurrencyException ex)
        {
            throw new ApplicationPersistenceException(ex.Message);
        }
        catch (DbUpdateException ex)
        {
            throw new ApplicationPersistenceException(ex.Message);
        }
    }

    public async Task RollbackAsync(CancellationToken token)
    {
        if (_transaction is not null)
        {
            await _transaction.RollbackAsync(token);
        }
    }

    public async Task SaveChangesAsync(CancellationToken token)
    {
        try
        {
            await _context.SaveChangesAsync(token);
        }
        catch (DbUpdateConcurrencyException ex)
        {
            throw new ApplicationPersistenceException(ex.Message);
        }
        catch (DbUpdateException ex)
        {
            throw new ApplicationPersistenceException(ex.Message);
        }
    }
}
