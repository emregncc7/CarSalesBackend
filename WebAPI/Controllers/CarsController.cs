using Microsoft.AspNetCore.Mvc;
using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using System;
using Microsoft.Extensions.Logging;

namespace WebAPI.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class CarsController : ControllerBase
    {
        private readonly ICarService _carService;
        private readonly ILogger<CarsController> _logger;

        public CarsController(ICarService carService, ILogger<CarsController> logger)
        {
            _carService = carService;
            _logger = logger;
        }

        /// <summary>
        /// Get all cars with optional filtering
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(List<Car>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAll([FromQuery] int? brandId = null, [FromQuery] int? colorId = null)
        {
            try
            {
                var result = await _carService.GetAllAsync(brandId, colorId);
                if (!result.Success)
                    return NotFound(result.Message);

                return Ok(result.Data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting all cars");
                return StatusCode(500, "Internal server error occurred");
            }
        }

        /// <summary>
        /// Get car by ID
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Car), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var result = await _carService.GetByIdAsync(id);
                if (!result.Success)
                    return NotFound(result.Message);

                return Ok(result.Data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting car with ID: {CarId}", id);
                return StatusCode(500, "Internal server error occurred");
            }
        }

        /// <summary>
        /// Add a new car
        /// </summary>
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(Car), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Add([FromBody] Car car)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var result = await _carService.AddAsync(car);
                if (!result.Success)
                    return BadRequest(result.Message);

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding new car");
                return StatusCode(500, "Internal server error occurred");
            }
        }

        /// <summary>
        /// Update an existing car
        /// </summary>
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(int id, [FromBody] Car car)
        {
            try
            {
                if (id != car.Id)
                    return BadRequest("ID mismatch");

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var result = await _carService.UpdateAsync(car);
                if (!result.Success)
                    return NotFound(result.Message);

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating car with ID: {CarId}", id);
                return StatusCode(500, "Internal server error occurred");
            }
        }

        /// <summary>
        /// Delete a car
        /// </summary>
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var result = await _carService.DeleteAsync(id);
                if (!result.Success)
                    return NotFound(result.Message);

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting car with ID: {CarId}", id);
                return StatusCode(500, "Internal server error occurred");
            }
        }
    }
}
