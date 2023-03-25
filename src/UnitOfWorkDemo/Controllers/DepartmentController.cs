using Mapster;
using Microsoft.AspNetCore.Mvc;
using UnitOfWorkDemo.DTOs;
using UnitOfWorkDemo.Entities;
using UnitOfWorkDemo.Repository;

namespace UnitOfWorkDemo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DepartmentController : ControllerBase
    {
        private readonly ILogger<DepartmentController> _logger;
        private readonly IDepartmentRepository _repo;
        private readonly IUnitOfWork<int> _unitOfWork;


        public DepartmentController(ILogger<DepartmentController> logger, IUnitOfWork<int> unitOfWork, IDepartmentRepository repo)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _repo = repo;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DepartmentDto>> GetById(int id)
        {
            var department = await _repo.GetByIdAsync(id); //await _unitOfWork.Repository<Department>().GetByIdAsync(id); // _repo.GetByIdAsync(id);
            if (department == null)
            {
                return NotFound();
            }

            var result = department.Adapt<DepartmentDto>();

            return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult<List<DepartmentDto>>> GetAll()
        {
            List<DepartmentDto> result = null;
            var staffs = await _repo.GetAllAsync(); //await _unitOfWork.Repository<Department>().GetAllAsync(); //_repo.GetAllAsync();
            if (staffs != null)
            {
                result = staffs.Adapt<List<DepartmentDto>>();
            }
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<DepartmentDto>> Create(DepartmentDto staff)
        {
            try
            {
                var newDepartment = staff.Adapt<Department>();
                await _repo.AddAsync(newDepartment); //_unitOfWork.Repository<Department>().AddAsync(newDepartment); //_repo.AddAsync(newDepartment);
                //await _unitOfWork.Commit();

                var result = newDepartment.Adapt<DepartmentDto>();
                return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in Create(): {ex.Message}", ex);
                return StatusCode(500, "An error occurred while creating an object.");
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update(DepartmentDto item)
        {
            try
            {
                var _staff = await _repo.GetByIdAsync(item.Id);
                if (_staff != null)
                {
                    var updatedItem = item.Adapt<Department>();
                    await _repo.UpdateAsync(updatedItem);
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
                return StatusCode(500, "An error occurred while updating an object.");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var _existingItem = await _repo.GetByIdAsync(id);
            if (_existingItem == null)
            {
                return NotFound();
            }
            await _repo.DeleteAsync(_existingItem);
            return NoContent();
        }
    }
}