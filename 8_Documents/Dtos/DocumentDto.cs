namespace Dtos;

public class DocumentDto
{
    public Guid Id { get; set; }
    public string TitleAr { get; set; }
    public string TitleEn { get; set; }
    public string DescriptionAr { get; set; }
    public string DescriptionEn { get; set; }
    public string Url { get; set; }
    public string Base64Url { get; set; }
    public DateTime? CreatedAt { get; set; }
    public string CreatedBy { get; set; }
}
