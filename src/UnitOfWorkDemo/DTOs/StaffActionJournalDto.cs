namespace UnitOfWorkDemo.DTOs
{
    public class StaffActionJournalDto 
    {
        public int StaffId { get; set; }
        public int DepartmentId { get; set; }
        public DateTime Date { get; set; }
        public string Details { get; set; }
    }
}
