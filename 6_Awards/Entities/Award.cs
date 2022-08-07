using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using DB.Common;

namespace DB.Entities;

public class Award : Entity<Guid>
{
    public string TitleAr { get; set; }
    public string TitleEn { get; set; }
    public string ContentAr { get; set; }
    public string ContentEn { get; set; }
    public string IconUrl { get; set; }
    public string IconBase64Url { get; set; }
    public int? Order { get; set; }
}

public class AwardConfig : EntityConfig<Award, Guid>
{
    public override void Configure(EntityTypeBuilder<Award> entity)
    {
        entity.ToTable("awards");
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

        entity.Property(e => e.IconUrl)
            .HasMaxLength(500)
            .HasColumnName("icon_url")
            .HasColumnType("varchar(500)");

        entity.Property(e => e.IconBase64Url)
            .HasMaxLength(1000)
            .HasColumnName("icon_base64_url")
            .HasColumnType("varchar(1000)");

        entity.Property(e => e.Order)
            .HasColumnName("order")
            .HasColumnType("int");

    }
}
