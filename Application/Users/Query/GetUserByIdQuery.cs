using Application.Dto.Results;
using FluentResults;
using MediatR;
using Movie_asp.Entities;
using Movie_asp.ValueObjects;

namespace Application.Users.Query;

public record GetUserByIdQuery(Id Id) : IRequest<CustomGenericResult>;