using Application.Dto.Results;
using FluentResults;
using MediatR;
using Movie_asp.Entities;
using Movie_asp.ValueObjects;

namespace Application.Users.Commands;

public record UpdateUserCommand(
    UserId UserId,
    string FullName,
    string Username,
    string Password,
    string Email
    ) : IRequest<UserResultDto>;

    