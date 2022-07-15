using DB.Entities;
using System.ComponentModel.DataAnnotations;

namespace Dtos;

public class CreateUserInput
{
    [Required]
    [StringLength(30)]
    public string FirstName { get; set; }
    
    [Required]
    [StringLength(70)]
    public string LastName { get; set; }

    [Required]
    public Gender Gender { get; set; }
    
    public DateTime? BirthDate { get; set; }

    [Required]
    [StringLength(15)]
    public string NationalId { get; set; }
    
    [Required]
    [StringLength(100)]
    public string Email { get; set; }

    [Required]
    [StringLength(50)]
    public string Password { get; set; }

    [Required]
    [StringLength(20)]
    public string Mobile { get; set; }
}
