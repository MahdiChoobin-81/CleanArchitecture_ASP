using Application.Data;
using FluentResults;
using Microsoft.EntityFrameworkCore;
using Movie_asp.Entities;
using Movie_asp.Repositories;
using Movie_asp.ValueObjects;
using Movie_asp.ValueObjects.Language;

namespace Infrastructure.Repositories;

public class LanguageRepository : ILanguageRepository
{
    private readonly IApplicationDbContext _context;

    public LanguageRepository(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Language?> FindByIdAsync(Id id)
    {
        return await _context.Languages.FirstOrDefaultAsync(l => l.Id == id);
    }


    public async Task<Result> Add(Language language)
    {
        IQueryable<Language> languages = _context.Languages.AsQueryable();

        var isLanguageNameUnique = await languages
            .Where(l => l.LanguageName == language.LanguageName).FirstOrDefaultAsync();
        
        if (isLanguageNameUnique is not null)
        {
            return Result.Fail("Language already exist.");
        }
        _context.Languages.Add(language);
        return Result.Ok();
    }

    public void Remove(Language language)
    {
        _context.Languages.Remove(language);
    }

    public async Task<Result> Update(Language language, LanguageName currentLanguageName)
    {
        IQueryable<Language> languages = _context.Languages.AsQueryable();

        var isLanguageNameUnique = await languages
            .Where(l =>
                l.LanguageName != currentLanguageName &&
                l.LanguageName == language.LanguageName)
            .FirstOrDefaultAsync();
            
        if (isLanguageNameUnique is not null)
        {
            return Result.Fail("Language already exist.");
        }
        
        _context.Languages.Update(language);
        return Result.Ok();
    }

    public async Task<IEnumerable<Language>> GetAllAsync()
    {
        return await _context.Languages.ToListAsync();
    }
}