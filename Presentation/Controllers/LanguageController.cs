using Application.Data;
using Application.Dto.Results;
using FluentResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Movie_asp.Entities;
using Movie_asp.ValueObjects;
using Movie_asp.ValueObjects.Language;

namespace Presentation.Controllers;

[Route("api/[controller]")]
[ApiController]

public class LanguageController : Controller
{

    private readonly IApplicationDbContext _applicationDbContext;
    private readonly IUnitOfWork _unitOfWork;

    public LanguageController(IApplicationDbContext applicationDbContext, IUnitOfWork unitOfWork)
    {
        _applicationDbContext = applicationDbContext;
        _unitOfWork = unitOfWork;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<LanguageResultDto>> GetById(Guid id)
    {
        var result = await _applicationDbContext.Languages
            .FirstOrDefaultAsync(l => l.Id == new Id(id));

        if (result is null)
        {
            return NotFound(Result.Fail("There's no Language with This Id : " + id)
                .ToLanguageResultDto(null));
        }

        return Ok(Result.Ok().ToLanguageResultDto(result));
    }


    [HttpGet]
    public async Task<ActionResult<IEnumerable<Language>>> GetAllAsync()
    {
        var result = await _applicationDbContext.Languages
            .ToListAsync();

        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<LanguageResultDto>> Post([FromBody] string languageName)
    {
        var name = LanguageName.Create(languageName);

        if (name.IsFailed)
            return BadRequest(name.ToResult().ToLanguageResultDto(null));

        var language = new Language(new Id(Guid.NewGuid()), name.Value!);

        _applicationDbContext.Languages.Add(language);

        await _unitOfWork.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById), new { id = language.Id }, language);


    }


    [HttpDelete("{id}")]
    public async Task<ActionResult<LanguageResultDto>> Delete(Guid id)
    {
        var language = await _applicationDbContext
            .Languages.
            FirstOrDefaultAsync(l => l.Id == new Id(id));

        if (language is null)
        {
            return NotFound(Result.Fail("There's no Language with This Id : " + id));
        }

        _applicationDbContext.Languages.Remove(language);
        await _unitOfWork.SaveChangesAsync();
        
        return Ok(language);

    }


    [HttpPut("{id}")]
    public async Task<ActionResult<LanguageResultDto>> Put(Guid id, [FromBody] string languageName)
    {
        var language = await _applicationDbContext
            .Languages.
            FirstOrDefaultAsync(l => l.Id == new Id(id));

        if (language is null)
        {
            return NotFound(Result.Fail("There's no Language with This Id : " + id)
                .ToLanguageResultDto(null));
        }

        var name = LanguageName.Create(languageName);
        if (name.IsFailed)
        {
            return BadRequest(name.ToResult().ToLanguageResultDto(null));
        }
        language.Update(name.Value!);
        

        _applicationDbContext.Languages.Update(language);
        await _unitOfWork.SaveChangesAsync();
        return Ok(language);

    }
    
    
}