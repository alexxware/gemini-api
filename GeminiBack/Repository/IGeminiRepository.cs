using GeminiBack.Dtos;

namespace GeminiBack.Repository;

public interface IGeminiRepository
{
    Task<string> BasicPrompt(BasicPromptDto promptDto);
    Task<string> AdvancedPrompt(string prompt, List<IFormFile?> files);
}