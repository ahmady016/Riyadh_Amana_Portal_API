using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using DB.Common;

namespace DB.Entities;

public class Partner : Entity<Guid>
{
    public string PartnershipTitleAr { get; set; }
    public string PartnershipTitleEn { get; set; }
    public string PartnerTitleAr { get; set; }
    public string PartnerTitleEn { get; set; }
    public string ContentAr { get; set; }
    public string ContentEn { get; set; }
    public string PartnerAddressAr { get; set; }
    public string PartnerAddressEn { get; set; }
    public string RmDepartmentEn { get; set; }
    public string RmDepartmentAr { get; set; }
    public string IconUrl { get; set; }
    public string IconBase64Url { get; set; }
    public DateTime? ContractDate { get; set; }
    public bool? IsActiveContract { get; set; }
}

public class PartnerConfig : EntityConfig<Partner, Guid>
{
    public override void Configure(EntityTypeBuilder<Partner> entity)
    {
        entity.ToTable("partners");
        base.Configure(entity);

        entity.Property(e => e.PartnershipTitleAr)
            .IsRequired()
            .HasMaxLength(100)
            .HasColumnName("partnership_title_ar")
            .HasColumnType("nvarchar(100)");

        entity.Property(e => e.PartnershipTitleEn)
            .IsRequired()
            .HasMaxLength(100)
            .HasColumnName("partnership_title_en")
            .HasColumnType("varchar(100)");

        entity.Property(e => e.PartnerTitleAr)
            .IsRequired()
            .HasMaxLength(100)
            .HasColumnName("partner_title_ar")
            .HasColumnType("nvarchar(100)");

        entity.Property(e => e.PartnerTitleEn)
            .IsRequired()
            .HasMaxLength(100)
            .HasColumnName("partner_title_en")
            .HasColumnType("varchar(100)");

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

        entity.Property(e => e.PartnerAddressAr)
            .IsRequired()
            .HasMaxLength(200)
            .HasColumnName("partner_address_ar")
            .HasColumnType("nvarchar(200)");

        entity.Property(e => e.PartnerAddressEn)
            .IsRequired()
            .HasMaxLength(200)
            .HasColumnName("partner_address_en")
            .HasColumnType("varchar(200)");

        entity.Property(e => e.RmDepartmentAr)
            .IsRequired()
            .HasMaxLength(100)
            .HasColumnName("rm_department_ar")
            .HasColumnType("nvarchar(100)");

        entity.Property(e => e.RmDepartmentEn)
            .IsRequired()
            .HasMaxLength(100)
            .HasColumnName("rm_department_en")
            .HasColumnType("varchar(100)");

        entity.Property(e => e.IconUrl)
            .HasMaxLength(400)
            .HasColumnName("icon_url")
            .HasColumnType("varchar(400)");

        entity.Property(e => e.IconBase64Url)
            .HasMaxLength(1000)
            .HasColumnName("icon_base64_url")
            .HasColumnType("varchar(1000)");

        entity.Property(e => e.ContractDate)
            .HasColumnName("contract_date")
            .HasColumnType("datetime");

        entity.Property(e => e.IsActiveContract)
            .HasDefaultValue(true)
            .HasColumnName("is_active_contract")
            .HasColumnType("bit");

    }
}
