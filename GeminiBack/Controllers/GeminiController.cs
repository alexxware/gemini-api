using FluentValidation;
using GeminiBack.Dtos;
using GeminiBack.Models;
using GeminiBack.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Mscc.GenerativeAI;

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
        var validationsResult = await _validatorPrompt.ValidateAsync(promptDto);
        if (!validationsResult.IsValid)
        {
            throw new ArgumentException("El prompt no puede estar vacio");
        }
        
        Response.ContentType = "text/plain; charset=utf-8";

        await foreach (var response in _geminiService.BasicPromptStream(promptDto))
        {
            await Response.WriteAsync(response);
            await Response.Body.FlushAsync();
        }

        return new EmptyResult();
    }
}