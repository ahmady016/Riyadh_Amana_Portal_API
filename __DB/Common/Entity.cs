namespace DB.Common;

public abstract class Entity<T>
{
    public virtual T Id { get; set; }

    public virtual bool IsDeleted { get; set; } = false;
    public virtual bool IsActive { get; set; } = false;

    public virtual DateTime CreatedAt { get; set; }
    public virtual string CreatedBy { get; set; }

    public virtual DateTime? UpdatedAt { get; set; }
    public virtual string UpdatedBy { get; set; }

    public virtual DateTime? DeletedAt { get; set; }
    public virtual string DeletedBy { get; set; }

    public virtual DateTime? ActivatedAt { get; set; }
    public virtual string ActivatedBy { get; set; }
}
