using UnitOfWorkDemo.Entities;

namespace UnitOfWorkDemo.Repository
{
    public interface IStaffRepository
    {
    }

    public class StaffRepository : IStaffRepository
    {
        private readonly IRepositoryAsync<Staff, int> _repo;

        public StaffRepository(IRepositoryAsync<Staff, int> repo)
        {
            _repo = repo;
        }
    }
}