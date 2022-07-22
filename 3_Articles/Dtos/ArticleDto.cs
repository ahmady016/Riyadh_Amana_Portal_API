namespace Dtos;

public class ArticleDto
{
    public Guid Id { get; set; }
    public string TitleAr { get; set; }
    public string TitleEn { get; set; }
    public string ContentAr { get; set; }
    public string ContentEn { get; set; }
    public string Url { get; set; }
    public DateTime? CreatedAt { get; set; }
    public string CreatedBy { get; set; }
}
