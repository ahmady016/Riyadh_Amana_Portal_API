using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using DB.Common;

namespace DB.Entities;

public class Advertisement : Entity<Guid>
{
    public string TitleAr { get; set; }
    public string TitleEn { get; set; }
    public string ContentAr { get; set; }
    public string ContentEn { get; set; }
    public string Url { get; set; }
    public string ImageUrl { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public int? Order { get; set; }
    public bool? IsHomeSlider { get; set; } = false;
    public bool? IsPopup { get; set; } = false;
}

public class AdvertisementConfig : EntityConfig<Advertisement, Guid>
{
    public override void Configure(EntityTypeBuilder<Advertisement> entity)
    {
        entity.ToTable("advertisements");
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

        entity.Property(e => e.Url)
            .IsRequired()
            .HasMaxLength(500)
            .HasColumnName("url")
            .HasColumnType("varchar(500)");

        entity.Property(e => e.ImageUrl)
            .IsRequired()
            .HasMaxLength(500)
            .HasColumnName("image_url")
            .HasColumnType("varchar(500)");

        entity.Property(e => e.StartDate)
            .HasColumnName("start_date")
            .HasColumnType("datetime");

        entity.Property(e => e.EndDate)
            .HasColumnName("end_date")
            .HasColumnType("datetime");

        entity.Property(e => e.Order)
            .HasColumnName("order")
            .HasColumnType("int");

        entity.Property(e => e.IsPopup)
            .HasDefaultValue(false)
            .HasColumnName("is_popup")
            .HasColumnType("bit");

        entity.Property(e => e.IsHomeSlider)
            .HasDefaultValue(false)
            .HasColumnName("is_home_slider")
            .HasColumnType("bit");

    }
}
