using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using DB.Common;

namespace DB.Entities;

public class Document : Entity<Guid>
{
    public string TitleAr { get; set; }
    public string TitleEn { get; set; }
    public string DescriptionAr { get; set; }
    public string DescriptionEn { get; set; }
    public string Url { get; set; }
    public string Base64Url { get; set; }
}

public class DocumentConfig : EntityConfig<Document, Guid>
{
    public override void Configure(EntityTypeBuilder<Document> entity)
    {
        entity.ToTable("documents");
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
            .IsRequired()
            .HasMaxLength(500)
            .HasColumnName("description_ar")
            .HasColumnType("nvarchar(500)");

        entity.Property(e => e.DescriptionEn)
            .IsRequired()
            .HasMaxLength(500)
            .HasColumnName("description_en")
            .HasColumnType("varchar(500)");

        entity.Property(e => e.Url)
            .HasMaxLength(500)
            .HasColumnName("url")
            .HasColumnType("varchar(500)");

        entity.Property(e => e.Base64Url)
            .HasMaxLength(1000)
            .HasColumnName("base64_url")
            .HasColumnType("varchar(1000)");

    }
}
