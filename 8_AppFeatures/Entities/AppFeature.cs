using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using DB.Common;

namespace DB.Entities;

public class AppFeature : Entity<Guid>
{
    public string TitleAr { get; set; }
    public string TitleEn { get; set; }
    public string DescriptionAr { get; set; }
    public string DescriptionEn { get; set; }
    public string IconUrl { get; set; }
    public string Url { get; set; }
    public byte? Order { get; set; }
}

public class AppFeatureConfig : EntityConfig<AppFeature, Guid>
{
    public override void Configure(EntityTypeBuilder<AppFeature> entity)
    {
        entity.ToTable("app_features");
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
            .IsRequired()
            .HasMaxLength(500)
            .HasColumnName("description_ar")
            .HasColumnType("nvarchar(500)");

        entity.Property(e => e.DescriptionEn)
            .IsRequired()
            .HasMaxLength(500)
            .HasColumnName("description_en")
            .HasColumnType("varchar(500)");

        entity.Property(e => e.IconUrl)
            .IsRequired()
            .HasMaxLength(500)
            .HasColumnName("icon_url")
            .HasColumnType("varchar(400)");

        entity.Property(e => e.Url)
            .IsRequired()
            .HasMaxLength(500)
            .HasColumnName("url")
            .HasColumnType("varchar(400)");

        entity.Property(e => e.Order)
            .HasColumnName("order")
            .HasColumnType("tinyint");

    }
}
