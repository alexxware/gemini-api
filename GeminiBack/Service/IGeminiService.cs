using GeminiBack.Dtos;
using Microsoft.AspNetCore.Mvc;
using Mscc.GenerativeAI;

namespace GeminiBack.Service;

public interface IGeminiService
{
    Task<string> BasicPrompt(BasicPromptDto promptDto);
    IAsyncEnumerable<string> BasicPromptStream(BasicPromptDto promptDto);
    Task<string> AdvancedPrompt(AdvancedPromptDto promptDto);
}