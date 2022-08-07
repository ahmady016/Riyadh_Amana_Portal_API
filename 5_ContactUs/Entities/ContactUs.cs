using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using DB.Common;

namespace DB.Entities;

public class ContactUs : Entity<Guid>
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Mobile { get; set; }
    public string Address { get; set; }
    public string Longitude { get; set; }
    public string Latitude { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string FileUrl { get; set; }
}

public class ContactUsConfig : EntityConfig<ContactUs, Guid>
{
    public override void Configure(EntityTypeBuilder<ContactUs> entity)
    {
        entity.ToTable("contact_us");
        base.Configure(entity);

        entity.Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(100)
            .HasColumnName("name")
            .HasColumnType("nvarchar(100)");

        entity.Property(e => e.Email)
            .IsRequired()
            .HasMaxLength(100)
            .HasColumnName("email")
            .HasColumnType("varchar(100)");

        entity.Property(e => e.Mobile)
            .IsRequired()
            .HasMaxLength(20)
            .HasColumnName("mobile")
            .HasColumnType("varchar(20)");

        entity.Property(e => e.Address)
            .HasMaxLength(200)
            .HasColumnName("address")
            .HasColumnType("varchar(200)");

        entity.Property(e => e.Longitude)
            .HasMaxLength(20)
            .HasColumnName("longitude")
            .HasColumnType("varchar(20)");

        entity.Property(e => e.Latitude)
            .HasMaxLength(20)
            .HasColumnName("latitude")
            .HasColumnType("varchar(20)");

        entity.Property(e => e.Title)
            .IsRequired()
            .HasMaxLength(100)
            .HasColumnName("title")
            .HasColumnType("nvarchar(100)");

        entity.Property(e => e.Description)
            .IsRequired()
            .HasMaxLength(500)
            .HasColumnName("description")
            .HasColumnType("nvarchar(500)");

        entity.Property(e => e.FileUrl)
            .HasMaxLength(500)
            .HasColumnName("file_url")
            .HasColumnType("varchar(500)");

    }
}
