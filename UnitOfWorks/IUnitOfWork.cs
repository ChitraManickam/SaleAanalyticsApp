using SaleAanalyticsApp.Interfaces;

namespace SaleAanalyticsApp.UnitOfWorks;

public interface IUnitOfWork
{
    ISaleRecordRepository SaleRecords { get; }
    Task<int> CompleteAsync();
}
