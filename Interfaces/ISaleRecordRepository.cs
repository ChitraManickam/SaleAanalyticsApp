using SaleAanalyticsApp.Models;

namespace SaleAanalyticsApp.Interfaces;

public interface ISaleRecordRepository
{
    Task<IEnumerable<SaleRecord>> GetAllAsync();

    Task<SaleRecord> GetByIdAsync(int id);

    Task AddAsync (SaleRecord record);

    Task UpdateAsync (SaleRecord record);

    Task DeleteAsync (int id);

    Task<decimal> GetTotalSales(DateTime startDate, DateTime endDate);

    Task<IEnumerable<SalesTrend>> GetSalesTrends();

    Task<IEnumerable<TopProduct>> GetTopProducts(DateTime startDate, DateTime endDate);

    Task<IEnumerable<SalesByRegion>> GetSalesByRegion();

}
