using Microsoft.EntityFrameworkCore;
using NLog;
using SaleAanalyticsApp.DbContexts;
using SaleAanalyticsApp.Interfaces;
using SaleAanalyticsApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SaleAanalyticsApp.Repositories;

public class SaleRecordRepository : ISaleRecordRepository
{
    private static readonly Logger logger = LogManager.GetCurrentClassLogger();
    private readonly AppDbContext _dbContext;

    public SaleRecordRepository(AppDbContext context)
    {
        _dbContext = context;
    }

    public async Task AddAsync(SaleRecord record)
    {
        try
        {
            await _dbContext.SaleRecords.AddAsync(record);
            await _dbContext.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            logger.Error(ex, $"Error in AddAsync: {ex.Message}");
            throw; 
        }
    }

    public async Task UpdateAsync(SaleRecord record)
    {
        try
        {
            _dbContext.Entry(record).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            logger.Error(ex, $"Error in UpdateAsync: {ex.Message}");
            throw;  
        }
    }

    public async Task DeleteAsync(int id)
    {
        try
        {
            var saleRecordId = await _dbContext.SaleRecords.FindAsync(id);

            if (saleRecordId != null)
            {
                _dbContext.SaleRecords.Remove(saleRecordId);
                await _dbContext.SaveChangesAsync();
            }
        }
        catch (Exception ex)
        {
            logger.Error(ex, $"Error in DeleteAsync: {ex.Message}");
            throw;  
        }
    }

    public async Task<IEnumerable<SaleRecord>> GetAllAsync()
    {
        try
        {
            return await _dbContext.SaleRecords.ToListAsync();
        }
        catch (Exception ex)
        {
            logger.Error(ex, $"Error in GetAllAsync: {ex.Message}");
            throw;  
        }
    }

    public async Task<SaleRecord> GetByIdAsync(int id)
    {
        try
        {
            return await _dbContext.SaleRecords.FindAsync(id);
        }
        catch (Exception ex)
        {
            logger.Error(ex, $"Error in GetByIdAsync: {ex.Message}");
            throw; 
        }
    }

    public async Task<decimal> GetTotalSales(DateTime startDate, DateTime endDate)
    {
        try
        {
            decimal totalSales = await _dbContext.SaleRecords
                .Where(s => s.SaleDate >= startDate && s.SaleDate <= endDate)
                .SumAsync(s => s.Price);

            return totalSales;
        }
        catch (Exception ex)
        {
            logger.Error(ex, $"Error in GetTotalSales: {ex.Message}");
            throw; 
        }
    }

    public async Task<IEnumerable<SalesTrend>> GetSalesTrends()
    {
        try
        {
            var query = await _dbContext.SaleRecords
                .GroupBy(s => new
                {
                    Year = s.SaleDate.Year,
                    Month = s.SaleDate.Month,
                    Day = s.SaleDate.Day
                })
                .Select(g => new
                {
                    Year = g.Key.Year,
                    Month = g.Key.Month,
                    Day = g.Key.Day,
                    TotalSales = g.Sum(s => s.Price)
                })
                .ToListAsync();

            var salesTrends = query.Select(g => new SalesTrend
            {
                Date = new DateTime(g.Year, g.Month, g.Day),
                TotalSales = g.TotalSales
            })
            .OrderBy(g => g.Date);

            return salesTrends;
        }
        catch (Exception ex)
        {
            logger.Error(ex, $"Error in GetSalesTrends: {ex.Message}");
            throw;  
        }
    }

    public async Task<IEnumerable<TopProduct>> GetTopProducts(DateTime startDate, DateTime endDate)
    {
        try
        {
            var topProducts = await _dbContext.SaleRecords
                .Where(s => s.SaleDate >= startDate && s.SaleDate <= endDate)
                .GroupBy(s => s.ProductName)
                .Select(g => new TopProduct
                {
                    ProductName = g.Key,
                    SalesCount = g.Count() // Count the number of records for each product
                })
                .OrderByDescending(p => p.SalesCount)
                .Take(10)
                .ToListAsync();

            return topProducts;
        }
        catch (Exception ex)
        {
            // Log the exception using NLog
            logger.Error(ex, $"Error in GetTopProducts: {ex.Message}");
            throw; // Re-throw the exception to propagate it up the call stack
        }
    }

    public async Task<IEnumerable<SalesByRegion>> GetSalesByRegion()
    {
        try
        {
            var query = await _dbContext.SaleRecords
                .GroupBy(s => s.Region)
                .Select(g => new SalesByRegion
                {
                    Region = g.Key,
                    TotalSales = g.Sum(s => s.Price)
                })
                .ToListAsync();

            return query;
        }
        catch (Exception ex)
        {
            logger.Error(ex, $"Error in GetSalesByRegion: {ex.Message}");
            throw;  
        }
    }
}
