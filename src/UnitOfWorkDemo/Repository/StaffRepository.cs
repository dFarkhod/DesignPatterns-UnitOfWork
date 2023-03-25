using UnitOfWorkDemo.Entities;

namespace UnitOfWorkDemo.Repository
{
    public interface IStaffRepository
    {
        List<Staff> GetStaffWithoutDepartment();
    }

    public class StaffRepository : IStaffRepository
    {
        private readonly IRepositoryAsync<Staff, int> _repo;

        public StaffRepository(IRepositoryAsync<Staff, int> repo)
        {
            _repo = repo;
        }

        public List<Staff> GetStaffWithoutDepartment()
        {
            return _repo.Entities.Where(s => s.Department == null)?.ToList();
        }

    }
}