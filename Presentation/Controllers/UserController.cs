using Application.Data;
using Application.Dto.Results;
using Application.Users.Commands;
using Application.Users.Query;
using FluentResults;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Movie_asp.Entities;
using Movie_asp.Repositories;
using Movie_asp.ValueObjects;
using Presentation.Dto;

namespace Presentation.Controllers;

    [Route("api/[controller]")]
    [ApiController]

public class UserController : Controller
{
    private IMediator _mediator;

    public UserController(IMediator mediator)
    {
        _mediator = mediator;
    }


    [HttpGet("{id}")]
    public async Task<ActionResult<UserResultDto>> GetById(Guid id)
    {
        var result = await _mediator.Send(new GetUserByIdQuery(new Id(id)));

        if (result.IsFailed)
        {
            return NotFound(result);
        }
        return Ok(result);
    }

    [HttpGet]
    public async Task<IEnumerable<User>> GetAll()
    {
        return await _mediator.Send(new GetAllUsersQuery());
    }

    [HttpPost]
    public async Task<ActionResult<UserResultDto>> Post([FromBody] AddUserDto value)
     {
        var command = new AddUserCommand(
            value.FullName,
            value.Username,
            value.Password,
            value.Email
            );
        UserResultDto result = await _mediator.Send(command);

        if (result.IsFailed)
        {
            return BadRequest(result);
        }
        return CreatedAtAction(nameof(GetById), new{ id = result.User.Id.Value}, result.User);
    }
    
    
    [HttpDelete("{id}")]
    public async Task<ActionResult<UserResultDto>> Delete(Guid id)
    {
        var result = await _mediator.Send(new DeleteUserCommand(new Id(id)));

        if (result.IsFailed)
        {
            return NotFound(result);
        }
        
        return Ok(result);
    }
    
    
    [HttpPut("{id}")]
    public async Task<ActionResult<UserResultDto>> Put(Guid id,[FromBody] UpdateUserDto value)
    {
        var command = new UpdateUserCommand(
            new Id(id),
            value.FullName,
            value.Username,
            value.Password,
            value.Email
            );
    
        var result = await _mediator.Send(command);

        if (result.IsFailed)
        {
            return BadRequest(result);
        }
    
        return Ok(result);
    }
    
    
    

    
    


    
}