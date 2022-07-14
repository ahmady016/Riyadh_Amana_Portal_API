using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using DB.Common;

namespace DB.Entities;

public class News : Entity<Guid>
{
    public string TitleAr { get; set; }
    public string TitleEn { get; set; }
    public string SourceAr { get; set; }
    public string SourceEn { get; set; }
    public string BriefAr { get; set; }
    public string BriefEn { get; set; }
    public string ContentAr { get; set; }
    public string ContentEn { get; set; }
    public string ImageUrl { get; set; }
    public string ThumbUrl { get; set; }
    public string HijriDate { get; set; }
    public string Tags { get; set; }
    public bool? IsInHome { get; set; } = false;
}

public class NewsConfig : IEntityTypeConfiguration<News>
{
    public void Configure(EntityTypeBuilder<News> entity)
    {
        entity.ToTable("news");
        entity.HasKey(e => e.Id);

        entity.Property(e => e.Id)
            .HasDefaultValueSql("NEWID()")
            .HasColumnName("id")
            .HasColumnType("uniqueidentifier");

        entity.Property(e => e.TitleAr)
            .IsRequired()
            .HasMaxLength(100)
            .HasColumnName("title_ar")
            .HasColumnType("nvarchar(100)");

        entity.Property(e => e.TitleEn)
            .IsRequired()
            .HasMaxLength(100)
            .HasColumnName("title_en")
            .HasColumnType("varchar(100)");

        entity.Property(e => e.SourceAr)
            .IsRequired()
            .HasMaxLength(100)
            .HasColumnName("source_ar")
            .HasColumnType("nvarchar(100)");

        entity.Property(e => e.SourceEn)
            .IsRequired()
            .HasMaxLength(100)
            .HasColumnName("source_en")
            .HasColumnType("varchar(100)");

        entity.Property(e => e.BriefAr)
            .IsRequired()
            .HasMaxLength(200)
            .HasColumnName("brief_ar")
            .HasColumnType("nvarchar(200)");

        entity.Property(e => e.BriefEn)
            .IsRequired()
            .HasMaxLength(200)
            .HasColumnName("brief_en")
            .HasColumnType("varchar(200)");

        entity.Property(e => e.ContentAr)
            .IsRequired()
            .HasMaxLength(2000)
            .HasColumnName("content_ar")
            .HasColumnType("nvarchar(2000)");

        entity.Property(e => e.ContentEn)
            .IsRequired()
            .HasMaxLength(2000)
            .HasColumnName("content_en")
            .HasColumnType("varchar(2000)");

        entity.Property(e => e.ThumbUrl)
            .IsRequired()
            .HasMaxLength(400)
            .HasColumnName("thumb_url")
            .HasColumnType("varchar(400)");

        entity.Property(e => e.ImageUrl)
            .IsRequired()
            .HasMaxLength(400)
            .HasColumnName("image_url")
            .HasColumnType("varchar(400)");

        entity.Property(e => e.HijriDate)
            .HasMaxLength(50)
            .HasColumnName("hijri_date")
            .HasColumnType("nvarchar(50)");

        entity.Property(e => e.Tags)
            .HasMaxLength(500)
            .HasColumnName("tags")
            .HasColumnType("nvarchar(500)");

        entity.Property(e => e.IsInHome)
            .HasDefaultValue(false)
            .HasColumnName("is_in_home")
            .HasColumnType("bit");

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
            .HasDefaultValueSql("GETDATE()")
            .HasColumnName("created_at")
            .HasColumnType("datetime");
        entity.Property(e => e.CreatedBy)
            .IsRequired()
            .HasDefaultValue("app_dev")
            .HasMaxLength(100)
            .HasColumnName("created_by")
            .HasColumnType("varchar(100)");

        entity.Property(e => e.UpdatedAt)
            .HasColumnName("updated_at")
            .HasColumnType("datetime");
        entity.Property(e => e.UpdatedBy)
            .HasMaxLength(100)
            .HasColumnName("updated_by")
            .HasColumnType("varchar(100)");

        entity.Property(e => e.DeletedAt)
            .HasColumnName("deleted_at")
            .HasColumnType("datetime");
        entity.Property(e => e.DeletedBy)
            .HasMaxLength(100)
            .HasColumnName("deleted_by")
            .HasColumnType("varchar(100)");

        entity.Property(e => e.ActivatedAt)
            .HasColumnName("activated_at")
            .HasColumnType("datetime");
        entity.Property(e => e.ActivatedBy)
            .HasMaxLength(100)
            .HasColumnName("activated_by")
            .HasColumnType("varchar(100)");
    }
}
