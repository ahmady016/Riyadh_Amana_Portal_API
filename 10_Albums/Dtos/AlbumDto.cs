namespace Dtos;

public class AlbumDto
{
    public Guid Id { get; set; }
    public string TitleAr { get; set; }
    public string TitleEn { get; set; }
    public string DescriptionAr { get; set; }
    public string DescriptionEn { get; set; }
    public string TagsAr { get; set; }
    public string TagsEn { get; set; }
    public List<PhotoDto> Photos { get; set; }
}
