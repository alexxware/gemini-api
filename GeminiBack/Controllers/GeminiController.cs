using FluentValidation;
using GeminiBack.Dtos;
using GeminiBack.Models;
using GeminiBack.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace GeminiBack.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GeminiController : ControllerBase
{
    private IGeminiService _geminiService;
    private IValidator<BasicPromptDto> _validatorPrompt;

    public GeminiController( 
        IGeminiService geminiService,
        IValidator<BasicPromptDto> validatorPrompt)
    {
        _geminiService = geminiService;
        _validatorPrompt = validatorPrompt;
    }

    [HttpPost("basicPrompt")]
    public async Task<IActionResult> BasicPrompt(BasicPromptDto promptDto)
    {
        var validationsResult = await _validatorPrompt.ValidateAsync(promptDto);
        if (!validationsResult.IsValid)
        {
            return BadRequest(validationsResult.Errors);
        }
        
        var res = _geminiService.BasicPrompt(promptDto).Result;
        return Ok(res);
    }

    [HttpPost("basicPromptStream")]
    public async Task<IActionResult> BasicPromptStream(BasicPromptDto promptDto)
    {
        return Ok("Hola mundo como stream");
    }
}