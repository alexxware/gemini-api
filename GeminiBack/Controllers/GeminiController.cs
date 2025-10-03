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
    public async Task<string> BasicPrompt(BasicPromptDto promptDto)
    {
        var validationsResult = await _validatorPrompt.ValidateAsync(promptDto);
        if (!validationsResult.IsValid)
        {
            return string.Empty;
        }
        
        var res = _geminiService.BasicPrompt(promptDto).Result;
        return res;
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

    [HttpPost("advancedPrompt")]
    public async Task<IActionResult> AdvancedPrompt([FromForm] AdvancedPromptDto promptDto)
    {
        if(string.IsNullOrEmpty(promptDto.prompt)) return BadRequest("El prompt no puede estar vacio");
        
        Response.ContentType = "text/plain; charset=utf-8";
        
        foreach (var file in promptDto.files)
        {
            // Ejemplo: console.log en el servidor para ver que llegan
            Console.WriteLine($"Received file: {file.FileName} with size {file.Length} bytes.");
        
            // Aquí podrías guardar el archivo, como hicimos con los productos.
            // O procesar la imagen/archivo en memoria.
        }
        
        return Ok(new 
        {
            Message = "Prompt recibido exitosamente y archivos procesados.",
            Prompt = promptDto.prompt,
            FilesReceived = promptDto.files.Count
        });
    }
}