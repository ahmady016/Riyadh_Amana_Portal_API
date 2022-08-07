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

public class NewsConfig : EntityConfig<News, Guid>
{
    public override void Configure(EntityTypeBuilder<News> entity)
    {
        entity.ToTable("news");
        base.Configure(entity);

        entity.Property(e => e.TitleAr)
            .IsRequired()
            .HasMaxLength(200)
            .HasColumnName("title_ar")
            .HasColumnType("nvarchar(200)");

        entity.Property(e => e.TitleEn)
            .IsRequired()
            .HasMaxLength(200)
            .HasColumnName("title_en")
            .HasColumnType("varchar(200)");

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
            .HasMaxLength(400)
            .HasColumnName("brief_ar")
            .HasColumnType("nvarchar(400)");

        entity.Property(e => e.BriefEn)
            .IsRequired()
            .HasMaxLength(400)
            .HasColumnName("brief_en")
            .HasColumnType("varchar(400)");

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
            .HasMaxLength(500)
            .HasColumnName("thumb_url")
            .HasColumnType("varchar(500)");

        entity.Property(e => e.ImageUrl)
            .IsRequired()
            .HasMaxLength(500)
            .HasColumnName("image_url")
            .HasColumnType("varchar(500)");

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

    }
}
