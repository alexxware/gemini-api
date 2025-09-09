using GeminiBack.Dtos;
using GeminiBack.Models;
using GeminiBack.Repository;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Mscc.GenerativeAI;

namespace GeminiBack.Service;

public class GeminiService: IGeminiService
{
    private IGeminiRepository _repository;

    public GeminiService(IGeminiRepository repository)
    {
        _repository = repository;
    }
    public async Task<string> BasicPrompt(BasicPromptDto promptDto)
    {
        return await _repository.BasicPrompt(promptDto);
    }

    public async Task<string> BasicPromptStream(BasicPromptDto promptDto)
    {
        return "Hola mundo desde el service de stream";
    }
}