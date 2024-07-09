using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SaleAanalyticsApp.Interfaces;
using SaleAanalyticsApp.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NLog;
using Microsoft.AspNetCore.Authorization;

namespace SaleAanalyticsApp.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AnalyticsController : ControllerBase
    {
        private readonly ISaleRecordRepository _saleRecordRepository;
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

        public AnalyticsController(ISaleRecordRepository recordRepository)
        {
            _saleRecordRepository = recordRepository;
        }

        [HttpGet("Total-Sales")]
        public async Task<ActionResult<decimal>> GetTotalSales([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            try
            {
                decimal totalSales = await _saleRecordRepository.GetTotalSales(startDate, endDate);
                _logger.Info($"Retrieved total sales for period {startDate} to {endDate}: {totalSales}");
                return Ok(totalSales);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Error retrieving total sales for period {startDate} to {endDate}");
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error retrieving total sales: {ex.Message}");
            }
        }

        [HttpGet("Sales-Trends")]
        public async Task<ActionResult<IEnumerable<SalesTrend>>> GetSalesTrends()
        {
            try
            {
                var salesTrends = await _saleRecordRepository.GetSalesTrends();
                _logger.Info("Retrieved sales trends");
                return Ok(salesTrends);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error retrieving sales trends");
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error retrieving sales trends: {ex.Message}");
            }
        }

        [HttpGet("Top-Products")]
        public async Task<ActionResult<IEnumerable<TopProduct>>> GetTopProducts([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            try
            {
                var topProducts = await _saleRecordRepository.GetTopProducts(startDate, endDate);
                _logger.Info($"Retrieved top products for period {startDate} to {endDate}");
                return Ok(topProducts);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Error retrieving top products for period {startDate} to {endDate}");
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error retrieving top products: {ex.Message}");
            }
        }

        [HttpGet("Sales-By-Region")]
        public async Task<ActionResult<IEnumerable<SalesByRegion>>> GetSalesByRegion()
        {
            try
            {
                var salesByRegion = await _saleRecordRepository.GetSalesByRegion();
                _logger.Info("Retrieved sales by region");
                return Ok(salesByRegion);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error retrieving sales by region");
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error retrieving sales by region: {ex.Message}");
            }
        }
    }
}
