using Application.Data;
using Application.Dto.Results;
using Application.Validations;
using FluentResults;
using Microsoft.AspNetCore.Mvc;
using Movie_asp.Entities;
using Movie_asp.Repositories;
using Movie_asp.ValueObjects;
using Movie_asp.ValueObjects.Language;

namespace Presentation.Controllers;

[Route("api/[controller]")]
[ApiController]

public class LanguageController : Controller

{
    
    private readonly ILanguageRepository _languageRepository;
    private readonly IUnitOfWork _unitOfWork;

    public LanguageController(IUnitOfWork unitOfWork, ILanguageRepository languageRepository)
    {
        _unitOfWork = unitOfWork;
        _languageRepository = languageRepository;
    }


    [HttpGet("{id}")]
    public async Task<ActionResult<CustomGenericResult>> GetById(Guid id)
    {
        var result = await FindEntityRecordById<Language>.Find(
            _languageRepository, new Id(id));

        if (result.IsFailed)
        {
            return result.ToResult().ToCustomGenericResult(null, Movie_asp.StatusCode.NotFound);
        }
        var language = result.Value;
        return Result.Ok().ToCustomGenericResult(language, Movie_asp.StatusCode.Ok);
        
    }

    [HttpGet]
    public async Task<IEnumerable<Language>> GetAllAsync()
    {
        return await _languageRepository.GetAllAsync();
    }

    [HttpPost]
    public async Task<ActionResult<CustomGenericResult>> Post([FromBody] string languageName)
    {
        var name = LanguageName.Create(languageName);

        if (name.IsFailed)
        {
            return BadRequest(name.ToResult()
                .ToCustomGenericResult(null, Movie_asp.StatusCode.BadRequest));
        }


        var language = new Language(
            new Id(Guid.NewGuid()),
            name.Value);
        
        var result = await _languageRepository.Add(language);
        if (result.IsFailed)
            return result.ToCustomGenericResult(null, Movie_asp.StatusCode.BadRequest);
        await _unitOfWork.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById), new { id = language.Id.Value }, language);

    }


    [HttpDelete("{id}")]
    public async Task<ActionResult<CustomGenericResult>> Delete(Guid id)
    {
        var result = await FindEntityRecordById<Language>.Find(
            _languageRepository, new Id(id));

        if (result.IsFailed)
        {
            return result.ToResult().ToCustomGenericResult(null, Movie_asp.StatusCode.NotFound);
        }
        var language = result.Value;
        
        _languageRepository.Remove(language);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok().ToCustomGenericResult(language, Movie_asp.StatusCode.Ok);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<CustomGenericResult>> Put(Guid id, [FromBody] string languageName)
    
    {
        var findLanguage = await FindEntityRecordById<Language>.Find(
            _languageRepository, new Id(id));

        if (findLanguage.IsFailed)
        {
            return findLanguage.ToResult()
                .ToCustomGenericResult(null, Movie_asp.StatusCode.NotFound);
        }
        var language = findLanguage.Value;
        var currentLanguageName = language.LanguageName;
        var name = LanguageName.Create(languageName);

        if (name.IsFailed)
            return BadRequest(name.ToResult()
                .ToCustomGenericResult(null, Movie_asp.StatusCode.BadRequest));
        
        language.Update(name.Value);

        var result = await _languageRepository.Update(language, currentLanguageName);
        if (result.IsFailed)
            return result.ToCustomGenericResult(null, Movie_asp.StatusCode.BadRequest);
        
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok().ToCustomGenericResult(language, Movie_asp.StatusCode.Ok);
        
    }






}