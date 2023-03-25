using System.ComponentModel;

namespace UnitOfWorkDemo.Entities
{
    public interface IAuditableEntity<TId> : IAuditableEntity, IEntity<TId>
    {
    }

    public interface IAuditableEntity : IEntity
    {
        string CreatedBy { get; set; }

        DateTime CreatedAt { get; set; }

        string LastModifiedBy { get; set; }

        DateTime? LastModifiedAt { get; set; }
    }

    public abstract class AuditableEntity<TId> : IAuditableEntity<TId>
    {
        public TId Id { get; set; }


        [DefaultValue("System")]
        public string CreatedBy { get; set; }

        public DateTime CreatedAt { get; set; }

        [DefaultValue("System")]
        public string LastModifiedBy { get; set; }

        public DateTime? LastModifiedAt { get; set; }
    }
}