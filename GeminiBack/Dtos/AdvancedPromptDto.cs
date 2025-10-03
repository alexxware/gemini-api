namespace GeminiBack.Dtos;

public class AdvancedPromptDto
{
    public string prompt { get; set; }
    public List<IFormFile> files { get; set; } = new  List<IFormFile>();
}