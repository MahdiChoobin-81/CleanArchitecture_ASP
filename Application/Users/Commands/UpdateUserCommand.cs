using Application.Dto.Results;
using FluentResults;
using MediatR;
using Movie_asp.Entities;
using Movie_asp.ValueObjects;

namespace Application.Users.Commands;

public record UpdateUserCommand(
    Id Id,
    string FullName,
    string Username,
    string Password,
    string Email
    ) : IRequest<CustomGenericResult>;

    