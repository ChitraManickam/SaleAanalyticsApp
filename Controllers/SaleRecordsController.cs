using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NLog;
using SaleAanalyticsApp.Models;
using SaleAanalyticsApp.UnitOfWorks;

namespace SaleAanalyticsApp.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class SaleRecordsController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;
    private static readonly Logger logger = LogManager.GetCurrentClassLogger();

    public SaleRecordsController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [HttpGet("GetAllSaleRecords")]
    public async Task<IActionResult> GetAllSaleRecords()
    {
        try
        {
            var salesRecords = await _unitOfWork.SaleRecords.GetAllAsync();
            logger.Info("Retrieved all sale records.");
            return Ok(salesRecords);
        }
        catch (Exception ex)
        {
            logger.Error(ex, "Failed to retrieve all sale records.");
            throw; // Rethrow the exception to let the global exception handler manage it
        }
    }

    [HttpGet("GetId")]
    public async Task<IActionResult> GetSaleRecordId(int id)
    {
        try
        {
            var salesRecord = await _unitOfWork.SaleRecords.GetByIdAsync(id);

            if (salesRecord == null)
            {
                logger.Info($"Sale record with ID {id} not found.");
                return NotFound();
            }

            logger.Info($"Retrieved sale record with ID {id}.");
            return Ok(salesRecord);
        }
        catch (Exception ex)
        {
            logger.Error(ex, $"Failed to retrieve sale record with ID {id}.");
            throw;
        }
    }

    [HttpPost("CreateSaleRecord")]
    public async Task<IActionResult> CreateSaleRecord(SaleRecord saleRecord)
    {
        try
        {
            await _unitOfWork.SaleRecords.AddAsync(saleRecord);
            logger.Info($"Created new sale record with ID {saleRecord.Id}.");
            return CreatedAtAction(nameof(GetSaleRecordId), new { id = saleRecord.Id }, saleRecord);
        }
        catch (Exception ex)
        {
            logger.Error(ex, "Failed to create sale record.");
            throw;
        }
    }

    [HttpPut("UpdateSaleRecordById")]
    public async Task<IActionResult> UpdateSaleRecord([FromBody] SaleRecord saleRecord, int id)
    {
        try
        {
            if (id != saleRecord.Id)
                return BadRequest();

            await _unitOfWork.SaleRecords.UpdateAsync(saleRecord);
            logger.Info($"Updated sale record with ID {id}.");
            return Ok();
        }
        catch (Exception ex)
        {
            logger.Error(ex, $"Failed to update sale record with ID {id}.");
            throw;
        }
    }

    [HttpDelete("DeleteSaleRecordId")]
    public async Task<IActionResult> DeleteSaleRecordId([FromBody] int id)
    {
        try
        {
            await _unitOfWork.SaleRecords.DeleteAsync(id);
            logger.Info($"Deleted sale record with ID {id}.");
            return Ok();
        }
        catch (Exception ex)
        {
            logger.Error(ex, $"Failed to delete sale record with ID {id}.");
            throw;
        }
    }
}
