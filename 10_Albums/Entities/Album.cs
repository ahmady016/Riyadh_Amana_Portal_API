using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using DB.Common;

namespace DB.Entities;

public class Album : Entity<Guid>
{
    public string TitleAr { get; set; }
    public string TitleEn { get; set; }
    public string DescriptionAr { get; set; }
    public string DescriptionEn { get; set; }
    public string TagsAr { get; set; }
    public string TagsEn { get; set; }

    public virtual ICollection<Photo> Photos { get; set; } = new HashSet<Photo>();
}

public class AlbumConfig : EntityConfig<Album, Guid>
{
    public override void Configure(EntityTypeBuilder<Album> entity)
    {
        entity.ToTable("albums");
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

        entity.Property(e => e.TagsAr)
            .HasMaxLength(500)
            .HasColumnName("tags_ar")
            .HasColumnType("nvarchar(500)");

        entity.Property(e => e.TagsEn)
            .HasMaxLength(500)
            .HasColumnName("tags_en")
            .HasColumnType("varchar(500)");
    }
}
