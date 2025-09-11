using GeminiBack.Dtos;
using Mscc.GenerativeAI;

namespace GeminiBack.Repository;

public interface IPromptStreamRepository
{
    IAsyncEnumerable<string> BasicPromptStream(BasicPromptDto promptDto);
    
}