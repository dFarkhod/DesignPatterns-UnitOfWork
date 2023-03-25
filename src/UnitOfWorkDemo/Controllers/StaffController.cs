using Mapster;
using Microsoft.AspNetCore.Mvc;
using UnitOfWorkDemo.DTOs;
using UnitOfWorkDemo.Entities;

namespace UnitOfWorkDemo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StaffController : ControllerBase
    {
        private readonly ILogger<StaffController> _logger;
        private readonly IUnitOfWork<int> _unitOfWork;

        public StaffController(ILogger<StaffController> logger, IUnitOfWork<int> unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<StaffDto>> GetById(int id)
        {
            var _staff = await _unitOfWork.Repository<Staff>().GetByIdAsync(id);
            if (_staff == null)
            {
                return NotFound();
            }

            var result = _staff.Adapt<StaffDto>();

            return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult<List<StaffDto>>> GetAll()
        {
            var staffs = await _unitOfWork.Repository<Staff>().GetAllAsync();

            var result = staffs.Adapt<IList<StaffDto>>();

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<StaffDto>> Create(StaffDto staff)
        {
            try
            {
                var newStaff = staff.Adapt<Staff>();

                await _unitOfWork.Repository<Staff>().AddAsync(newStaff);
                await _unitOfWork.Commit();

                return CreatedAtAction(nameof(GetById), new { id = newStaff.Id }, newStaff);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in Create(): {ex.Message}", ex);
                await _unitOfWork.Rollback();
                return StatusCode(500, "An error occurred while creating the staff.");
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update(StaffDto staff)
        {
            try
            {
                var _staff = await _unitOfWork.Repository<Staff>().GetByIdAsync(staff.Id);
                if (_staff != null)
                {
                    var updatedStaff = staff.Adapt<Staff>();

                    await _unitOfWork.Repository<Staff>().UpdateAsync(updatedStaff);
                    await _unitOfWork.Commit();
                }
                else
                {
                    return NotFound();
                }

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in Update(): {ex.Message}", ex);
                await _unitOfWork.Rollback();
                return StatusCode(500, "An error occurred while updating an object.");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var _staff = await _unitOfWork.Repository<Staff>().GetByIdAsync(id);
            if (_staff == null)
            {
                return NotFound();
            }

            await _unitOfWork.Repository<Staff>().DeleteAsync(_staff);
            await _unitOfWork.Commit();

            return NoContent();
        }
    }
}