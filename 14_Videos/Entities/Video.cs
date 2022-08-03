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
            .HasMaxLength(100)
            .HasColumnName("title_ar")
            .HasColumnType("nvarchar(100)");

        entity.Property(e => e.TitleEn)
            .IsRequired()
            .HasMaxLength(100)
            .HasColumnName("title_en")
            .HasColumnType("varchar(100)");

        entity.Property(e => e.DescriptionAr)
            .HasMaxLength(200)
            .HasColumnName("description_ar")
            .HasColumnType("nvarchar(200)");

        entity.Property(e => e.DescriptionEn)
            .HasMaxLength(200)
            .HasColumnName("description_en")
            .HasColumnType("varchar(200)");

        entity.Property(e => e.Url)
            .IsRequired()
            .HasMaxLength(400)
            .HasColumnName("url")
            .HasColumnType("varchar(400)");

        entity.Property(e => e.ThumbUrl)
            .IsRequired()
            .HasMaxLength(400)
            .HasColumnName("thumb_url")
            .HasColumnType("varchar(400)");

        entity.HasIndex(e => e.TitleAr, "title_ar_unique_index")
            .IsUnique();

        entity.HasIndex(e => e.TitleEn, "title_en_unique_index")
            .IsUnique();

    }
}
