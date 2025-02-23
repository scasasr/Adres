using Adres.Domain.Models;
using Adres.Domain.Services;
using AdresAPI.DTO;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AdresAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProviderController : ControllerBase
    {
        private readonly ILogger<ProviderController> _logger;
        private readonly IProviderService _providerService;
        private readonly IMapper _mapper;

        public ProviderController(ILogger<ProviderController> logger, IProviderService providerService, IMapper mapper)
        {
            _logger = logger;
            _providerService = providerService;
            _mapper = mapper;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllProviders()
        {
            try
            {
                var providers = await _providerService.GetAllProviders();
                return Ok(_mapper.Map<List<ProviderResponseDTO>>(providers));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching all providers.");
                return StatusCode(500, new { Message = "An unexpected error occurred. Please try again later." });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProviderByID(int id)
        {
            try
            {
                var provider = await _providerService.GetByID(id);
                if (provider == null)
                {
                    return NotFound(new { Message = "Provider not found." });
                }
                return Ok(_mapper.Map<ProviderResponseDTO>(provider));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching the provider with ID {Id}", id);
                return StatusCode(500, new { Message = "An unexpected error occurred. Please try again later." });
            }
        }

        [HttpPost("Add")]
        public async Task<IActionResult> CreateProvider([FromBody] ProviderInputDTO providerDTO)
        {
            if (providerDTO == null)
            {
                return BadRequest(new { Message = "Invalid provider data." });
            }

            try
            {
                var provider = _mapper.Map<Provider>(providerDTO);
                var created = await _providerService.CreateProvider(provider);
                return Created(string.Empty,_mapper.Map<ProviderResponseDTO>(created));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating a provider.");
                return StatusCode(500, new { Message = "An unexpected error occurred. Please try again later." });
            }
        }

        [HttpPut("Update")]
        public async Task<IActionResult> UpdateProvider([FromBody] ProviderResponseDTO providerDTO)
        {
            if (providerDTO == null)
            {
                return BadRequest(new { Message = "Invalid provider data." });
            }

            try
            {
                var existingProvider = await _providerService.GetByID(providerDTO.ProviderID);
                if (existingProvider == null)
                {
                    return NotFound(new { Message = "Provider not found." });
                }

                _mapper.Map(providerDTO, existingProvider);
                var updatedProvider = await _providerService.UpdateProvider(existingProvider);
                return Ok(_mapper.Map<ProviderResponseDTO>(updatedProvider));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating the provider with ID {Id}", providerDTO.ProviderID);
                return StatusCode(500, new { Message = "An unexpected error occurred. Please try again later." });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProvider(int id)
        {
            try
            {
                var existingProvider = await _providerService.GetByID(id);
                if (existingProvider == null)
                {
                    return NotFound(new { Message = "Provider not found." });
                }

                var deletedProvider = await _providerService.DeleteProvider(id);
                return Ok(_mapper.Map<ProviderResponseDTO>(deletedProvider));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting the provider with ID {Id}", id);
                return StatusCode(500, new { Message = "An unexpected error occurred. Please try again later." });
            }
        }
    }
}
