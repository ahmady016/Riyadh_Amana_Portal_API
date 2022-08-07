using System.ComponentModel.DataAnnotations;

namespace Dtos;

public class CreateLocalPartnerInput : CreateNormalPartnerInput
{
    [Required(ErrorMessage = "PartnershipTitleAr is required")]
    [StringLength(200, MinimumLength = 5, ErrorMessage = "PartnershipTitleAr Must be between 5 and 200 characters")]
    public string PartnershipTitleAr { get; set; }

    [Required(ErrorMessage = "PartnershipTitleEn is required")]
    [StringLength(200, MinimumLength = 5, ErrorMessage = "PartnershipTitleEn Must be between 5 and 200 characters")]
    public string PartnershipTitleEn { get; set; }

    [Required(ErrorMessage = "RmDepartmentAr is required")]
    [StringLength(200, MinimumLength = 5, ErrorMessage = "RmDepartmentAr Must be between 5 and 200 characters")]
    public string RmDepartmentAr { get; set; }

    [Required(ErrorMessage = "RmDepartmentEn is required")]
    [StringLength(200, MinimumLength = 5, ErrorMessage = "RmDepartmentEn Must be between 5 and 200 characters")]
    public string RmDepartmentEn { get; set; }

    [DataType(DataType.Date)]
    public DateTime? ContractDate { get; set; }

    public bool? IsActiveContract { get; set; }
}
