using GeminiBack.Dtos;
using GeminiBack.Models;
using Microsoft.Extensions.Options;
using Mscc.GenerativeAI;

namespace GeminiBack.Repository;

public class GeminiRepository: IGeminiRepository
{
    private readonly string _apiKey;

    public GeminiRepository(IOptions<ApiKeysOptions> options)
    {
        _apiKey = options.Value.GoogleGemini;
    }
    
    public async Task<string> BasicPrompt(BasicPromptDto promptDto)
    {
        var googleApi = new GoogleAI(apiKey: _apiKey);
        var model = googleApi.GenerativeModel(model: Model.Gemini25Flash);
        
        //generamos el contenido
        var response = await model.GenerateContent(promptDto.Prompt);
        
        return response.Text!;
    }
}