using System.ComponentModel.DataAnnotations;
using ZBARCH.Core;

namespace ZBARCH.Entities
{
    public abstract class BaseEntity : IEntity
    {
        [Key]
        public Guid Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? ChangedOn { get; set; }
        public Guid CreatedBy { get; set; }
        public Guid? ChangedBy { get; set; }
    }
}
