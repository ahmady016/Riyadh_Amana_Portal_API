using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace amana_mono._5_ContactUs.Dtos
{
    public class UpdateContactUsInput
    {
        [Required]
        [RegularExpression(
        @"^(\{){0,1}[0-9a-fA-F]{8}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{12}(\}){0,1}$",
        ErrorMessage = "Not a valid Id value"
    )]
        public Guid Id { get; set; }
    }
}
