using DB.Entities;

namespace Dtos;

public class AppPageDto
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string Url { get; set; }

    public List<PageKey> Keys { get; set; }
}
