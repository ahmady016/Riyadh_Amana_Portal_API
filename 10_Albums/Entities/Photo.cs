using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using DB.Common;

namespace DB.Entities;

public class Photo : Entity<Guid>
{
    public string TitleAr { get; set; }
    public string TitleEn { get; set; }
    public string DescriptionAr { get; set; }
    public string DescriptionEn { get; set; }
    public string TagsAr { get; set; }
    public string TagsEn { get; set; }
    public string Url { get; set; }
    public string ThumbUrl { get; set; }
    public Guid? AlbumId { get; set; }

    public Album Album { get; set; }
}

public class PhotoConfig : EntityConfig<Photo, Guid>
{
    public override void Configure(EntityTypeBuilder<Photo> entity)
    {
        entity.ToTable("photos");
        base.Configure(entity);

        entity.Property(e => e.TitleAr)
            .IsRequired()
            .HasMaxLength(200)
            .HasColumnName("title_ar")
            .HasColumnType("nvarchar(100)");

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

        entity.Property(e => e.AlbumId)
            .HasColumnName("album_id")
            .HasColumnType("uniqueidentifier");

        entity.HasIndex(e => e.AlbumId, "photos_albums_index");

        entity.HasOne(photo => photo.Album)
            .WithMany(album => album.Photos)
            .HasForeignKey(photo => photo.AlbumId)
            .OnDelete(DeleteBehavior.SetNull)
            .HasConstraintName("albums_photos_fk");
    }
}
