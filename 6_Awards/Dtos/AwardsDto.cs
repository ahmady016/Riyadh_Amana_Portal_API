namespace amana_mono._6_Awards.Dtos
{
    public class AwardsDto
    {
        public Guid Id { get; set; }

        public string TitleAr { get; set; }
        public string TitleEn { get; set; }
        public string ContentAr { get; set; }
        public string ContentEn { get; set; }
        public string IconUrl { get; set; }
        public string IconBase64Url { get; set; }
        public int? Order { get; set; }
    }
}
