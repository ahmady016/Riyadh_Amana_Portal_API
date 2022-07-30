using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using DB.Common;

namespace DB.Entities;

public class PageKey : Entity<Guid>
{
    public string Key { get; set; }
    public string ValueAr { get; set; }
    public string ValueEn { get; set; }
    public Guid PageId { get; set; }

    public AppPage Page { get; set; }
}

public class PageKeyConfig : EntityConfig<PageKey, Guid>
{
    public override void Configure(EntityTypeBuilder<PageKey> entity)
    {
        entity.ToTable("pages_keys");
        base.Configure(entity);

        entity.Property(e => e.Key)
            .IsRequired()
            .HasMaxLength(50)
            .HasColumnName("key")
            .HasColumnType("nvarchar(50)");

        entity.Property(e => e.ValueAr)
            .IsRequired()
            .HasMaxLength(400)
            .HasColumnName("value_ar")
            .HasColumnType("varchar(400)");

        entity.Property(e => e.ValueEn)
            .IsRequired()
            .HasMaxLength(400)
            .HasColumnName("value_en")
            .HasColumnType("varchar(400)");

        entity.Property(e => e.PageId)
            .HasColumnName("page_id")
            .HasColumnType("uniqueidentifier");

        entity.HasIndex(e => e.Key, "key_unique_index")
            .IsUnique();

        entity.HasIndex(e => e.PageId, "app_pages_pages_keys_index");
        entity.HasOne(key => key.Page)
            .WithMany(page => page.Keys)
            .HasForeignKey(photo => photo.PageId)
            .OnDelete(DeleteBehavior.Restrict)
            .HasConstraintName("app_pages_pages_keys_fk");

    }
}
