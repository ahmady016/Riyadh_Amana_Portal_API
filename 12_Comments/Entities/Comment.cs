using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using DB.Common;

namespace DB.Entities;

public class Comment : Entity<Guid>
{
    public Guid EntityId { get; set; }
    public string EntityName { get; set; }
    public string CommenterName { get; set; }
    public string CommenterEmail { get; set; }
    public string Text { get; set; }
    public bool IsApproved { get; set; } = false;
    public DateTime? ApprovedAt { get; set; }
    public string ApprovedBy { get; set; }

    public virtual ICollection<Reply> Replies { get; set; } = new HashSet<Reply>();
}

public class CommentConfig : EntityConfig<Comment, Guid>
{
    public override void Configure(EntityTypeBuilder<Comment> entity)
    {
        entity.ToTable("comments");
        base.Configure(entity);

        entity.Property(e => e.EntityId)
            .IsRequired()
            .HasColumnName("entity_id")
            .HasColumnType("uniqueidentifier");

        entity.Property(e => e.EntityName)
            .IsRequired()
            .HasMaxLength(50)
            .HasColumnName("entity_name")
            .HasColumnType("varchar(50)");

        entity.Property(e => e.CommenterName)
            .IsRequired()
            .HasMaxLength(50)
            .HasColumnName("commenter_name")
            .HasColumnType("varchar(50)");

        entity.Property(e => e.CommenterEmail)
            .IsRequired()
            .HasMaxLength(100)
            .HasColumnName("commenter_email")
            .HasColumnType("varchar(100)");

        entity.Property(e => e.Text)
            .IsRequired()
            .HasMaxLength(1000)
            .HasColumnName("text")
            .HasColumnType("varchar(1000)");

        entity.Property(e => e.IsApproved)
            .IsRequired()
            .HasDefaultValue(false)
            .HasColumnName("is_approved")
            .HasColumnType("bit");

        entity.Property(e => e.ApprovedBy)
            .HasMaxLength(100)
            .HasColumnName("approved_by")
            .HasColumnType("varchar(100)");

        entity.Property(e => e.ApprovedAt)
            .HasColumnName("approved_at")
            .HasColumnType("datetime2(3)");

    }
}
