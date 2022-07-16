using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using DB.Common;

namespace DB.Entities;

public class Permission : Entity<Guid>
{
    public string TitleAr { get; set; }
    public string TitleEn { get; set; }
    public string DescriptionAr { get; set; }
    public string DescriptionEn { get; set; }

    public ICollection<UserPermission> UsersPermissions { get; set; }
}

public class PermissionConfig : EntityConfig<Permission, Guid>
{
    public override void Configure(EntityTypeBuilder<Permission> entity)
    {
        entity.ToTable("permissions");
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
            .HasMaxLength(800)
            .HasColumnName("description_ar")
            .HasColumnType("nvarchar(800)");

        entity.Property(e => e.DescriptionEn)
            .HasMaxLength(800)
            .HasColumnName("description_en")
            .HasColumnType("varchar(800)");

    }
}
