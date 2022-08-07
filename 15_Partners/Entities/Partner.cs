using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using DB.Common;

namespace DB.Entities;

public abstract class Partner : Entity<Guid>
{
    public string TitleAr { get; set; }
    public string TitleEn { get; set; }
    public string DescriptionAr { get; set; }
    public string DescriptionEn { get; set; }
    public string IconUrl { get; set; }
}
public class NormalPartner : Partner { }
public class LocalPartner : Partner
{
    public string PartnershipTitleAr { get; set; }
    public string PartnershipTitleEn { get; set; }
    public string RmDepartmentAr { get; set; }
    public string RmDepartmentEn { get; set; }
    public DateTime? ContractDate { get; set; }
    public bool? IsActiveContract { get; set; }
}

public class PartnerConfig : EntityConfig<Partner, Guid>
{
    public override void Configure(EntityTypeBuilder<Partner> entity)
    {
        entity.ToTable("partners");
        base.Configure(entity);

        entity.Property(e => e.TitleAr)
            .IsRequired()
            .HasMaxLength(200)
            .HasColumnName("partner_title_ar")
            .HasColumnType("nvarchar(200)");

        entity.Property(e => e.TitleEn)
            .IsRequired()
            .HasMaxLength(200)
            .HasColumnName("partner_title_en")
            .HasColumnType("varchar(200)");

        entity.Property(e => e.DescriptionAr)
            .IsRequired()
            .HasMaxLength(500)
            .HasColumnName("description_ar")
            .HasColumnType("nvarchar(500)");

        entity.Property(e => e.DescriptionEn)
            .IsRequired()
            .HasMaxLength(500)
            .HasColumnName("description_en")
            .HasColumnType("varchar(500)");

        entity.Property(e => e.IconUrl)
            .IsRequired()
            .HasMaxLength(500)
            .HasColumnName("icon_url")
            .HasColumnType("varchar(500)");

        entity.HasDiscriminator<string>("discriminator")
            .HasValue<NormalPartner>("normal_partner")
            .HasValue<LocalPartner>("local_partner");

        entity.Property("discriminator")
            .HasMaxLength(50)
            .HasColumnType("varchar(50)");

    }
}
public class LocalPartnerConfig : IEntityTypeConfiguration<LocalPartner>
{
    public void Configure(EntityTypeBuilder<LocalPartner> entity)
    {
        entity.Property(e => e.PartnershipTitleAr)
            .HasMaxLength(200)
            .HasColumnName("partnership_title_ar")
            .HasColumnType("nvarchar(200)");

        entity.Property(e => e.PartnershipTitleEn)
            .HasMaxLength(200)
            .HasColumnName("partnership_title_en")
            .HasColumnType("varchar(200)");

        entity.Property(e => e.RmDepartmentAr)
            .HasMaxLength(200)
            .HasColumnName("rm_department_ar")
            .HasColumnType("nvarchar(200)");

        entity.Property(e => e.RmDepartmentEn)
            .HasMaxLength(200)
            .HasColumnName("rm_department_en")
            .HasColumnType("varchar(200)");

        entity.Property(e => e.ContractDate)
            .HasColumnName("contract_date")
            .HasColumnType("datetime");

        entity.Property(e => e.IsActiveContract)
            .HasDefaultValue(false)
            .HasColumnName("is_active_contract")
            .HasColumnType("bit");
    }
}
