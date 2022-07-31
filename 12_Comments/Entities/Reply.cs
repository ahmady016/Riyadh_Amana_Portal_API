using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using DB.Common;

namespace DB.Entities;

public class Reply : Entity<Guid>
{
    public string ReplierName { get; set; }
    public string ReplierEmail { get; set; }
    public string Text { get; set; }
    public bool IsApproved { get; set; } = false;
    public DateTime? ApprovedAt { get; set; }
    public string ApprovedBy { get; set; }
    public Guid CommentId { get; set; }

    public Comment Comment { get; set; }
}

public class ReplyConfig : EntityConfig<Reply, Guid>
{
    public override void Configure(EntityTypeBuilder<Reply> entity)
    {
        entity.ToTable("replies");
        base.Configure(entity);

        entity.Property(e => e.ReplierName)
            .IsRequired()
            .HasMaxLength(50)
            .HasColumnName("replier_name")
            .HasColumnType("varchar(50)");

        entity.Property(e => e.ReplierEmail)
            .IsRequired()
            .HasMaxLength(100)
            .HasColumnName("replier_email")
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

        entity.Property(e => e.CommentId)
            .HasColumnName("comment_id")
            .HasColumnType("uniqueidentifier");

        entity.HasIndex(e => e.CommentId, "comments_replies_index");
        entity.HasOne(reply => reply.Comment)
            .WithMany(comment => comment.Replies)
            .HasForeignKey(reply => reply.CommentId)
            .OnDelete(DeleteBehavior.Restrict)
            .HasConstraintName("comments_replies_fk");

    }
}
