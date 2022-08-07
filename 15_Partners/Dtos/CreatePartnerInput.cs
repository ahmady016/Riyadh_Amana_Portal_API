using System.ComponentModel.DataAnnotations;

namespace Dtos;

public class CreatePartnerInput
{
    [Required]
    [StringLength(100)]
    public string PartnershipTitleAr { get; set; }
    [Required]
    [StringLength(100)]
    public string PartnershipTitleEn { get; set; }
    [Required]
    [StringLength(70)]
    public string PartnerTitleAr { get; set; }
    [Required]
    [StringLength(70)]
    public string PartnerTitleEn { get; set; }
    [Required]
    [StringLength(2000)]
    public string ContentAr { get; set; }
    [Required]
    [StringLength(2000)]
    public string ContentEn { get; set; }
    [StringLength(40)]
    public string PartnerAddressAr { get; set; }
    [StringLength(40)]
    public string PartnerAddressEn { get; set; }
    public string RmDepartmentEn { get; set; }
    public string RmDepartmentAr { get; set; }
    [StringLength(400)]
    public string IconUrl { get; set; }
    [StringLength(400)]
    public string IconBase64Url { get; set; }
    public DateTime? ContractDate { get; set; }
    public bool? IsActiveContract { get; set; }

}
