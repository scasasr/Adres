using Adres.Domain.Models;
using Adres.Domain.Services;
using AdresAPI.DTO;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AdresAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdquisitionHistoryController : ControllerBase
    {
        private readonly ILogger<AdquisitionHistoryController> _logger;
        private readonly IAdquisitionHistoryService _adquisitionHistoryService;
        private readonly IMapper _mapper;

        public AdquisitionHistoryController(ILogger<AdquisitionHistoryController> logger, IAdquisitionHistoryService adquisitionHistoryService, IMapper mapper)
        {
            _logger = logger;
            _adquisitionHistoryService = adquisitionHistoryService;
            _mapper = mapper;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllAdquisitionHistories()
        {
            try
            {
                var histories = await _adquisitionHistoryService.GetAllAdquisitionHistories();
                return Ok(_mapper.Map<List<AdquisitionHistoryResponseDTO>>(histories));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching all adquisition histories.");
                return StatusCode(500, new { Message = "An unexpected error occurred. Please try again later." });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAdquisitionHistoryByID(int id)
        {
            try
            {
                var history = await _adquisitionHistoryService.GetByID(id);
                if (history == null)
                {
                    return NotFound(new { Message = "Adquisition history not found." });
                }
                return Ok(_mapper.Map<AdquisitionHistoryResponseDTO>(history));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching the adquisition history with ID {Id}", id);
                return StatusCode(500, new { Message = "An unexpected error occurred. Please try again later." });
            }
        }

        [HttpPost("Add")]
        public async Task<IActionResult> CreateAdquisitionHistory([FromBody] AdquisitionHistoryInputDTO historyDTO)
        {
            if (historyDTO == null)
            {
                return BadRequest(new { Message = "Invalid adquisition history data." });
            }

            try
            {
                var history = _mapper.Map<AdquisitionHistory>(historyDTO);
                history.TimeStamp = DateTime.Now;   
                var created = await _adquisitionHistoryService.CreateAdquisitionHistory(history);
                return Created(string.Empty, _mapper.Map<AdquisitionHistoryResponseDTO>(created));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating an adquisition history.");
                return StatusCode(500, new { Message = "An unexpected error occurred. Please try again later." });
            }
        }

        [HttpPut("Update")]
        public async Task<IActionResult> UpdateAdquisitionHistory([FromBody] AdquisitionHistoryUpdateDTO historyDTO)
        {
            if (historyDTO == null)
            {
                return BadRequest(new { Message = "Invalid adquisition history data." });
            }

            try
            {
                var existingHistory = await _adquisitionHistoryService.GetByID(historyDTO.AdquisitionHistoryID);
                if (existingHistory == null)
                {
                    return NotFound(new { Message = "Adquisition history not found." });
                }

                _mapper.Map(historyDTO, existingHistory);
                var updatedHistory = await _adquisitionHistoryService.UpdateAdquisitionHistory(existingHistory);
                return Ok(_mapper.Map<AdquisitionHistoryResponseDTO>(updatedHistory));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating the adquisition history with ID {Id}", historyDTO.AdquisitionHistoryID);
                return StatusCode(500, new { Message = "An unexpected error occurred. Please try again later." });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAdquisitionHistory(int id)
        {
            try
            {
                var existingHistory = await _adquisitionHistoryService.GetByID(id);
                if (existingHistory == null)
                {
                    return NotFound(new { Message = "Adquisition history not found." });
                }

                var deletedHistory = await _adquisitionHistoryService.DeleteAdquisitionHistory(id);
                return Ok(_mapper.Map<AdquisitionHistoryResponseDTO>(deletedHistory));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting the adquisition history with ID {Id}", id);
                return StatusCode(500, new { Message = "An unexpected error occurred. Please try again later." });
            }
        }
    }
}
