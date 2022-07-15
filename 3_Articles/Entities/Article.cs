using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using DB.Common;

namespace DB.Entities;

public class Article : Entity<Guid>
{
    public string TitleAr { get; set; }
    public string TitleEn { get; set; }
    public string ContentAr { get; set; }
    public string ContentEn { get; set; }
    public string Url { get; set; }
}

public class ArticleConfig : EntityConfig<Article, Guid>
{
    public override void Configure(EntityTypeBuilder<Article> entity)
    {
        entity.ToTable("articles");
        base.Configure(entity);

        entity.Property(e => e.TitleAr)
            .IsRequired()
            .HasMaxLength(80)
            .HasColumnName("title_ar")
            .HasColumnType("nvarchar(80)");

        entity.Property(e => e.TitleEn)
            .IsRequired()
            .HasMaxLength(80)
            .HasColumnName("title_en")
            .HasColumnType("varchar(80)");

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

        entity.Property(e => e.Url)
            .IsRequired()
            .HasMaxLength(400)
            .HasColumnName("url")
            .HasColumnType("varchar(400)");

    }
}
