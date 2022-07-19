using System.ComponentModel.DataAnnotations;

namespace amana_mono._6_Awards.Dtos
{
    public class UpdateAwardsInput : CreateAwardsInput
    {
        [Required]
        [RegularExpression(
       @"^(\{){0,1}[0-9a-fA-F]{8}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{12}(\}){0,1}$",
       ErrorMessage = "Not a valid Id value"
   )]
        public Guid Id { get; set; }
    }
}
