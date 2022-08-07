namespace Dtos;

public class LocalPartnerDto : NormalPartnerDto
{
    public string PartnershipTitleAr { get; set; }
    public string PartnershipTitleEn { get; set; }
    public string RmDepartmentAr { get; set; }
    public string RmDepartmentEn { get; set; }
    public DateTime? ContractDate { get; set; }
    public bool? IsActiveContract { get; set; }
}
