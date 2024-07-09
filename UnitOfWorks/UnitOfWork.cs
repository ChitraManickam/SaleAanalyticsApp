using SaleAanalyticsApp.DbContexts;
using SaleAanalyticsApp.Interfaces;
using SaleAanalyticsApp.Repositories;

namespace SaleAanalyticsApp.UnitOfWorks;

public class UnitOfWork : IUnitOfWork, IDisposable
{
    private readonly AppDbContext _dbContext;
    private ISaleRecordRepository _saleRecordRepository;

    public UnitOfWork(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public ISaleRecordRepository SaleRecords
    {
        get
        {
            if (_saleRecordRepository == null)
            {
                _saleRecordRepository = new SaleRecordRepository(_dbContext);
            }
            return _saleRecordRepository;
        }
    }

    public async Task<int> CompleteAsync()
    {
        return await _dbContext.SaveChangesAsync();
    }

    public void Dispose()
    {
        _dbContext.Dispose();
    }
}
