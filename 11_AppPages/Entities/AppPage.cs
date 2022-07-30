using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using DB.Common;

namespace DB.Entities;

public class AppPage : Entity<Guid>
{
    public string Title { get; set; }
    public string Description { get; set; }
    public string Url { get; set; }

    public virtual ICollection<PageKey> Keys { get; set; } = new HashSet<PageKey>();
}

public class AppPageConfig : EntityConfig<AppPage, Guid>
{
    public override void Configure(EntityTypeBuilder<AppPage> entity)
    {
        entity.ToTable("app_pages");
        base.Configure(entity);

        entity.Property(e => e.Title)
            .IsRequired()
            .HasMaxLength(80)
            .HasColumnName("title")
            .HasColumnType("nvarchar(80)");

        entity.Property(e => e.Description)
            .IsRequired()
            .HasMaxLength(200)
            .HasColumnName("description")
            .HasColumnType("varchar(200)");

        entity.Property(e => e.Url)
            .IsRequired()
            .HasMaxLength(400)
            .HasColumnName("url")
            .HasColumnType("varchar(400)");

        entity.HasIndex(e => e.Title, "title_unique_index")
            .IsUnique();

    }
}
