using Adres.Domain.Models;
using Adres.Domain.Services;
using AdresAPI.DTO;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AdresAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdminUnitController : ControllerBase
    {
        private readonly ILogger<AdminUnitController> _logger;
        private readonly IAdminUnitService _adminUnitService;
        private readonly IMapper _mapper;

        public AdminUnitController(ILogger<AdminUnitController> logger, IAdminUnitService adminUnitService, IMapper mapper)
        {
            _logger = logger;
            _adminUnitService = adminUnitService;
            _mapper = mapper;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllAdminUnits()
        {
            try
            {
                var adminUnits = await _adminUnitService.GetAllAdminUnits();
                return Ok(_mapper.Map<List<AdminUnitResponseDTO>>(adminUnits));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching all admin units.");
                return StatusCode(500, new { Message = "An unexpected error occurred. Please try again later." });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAdminUnitByID(int id)
        {
            try
            {
                var adminUnit = await _adminUnitService.GetByID(id);
                if (adminUnit == null)
                {
                    return NotFound(new { Message = "Admin unit not found." });
                }
                return Ok(_mapper.Map<AdminUnitResponseDTO>(adminUnit));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching the admin unit with ID {Id}", id);
                return StatusCode(500, new { Message = "An unexpected error occurred. Please try again later." });
            }
        }

        [HttpPost("Add")]
        public async Task<IActionResult> CreateAdminUnit([FromBody] AdminUnitInputDTO adminUnitDTO)
        {
            if (adminUnitDTO == null)
            {
                return BadRequest(new { Message = "Invalid admin unit data." });
            }

            try
            {
                var adminUnit = _mapper.Map<AdminUnit>(adminUnitDTO);
                var created = await _adminUnitService.CreateAdminUnit(adminUnit);
                return Created(string.Empty, _mapper.Map<AdminUnitResponseDTO>(created));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating an admin unit.");
                return StatusCode(500, new { Message = "An unexpected error occurred. Please try again later." });
            }
        }

        [HttpPut("Update")]
        public async Task<IActionResult> UpdateAdminUnit([FromBody] AdminUnitResponseDTO adminUnitDTO)
        {
            if (adminUnitDTO == null)
            {
                return BadRequest(new { Message = "Invalid admin unit data." });
            }

            try
            {
                var existingUnit = await _adminUnitService.GetByID(adminUnitDTO.AdminUnitID);
                if (existingUnit == null)
                {
                    return NotFound(new { Message = "Admin unit not found." });
                }

                _mapper.Map(adminUnitDTO, existingUnit);
                var updatedUnit = await _adminUnitService.UpdateAdminUnit(existingUnit);
                return Ok(_mapper.Map<AdminUnitResponseDTO>(updatedUnit));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating the admin unit with ID {Id}", adminUnitDTO.AdminUnitID);
                return StatusCode(500, new { Message = "An unexpected error occurred. Please try again later." });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAdminUnit(int id)
        {
            try
            {
                var existingUnit = await _adminUnitService.GetByID(id);
                if (existingUnit == null)
                {
                    return NotFound(new { Message = "Admin unit not found." });
                }

                var deletedUnit = await _adminUnitService.DeleteAdminUnit(id);
                return Ok(_mapper.Map<AdminUnitResponseDTO>(deletedUnit));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting the admin unit with ID {Id}", id);
                return StatusCode(500, new { Message = "An unexpected error occurred. Please try again later." });
            }
        }
    }
}
