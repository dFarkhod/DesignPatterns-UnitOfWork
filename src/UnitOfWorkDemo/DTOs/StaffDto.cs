namespace UnitOfWorkDemo.DTOs
{
    public class StaffDto
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Title { get; set; }
        public DateTime HireDate { get; set; }
        public string Email { get; set; }
        public int DepartmentId { get; set; }    
    }

}
