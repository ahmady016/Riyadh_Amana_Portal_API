namespace Dtos;

public class CreateAppPageWithKeysInput : CreateAppPageInput
{
    public List<CreatePageKeyInput> Keys { get; set; }
}
