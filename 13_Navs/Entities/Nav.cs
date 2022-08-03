using DB.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DB.Entities;
public class Nav : Entity<Guid>
{
    public string TitleAr { get; set; } 
    public string TitleEn { get; set; } 
    public string DescriptionAr { get; set; } 
    public string DescriptionEn { get; set; } 
    public string IconUrl { get; set; }

    public virtual ICollection<NavLink> Links { get; set; } = new HashSet<NavLink>();
}

public class NavConfig : EntityConfig<Nav, Guid>
{
    public override void Configure(EntityTypeBuilder<Nav> entity)
    {
        entity.ToTable("navs");
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

        entity.Property(e => e.DescriptionAr)
            .HasMaxLength(200)
            .HasColumnName("description_ar")
            .HasColumnType("nvarchar(200)");

        entity.Property(e => e.DescriptionEn)
            .HasMaxLength(200)
            .HasColumnName("description_en")
            .HasColumnType("varchar(200)");

        entity.Property(e => e.IconUrl)
           .IsRequired()
           .HasMaxLength(400)
           .HasColumnName("icon_url")
           .HasColumnType("varchar(400)");

        entity.HasIndex(e => e.TitleAr)
         .HasDatabaseName("navs_title_ar_unique_index")
         .IsUnique();
        
        entity.HasIndex(e => e.TitleEn)
         .HasDatabaseName("navs_title_en_unique_index")
         .IsUnique();
    }
}


