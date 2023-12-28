// using Application.Data;
// using Microsoft.EntityFrameworkCore;
// using Movie_asp.Entities;
// using Movie_asp.Repositories;
// using Movie_asp.ValueObjects;
//
// namespace Infrastructure.Repositories;
//
// public class LanguageRepository : ILanguageRepository
// {
//     
//     private readonly IApplicationDbContext _context;
//
//     public LanguageRepository(IApplicationDbContext context)
//     {
//         _context = context;
//     }
//
//     public async Task<Language?> FindByIdAsync(Id id)
//     {
//         var language = await _context.Languages.FirstOrDefaultAsync(c => c.Id == id);
//         return language;
//     }
// }