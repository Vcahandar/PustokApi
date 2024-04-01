
namespace Pustok.Core.Entities.Common
{
    public abstract class BaseEntity
    {
        public Guid Id { get; set; }
        public virtual bool IsDeleted { get; set; }
    }
}
