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

public class AdvertisementConfig : IEntityTypeConfiguration<Advertisement>
{
    public void Configure(EntityTypeBuilder<Advertisement> entity)
    {
        entity.ToTable("advertisements");
        entity.HasKey(e => e.Id);

        entity.Property(e => e.Id)
            .HasDefaultValueSql("NEWID()")
            .HasColumnName("id")
            .HasColumnType("uniqueidentifier");

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

        entity.Property(e => e.ImageUrl)
            .IsRequired()
            .HasMaxLength(400)
            .HasColumnName("image_url")
            .HasColumnType("varchar(400)");

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

        entity.Property(e => e.IsDeleted)
            .IsRequired()
            .HasDefaultValue(false)
            .HasColumnName("is_deleted")
            .HasColumnType("bit");

        entity.Property(e => e.IsActive)
            .IsRequired()
            .HasDefaultValue(false)
            .HasColumnName("is_active")
            .HasColumnType("bit");

        entity.Property(e => e.CreatedAt)
            .IsRequired()
            .HasDefaultValueSql("GETDATE()")
            .HasColumnName("created_at")
            .HasColumnType("datetime");
        entity.Property(e => e.CreatedBy)
            .IsRequired()
            .HasDefaultValue("app_dev")
            .HasMaxLength(100)
            .HasColumnName("created_by")
            .HasColumnType("varchar(100)");

        entity.Property(e => e.UpdatedAt)
            .HasColumnName("updated_at")
            .HasColumnType("datetime");
        entity.Property(e => e.UpdatedBy)
            .HasMaxLength(100)
            .HasColumnName("updated_by")
            .HasColumnType("varchar(100)");

        entity.Property(e => e.DeletedAt)
            .HasColumnName("deleted_at")
            .HasColumnType("datetime");
        entity.Property(e => e.DeletedBy)
            .HasMaxLength(100)
            .HasColumnName("deleted_by")
            .HasColumnType("varchar(100)");

        entity.Property(e => e.ActivatedAt)
            .HasColumnName("activated_at")
            .HasColumnType("datetime");
        entity.Property(e => e.ActivatedBy)
            .HasMaxLength(100)
            .HasColumnName("activated_by")
            .HasColumnType("varchar(100)");
    }
}
