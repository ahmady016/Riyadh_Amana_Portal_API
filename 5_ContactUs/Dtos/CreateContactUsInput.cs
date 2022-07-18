using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace amana_mono._5_ContactUs.Dtos
{
    public class CreateContactUsInput
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        [Required]
        [StringLength(100)]
        public string UserId { get; set; }
        [Required]
        [StringLength(100)]
        public string MobileNo { get; set; }
        [Required]
        [StringLength(100)]
        public string Email { get; set; }
        [Required]
        [StringLength(100)]
        public string Subject { get; set; }
        [Required]
        [StringLength(100)]
        public string Description { get; set; }
        [Required]
        [StringLength(100)]
        public int? CreatedBy { get; set; }
        [Required]
        [StringLength(100)]
        public int? ModifiedBy { get; set; }
        [Required]
        [StringLength(100)]
        public DateTime? ModifiedDate { get; set; }
        [Required]
        [StringLength(100)]
        public DateTime? CreatedDate { get; set; }
        [Required]
        [StringLength(100)]
        public int? ReferenceId { get; set; }
        [Required]
        [StringLength(100)]
        public bool? IsDeleted { get; set; }
        [Required]
        [StringLength(100)]
        public int? EntityId { get; set; }

        public string FileUrl { get; set; }

        public string ComplainId { get; set; }
        [Required]
        [StringLength(100)]
        public string Address { get; set; }
        public string Longitude { get; set; }
        public string Latitude { get; set; }
    }
}
