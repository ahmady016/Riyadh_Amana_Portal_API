using System.ComponentModel.DataAnnotations;

namespace Dtos;

public class CreateDocumentInput
{
    [Required]
    [StringLength(80)]
    public string TitleAr { get; set; }
    [Required]
    [StringLength(80)]
    public string TitleEn { get; set; }

    [Required]
    [StringLength(2000)]
    public string DescriptionAr { get; set; }

    [Required]
    [StringLength(2000)]
    public string DescriptionEn { get; set; }
    [Required]
    [StringLength(400)]
    public string Url { get; set; }
    [Required]
    [StringLength(400)]
    public string Base64Url { get; set; }
}
