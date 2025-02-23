using Adres.Domain.Models;
using Adres.Domain.Services;
using AdresAPI.DTO;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AdresAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdquisitionController : ControllerBase
    {
        private readonly ILogger<AdquisitionController> _logger; 
        private readonly IAdquisitionService _adquisitionService;
        private readonly IMapper _mapper;

        public AdquisitionController(ILogger<AdquisitionController> logger, IAdquisitionService asquisitionService, IMapper mapper) 
        { 
            _logger = logger;
            _adquisitionService = asquisitionService;  
            _mapper = mapper;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllAdquisition() 
        {
            try
            {
                var adquisitions = await _adquisitionService.GetAllAdquisitions();
                return Ok(_mapper.Map<List<AdquisitionResponseDTO>>(adquisitions));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching all Adquisitions.");
                return StatusCode(500, new { Message = "An unexpected error occurred. Please try again later." });
            }
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetAdquisitionByID(int id) 
        {
            try
            {
                var adquisition = await _adquisitionService.GetByIDAsync(id);

                if (adquisition == null)
                {
                    return NotFound(new { Message = "Adquisition not found." });
                }

                return Ok(_mapper.Map<AdquisitionResponseDTO>(adquisition));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching the Adquisition with ID {Id}", id);
                return StatusCode(500, new { Message = "An unexpected error occurred. Please try again later." });
            }

        }


        [HttpPost("Add")]
        public async Task<IActionResult> CreateAdquisition([FromBody] AdquisitionInputDTO adquisitionDTO) 
        {
            if (adquisitionDTO == null)
            {
                return BadRequest(new { Message = "Invalid adquisition data." });
            }

            try
            {
                var adquisition = _mapper.Map<Adquisition>(adquisitionDTO);
                adquisition.AdquisitionDate = DateTime.UtcNow;
                var createdOrder = await _adquisitionService.CreateAdquisition(adquisition);

                return Created(string.Empty, _mapper.Map<AdquisitionResponseDTO>(createdOrder));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating an adquisition.");
                return StatusCode(500, new { Message = "An unexpected error occurred. Please try again later." });
            }
        }

        [HttpPut("Update")]
        public async Task<IActionResult> UpdateOrder([FromBody] AdquisitionUpdateDTO adquisitionDto)
        {
            if (adquisitionDto == null)
            {
                return BadRequest(new { Message = "Invalid adquisition data." });
            }

            try
            {
                var existingOrder = await _adquisitionService.GetByIDAsync(adquisitionDto.AdquisitionID);
                if (existingOrder == null)
                {
                    return NotFound(new { Message = "Adquisition not found." });
                }

                _mapper.Map(adquisitionDto, existingOrder);
                var updatedOrder = await _adquisitionService.UpdateAdquisition(existingOrder);

                return Ok(_mapper.Map<AdquisitionResponseDTO>(updatedOrder));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating the adquisition with ID {Id}", adquisitionDto.AdquisitionID);
                return StatusCode(500, new { Message = "An unexpected error occurred. Please try again later." });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAdquisition(int id)
        {
            try
            {
                var existingAdquisition = await _adquisitionService.GetByIDAsync(id);
                if (existingAdquisition == null)
                {
                    return NotFound(new { Message = "Adquisition not found." });
                }

                var deletedAdquisition = await _adquisitionService.DeleteAdquisition(id);

                return Ok(_mapper.Map<AdquisitionResponseDTO>(deletedAdquisition));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting the adquisition with ID {Id}", id);
                return StatusCode(500, new { Message = "An unexpected error occurred. Please try again later." });
            }
        }


    }
}
