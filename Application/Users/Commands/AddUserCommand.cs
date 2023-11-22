using Domain.Entities;
using FluentResults;
using MediatR;
using Movie_asp.Entities;

namespace Application.Users.Commands;

public record AddUserCommand(
    string FullName,
    string Username,
    string Password,
    string Email) : IRequest<Result<User?>>;
