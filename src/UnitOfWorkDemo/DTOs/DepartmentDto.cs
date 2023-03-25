namespace UnitOfWorkDemo.DTOs
{
    public class DepartmentDto
    {
        public DepartmentDto()
        {
            StaffList = new List<StaffDto>();
        }
        public int Id { get; set; }
        public string Title { get; set; }
        public List<StaffDto> StaffList { get; set; }
    }

}
