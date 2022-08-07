using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using DB.Common;

namespace DB.Entities;

public abstract class Lookup : Entity<Guid>
{
    public string TitleAr { get; set; }
    public string TitleEn { get; set; }
}
public class City : Lookup { }
public class Nationality : Lookup { }
public class Qualification : Lookup { }

public class LookupConfig : EntityConfig<Lookup, Guid>
{
    public override void Configure(EntityTypeBuilder<Lookup> entity)
    {
        entity.ToTable("lookups");
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

        entity.HasDiscriminator<string>("discriminator")
            .HasValue<City>("city")
            .HasValue<Nationality>("nationality")
            .HasValue<Qualification>("qualification");

        entity.Property("discriminator")
            .HasMaxLength(100)
            .HasColumnType("varchar(100)");

        entity.HasIndex(e => e.TitleAr)
            .HasDatabaseName("lookups_title_ar_unique_index")
            .IsUnique();

        entity.HasIndex(e => e.TitleEn)
            .HasDatabaseName("lookups_title_en_unique_index")
            .IsUnique();

    }
}
