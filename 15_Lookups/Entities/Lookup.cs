using DB.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DB.Entities;
public class Lookup : Entity<Guid>
{
    public string TitleAr { get; set; }
    public string TitleEn { get; set; }
}
public class Qualification : Lookup
{
}
public class City : Lookup
{
}
public class Nationality : Lookup
{
}
public class LookupConfig : EntityConfig<Lookup, Guid>
{
    public override void Configure(EntityTypeBuilder<Lookup> entity)
    {
        entity.ToTable("lookups");
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

        entity.HasDiscriminator<string>("discriminator")
            .HasValue<City>("city")
            .HasValue<Qualification>("qualification")
            .HasValue<Nationality>("nationality");

        entity.Property("discriminator")
            .HasMaxLength(25)
            .HasColumnType("varchar(25)");

    }
}


