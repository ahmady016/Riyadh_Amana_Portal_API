using DB.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DB.Entities;
public class NavLink : Entity<Guid>
{
    public string TitleAr { get; set; } 
    public string TitleEn { get; set; } 
    public string DescriptionAr { get; set; } 
    public string DescriptionEn { get; set; } 
    public string Url { get; set; }
    public Guid NavId { get; set; }

    public Nav Nav { get; set; }
}

public class NavLinkConfig : EntityConfig<NavLink, Guid>
{
    public override void Configure(EntityTypeBuilder<NavLink> entity)
    {
        entity.ToTable("navs_links");
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
            .HasMaxLength(500)
            .HasColumnName("url")
            .HasColumnType("varchar(500)");

        entity.Property(e => e.NavId)
            .IsRequired()
            .HasColumnName("nav_id")
            .HasColumnType("uniqueidentifier");

        entity.HasIndex(e => e.NavId, "navs_links_nav_index");

        entity.HasOne(navlink => navlink.Nav)
            .WithMany(nav => nav.Links)
            .HasForeignKey(navlink => navlink.NavId)
            .OnDelete(DeleteBehavior.Restrict)
            .HasConstraintName("navs_navs_links_fk");

        entity.HasIndex(e => e.TitleAr)
            .HasDatabaseName("navs_links_title_ar_unique_index")
            .IsUnique();

        entity.HasIndex(e => e.TitleEn)
             .HasDatabaseName("navs_links_title_en_unique_index")
             .IsUnique();
    }
}
