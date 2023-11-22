using Application.Data;
using Application.Users.Commands;
using Domain.Entities;
using Domain.Repositories;
using FluentResults;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Movie_asp.Entities;
using Presentation.DTO;

namespace Presentation.Controllers;

    [Route("api/[controller]")]
    [ApiController]

public class UserController
{
    
    
    
    private IMediator _mediator;

    public UserController(IMediator mediator, IUserRepository userRepository, IUnitOfWork unitOfWork)
    {
        _mediator = mediator;
    }


    [HttpPost]
    public async Task<Result<User?>> Create([FromBody] AddUserDTO value)
    {
        var command = new AddUserCommand(
            value.FullName,
            value.Username,
            value.Password,
            value.Email
            );
         await _mediator.Send(command);


        return  null;
    }

    
    


    
}