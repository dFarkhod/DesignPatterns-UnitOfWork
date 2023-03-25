
namespace UnitOfWorkDemo.Entities
{
    public class Department : BaseEntity<int>
    {
        public Department()
        {
            StaffList = new List<Staff>();
        }

        public string Title { get; set; }
        public List<Staff> StaffList { get; set; }
    }
}
