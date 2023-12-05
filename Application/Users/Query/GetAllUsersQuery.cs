using System.Collections;
using MediatR;
using Movie_asp.Entities;

namespace Application.Users.Query;

public record GetAllUsersQuery() : IRequest<IEnumerable<User>>;