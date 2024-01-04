using FluentResults;
using Movie_asp.Entities;
using Movie_asp.ValueObjects;
using Movie_asp.ValueObjects.Language;

namespace Movie_asp.Repositories;

public interface ILanguageRepository : IRepository<Language>
{
    Task<Result> Add(Language language);

    void Remove(Language language);

    Task<Result> Update(Language language, LanguageName currentLanguageName);

    Task<IEnumerable<Language>> GetAllAsync();
}