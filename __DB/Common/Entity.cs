using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DB.Common;

public abstract class Entity<Tkey>
{
    public virtual Tkey Id { get; set; }

    public virtual bool IsDeleted { get; set; } = false;
    public virtual bool IsActive { get; set; } = false;

    public virtual DateTime CreatedAt { get; set; } = DateTime.UtcNow.AddHours(3);
    public virtual string CreatedBy { get; set; } = "app_dev";

    public virtual DateTime? UpdatedAt { get; set; }
    public virtual string UpdatedBy { get; set; }

    public virtual DateTime? DeletedAt { get; set; }
    public virtual string DeletedBy { get; set; }

    public virtual DateTime? ActivatedAt { get; set; }
    public virtual string ActivatedBy { get; set; }
}

public abstract class EntityConfig<T, Tkey> : IEntityTypeConfiguration<T> where T : Entity<Tkey>
{
    public virtual void Configure(EntityTypeBuilder<T> entity)
    {
        entity.HasKey(e => e.Id);
        entity.Property(e => e.Id)
            .HasDefaultValueSql("NEWID()")
            .HasColumnName("id")
            .HasColumnType("uniqueidentifier");

        entity.Property(e => e.IsDeleted)
            .IsRequired()
            .HasDefaultValue(false)
            .HasColumnName("is_deleted")
            .HasColumnType("bit");

        entity.Property(e => e.IsActive)
            .IsRequired()
            .HasDefaultValue(false)
            .HasColumnName("is_active")
            .HasColumnType("bit");

        entity.Property(e => e.CreatedAt)
            .IsRequired()
            .HasDefaultValueSql("SYSDATETIME()")
            .HasColumnName("created_at")
            .HasColumnType("datetime2(3)");

        entity.Property(e => e.CreatedBy)
            .IsRequired()
            .HasDefaultValue("app_dev")
            .HasMaxLength(100)
            .HasColumnName("created_by")
            .HasColumnType("varchar(100)");

        entity.Property(e => e.UpdatedAt)
            .HasColumnName("updated_at")
            .HasColumnType("datetime2(3)");

        entity.Property(e => e.UpdatedBy)
            .HasMaxLength(100)
            .HasColumnName("updated_by")
            .HasColumnType("varchar(100)");

        entity.Property(e => e.DeletedAt)
            .HasColumnName("deleted_at")
            .HasColumnType("datetime2(3)");

        entity.Property(e => e.DeletedBy)
            .HasMaxLength(100)
            .HasColumnName("deleted_by")
            .HasColumnType("varchar(100)");

        entity.Property(e => e.ActivatedAt)
            .HasColumnName("activated_at")
            .HasColumnType("datetime2(3)");

        entity.Property(e => e.ActivatedBy)
            .HasMaxLength(100)
            .HasColumnName("activated_by")
            .HasColumnType("varchar(100)");

    }
}
