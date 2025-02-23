using Adres.Domain.Models;
using Adres.Domain.Services;
using AdresAPI.DTO;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AdresAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AssetServiceTypeController : ControllerBase
    {
        private readonly ILogger<AssetServiceTypeController> _logger;
        private readonly IAssetServiceTypeService _assetServiceTypeService;
        private readonly IMapper _mapper;

        public AssetServiceTypeController(ILogger<AssetServiceTypeController> logger, IAssetServiceTypeService assetServiceTypeService, IMapper mapper)
        {
            _logger = logger;
            _assetServiceTypeService = assetServiceTypeService;
            _mapper = mapper;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllAssetServiceTypes()
        {
            try
            {
                var assetTypes = await _assetServiceTypeService.GetAllAssetServiceType();
                return Ok(_mapper.Map<List<AssetServiceTypeResponseDTO>>(assetTypes));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching all asset service types.");
                return StatusCode(500, new { Message = "An unexpected error occurred. Please try again later." });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAssetServiceTypeByID(int id)
        {
            try
            {
                var assetType = await _assetServiceTypeService.GetByID(id);
                if (assetType == null)
                {
                    return NotFound(new { Message = "Asset service type not found." });
                }
                return Ok(_mapper.Map<AssetServiceTypeResponseDTO>(assetType));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching the asset service type with ID {Id}", id);
                return StatusCode(500, new { Message = "An unexpected error occurred. Please try again later." });
            }
        }

        [HttpPost("Add")]
        public async Task<IActionResult> CreateAssetServiceType([FromBody] AssetServiceTypeInputDTO assetTypeDTO)
        {
            if (assetTypeDTO == null)
            {
                return BadRequest(new { Message = "Invalid asset service type data." });
            }

            try
            {
                var assetType = _mapper.Map<AssetServiceType>(assetTypeDTO);
                var created = await _assetServiceTypeService.CreateAssetServiceType(assetType);
                return Created(string.Empty, _mapper.Map<AssetServiceTypeResponseDTO>(created));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating an asset service type.");
                return StatusCode(500, new { Message = "An unexpected error occurred. Please try again later." });
            }
        }

        [HttpPut("Update")]
        public async Task<IActionResult> UpdateAssetServiceType([FromBody] AssetServiceTypeResponseDTO assetTypeDTO)
        {
            if (assetTypeDTO == null)
            {
                return BadRequest(new { Message = "Invalid asset service type data." });
            }

            try
            {
                var existingType = await _assetServiceTypeService.GetByID(assetTypeDTO.AssetServiceTypeID);
                if (existingType == null)
                {
                    return NotFound(new { Message = "Asset service type not found." });
                }

                _mapper.Map(assetTypeDTO, existingType);
                var updatedType = await _assetServiceTypeService.UpdateAssetServiceType(existingType);
                return Ok(_mapper.Map<AssetServiceTypeResponseDTO>(updatedType));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating the asset service type with ID {Id}", assetTypeDTO.AssetServiceTypeID);
                return StatusCode(500, new { Message = "An unexpected error occurred. Please try again later." });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAssetServiceType(int id)
        {
            try
            {
                var existingType = await _assetServiceTypeService.GetByID(id);
                if (existingType == null)
                {
                    return NotFound(new { Message = "Asset service type not found." });
                }

                var deletedType = await _assetServiceTypeService.DeleteAssetServiceType(id);
                return Ok(_mapper.Map<AssetServiceTypeResponseDTO>(deletedType));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting the asset service type with ID {Id}", id);
                return StatusCode(500, new { Message = "An unexpected error occurred. Please try again later." });
            }
        }
    }
}
