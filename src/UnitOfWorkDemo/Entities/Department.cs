
namespace UnitOfWorkDemo.Entities
{
    public class Department : BaseEntity<int>
    {
        public string Title { get; set; }
        public List<Staff> StaffList { get; set; }
    }
}
