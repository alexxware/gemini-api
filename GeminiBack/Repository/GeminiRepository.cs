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

    public async Task<string> AdvancedPrompt(AdvancedPromptDto promptDto)
    {
        var googleAi = new GoogleAI(apiKey: _apiKey);
        var model = googleAi.GenerativeModel(model: Model.Gemini25Flash);
        
        var parts = new List<Part>();
        if (promptDto.files.Any())
        {
            foreach (var file in promptDto.files)
            {
                // Convertir IFormFile a byte[]
                byte[] fileBytes;
                using (var stream = new MemoryStream())
                {
                    await file.CopyToAsync(stream);
                    fileBytes = stream.ToArray();
                }

                var inlineData = new InlineData
                {
                    Data = Convert.ToBase64String(fileBytes),
                    MimeType = file.ContentType
                };

                var filePart = new Part { InlineData = inlineData };

                parts.Add(filePart);
            }
        }
        
        //parts.Add(Part.FromText(promptDto.prompt));

        return "ok";
    }
}