
namespace UnitOfWorkDemo.Entities
{
    public class Staff : BaseEntity<int>
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public DateTime HireDate { get; set; }
        public string Title { get; set; }
        public Department Department { get; set; }
        public int DepartmentId { get; set; }
    }
}
