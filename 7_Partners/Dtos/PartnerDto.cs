namespace Dtos;

public class PartnerDto
{
    public Guid Id { get; set; }
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
    public DateTime? CreatedAt { get; set; }
    public string CreatedBy { get; set; }
}
