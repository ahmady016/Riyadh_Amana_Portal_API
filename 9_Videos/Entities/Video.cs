using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using DB.Common;

namespace DB.Entities;

public class Video : Entity<Guid>
{
    public string TitleAr { get; set; }
    public string TitleEn { get; set; }
    public string DescriptionAr { get; set; }
    public string DescriptionEn { get; set; }
    public string ThumbUrl { get; set; }
    public string Url { get; set; }
}

public class VideoConfig : EntityConfig<Video, Guid>
{
    public override void Configure(EntityTypeBuilder<Video> entity)
    {
        entity.ToTable("videos");
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

        entity.Property(e => e.DescriptionAr)
            .HasMaxLength(500)
            .HasColumnName("description_ar")
            .HasColumnType("nvarchar(500)");

        entity.Property(e => e.DescriptionEn)
            .HasMaxLength(500)
            .HasColumnName("description_en")
            .HasColumnType("varchar(500)");

        entity.Property(e => e.Url)
            .IsRequired()
            .HasMaxLength(500)
            .HasColumnName("url")
            .HasColumnType("varchar(500)");

        entity.Property(e => e.ThumbUrl)
            .IsRequired()
            .HasMaxLength(500)
            .HasColumnName("thumb_url")
            .HasColumnType("varchar(500)");

        entity.HasIndex(e => e.TitleAr)
            .HasDatabaseName("title_ar_unique_index")
            .IsUnique();

        entity.HasIndex(e => e.TitleEn)
            .HasDatabaseName("title_en_unique_index")
            .IsUnique();

    }
}
