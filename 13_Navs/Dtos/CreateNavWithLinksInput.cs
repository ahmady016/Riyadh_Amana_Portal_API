namespace Dtos;

public class CreateNavWithLinksInput : CreateNavInput
{
    public List<CreateNavLinkInput> Links { get; set; }
}
