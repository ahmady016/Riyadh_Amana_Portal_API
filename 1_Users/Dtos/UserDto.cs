using DB.Entities;

namespace Dtos;

public class UserDto
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public Gender Gender { get; set; }
    public DateTime? BirthDate { get; set; }
    public string NationalId { get; set; }
    public string Email { get; set; }
    public string Mobile { get; set; }
}
