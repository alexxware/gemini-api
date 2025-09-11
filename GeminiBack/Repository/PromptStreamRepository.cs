using GeminiBack.Dtos;
using GeminiBack.Models;
using Microsoft.Extensions.Options;
using Mscc.GenerativeAI;

namespace GeminiBack.Repository;

public class PromptStreamRepository: IPromptStreamRepository
{
    private readonly string _apiKey;

    public PromptStreamRepository(IOptions<ApiKeysOptions> options)
    {
        _apiKey = options.Value.GoogleGemini;
    }
    
    public async IAsyncEnumerable<string> BasicPromptStream(BasicPromptDto promptDto)
    {
        var googleApi = new GoogleAI(apiKey: _apiKey);
        var model = googleApi.GenerativeModel(model: Model.Gemini25Flash);
        
        //generamos el contenido
        await foreach (var response in model.GenerateContentStream(promptDto.Prompt))
        {
            foreach (var candidate in response.Candidates)
            {
                foreach (var part in candidate.Content.Parts)
                {
                    if (!string.IsNullOrEmpty(part.Text))
                    {
                        yield return part.Text;
                    }
                }
            }
        }
        
    }
}