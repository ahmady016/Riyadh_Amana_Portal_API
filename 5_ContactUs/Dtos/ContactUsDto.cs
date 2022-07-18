using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace amana_mono._5_ContactUs.Dtos
{
    public class ContactUsDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string UserId { get; set; }
        public string MobileNo { get; set; }
        public string Email { get; set; }
        public string Subject { get; set; }
        public string Description { get; set; }
        public int? CreatedBy { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? ReferenceId { get; set; }
        public bool? IsDeleted { get; set; }
        public int? EntityId { get; set; }
        public string FileUrl { get; set; }
        public string ComplainId { get; set; }
        public string Address { get; set; }
        public string Longitude { get; set; }
        public string Latitude { get; set; }

        public virtual User CreatedByNavigation { get; set; }
        public virtual Entity Entity { get; set; }
        public virtual User ModifiedByNavigation { get; set; }
        public virtual Reference Reference { get; set; }
    }
}
