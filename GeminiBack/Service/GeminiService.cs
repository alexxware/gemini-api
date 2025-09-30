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
    private IPromptStreamRepository _promptStreamRepository;

    public GeminiService(IGeminiRepository repository, IPromptStreamRepository promptStreamRepository)
    {
        _repository = repository;
        _promptStreamRepository = promptStreamRepository;
    }
    public async Task<string> BasicPrompt(BasicPromptDto promptDto)
    {
        return await _repository.BasicPrompt(promptDto);
    }

    public async IAsyncEnumerable<string> BasicPromptStream(BasicPromptDto promptDto)
    {
        await foreach (var response in _promptStreamRepository.BasicPromptStream(promptDto))
        {
            yield return response;
        }
    }

    public Task<string> AdvancedPrompt(string prompt, List<IFormFile?> files)
    {
        throw new NotImplementedException();
    }
}