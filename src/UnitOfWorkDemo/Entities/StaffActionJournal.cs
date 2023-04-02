using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UnitOfWorkDemo.Entities
{
    public class StaffActionJournal : BaseEntity<int>
    {
        [Required]
        [ForeignKey("Staff")]
        public int StaffId { get; set; }

        [Required]
        [ForeignKey("Department")]
        public int DepartmentId { get; set; }
        public DateTime Date { get; set; }
        public string Details { get; set; }
    }
}
