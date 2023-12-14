using Movie_asp.Entities;
using Movie_asp.ValueObjects;

namespace Movie_asp.Repositories;

public interface ILanguageRepository
{
    void Add(Language language);

    void Remove(Language language);

    void Update(Language language);

    Task<Language?> GetByIdAsync(Id id);

    Task<IEnumerable<Language>> GetAllAsync();
}