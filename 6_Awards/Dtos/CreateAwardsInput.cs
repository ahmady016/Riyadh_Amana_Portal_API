using System.ComponentModel.DataAnnotations;

namespace amana_mono._6_Awards.Dtos
{
    public class CreateAwardsInput
    {
        [Required]
        [StringLength(80)]
        public string TitleAr { get; set; }
        [Required]
        [StringLength(80)]
        public string TitleEn { get; set; }
        [Required]
        [StringLength(2000)]
        public string ContentAr { get; set; }
        [Required]
        [StringLength(2000)]
        public string ContentEn { get; set; }
        [Required]
        [StringLength(400)]
        public string IconUrl { get; set; }
        [Required]
        [StringLength(400)]
        public string IconBase64Url { get; set; }
        public int? Order { get; set; }
    }
}
