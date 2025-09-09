using GeminiBack.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace GeminiBack.Service;

public interface IGeminiService
{
    Task<string> BasicPrompt(BasicPromptDto promptDto);
}