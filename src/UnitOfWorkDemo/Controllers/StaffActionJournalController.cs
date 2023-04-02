using Mapster;
using Microsoft.AspNetCore.Mvc;
using UnitOfWorkDemo.DTOs;
using UnitOfWorkDemo.Entities;

namespace UnitOfWorkDemo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StaffActionJournalController : ControllerBase
    {
        private readonly ILogger<StaffActionJournalController> _logger;
        private readonly IUnitOfWork<int> _unitOfWork;

        public StaffActionJournalController(ILogger<StaffActionJournalController> logger, IUnitOfWork<int> unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        

        [HttpGet]
        public async Task<ActionResult<List<StaffActionJournalDto>>> GetAll()
        {
            var staffs = await _unitOfWork.Repository<StaffActionJournal>().GetAllAsync();

            var result = staffs.Adapt<IList<StaffActionJournalDto>>();

            return Ok(result);
        }

    }
}