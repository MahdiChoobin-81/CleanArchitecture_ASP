using Application.Dto.Results;
using FluentResults;
using MediatR;
using Movie_asp.ValueObjects;

namespace Application.Users.Commands;

public record DeleteUserCommand(UserId UserId) : IRequest<UserResultDto>;