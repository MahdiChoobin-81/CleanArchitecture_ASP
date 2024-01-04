using Application;
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
    private readonly ISender _sender;

    public UserController(IMediator sender)
    {
        _sender = sender;
    }


    [HttpGet("{id}")]
    public async Task<ActionResult<CustomGenericResult>> GetById(Guid id)
    {
        var result = await _sender.Send(new GetUserByIdQuery(new Id(id)));

        if (result.IsFailed)
            return NotFound(result);
        
        return Ok(result);
    }

    [HttpGet]
    public async Task<IEnumerable<User>> GetAll()
    {
        return await _sender.Send(new GetAllUsersQuery());
    }

    [HttpPost]
    public async Task<ActionResult<CustomGenericResult>> Post([FromBody] UserDto value)
     {
        var command = new AddUserCommand(
            value.FullName,
            value.Username,
            value.Password,
            value.Email
            );
        var result = await _sender.Send(command);

        if (result.IsFailed)
        {
            return BadRequest(result);
        }

        var userId = ObjectConverter<User>.Convert(result.Data).Id.Value;
        // var userId2 = ((User)result.Data).Id.Value;
        
        return CreatedAtAction(nameof(GetById), new{ id = userId}, result);
    }
    
    
    [HttpDelete("{id}")]
    public async Task<ActionResult<CustomGenericResult>> Delete(Guid id)
    {
        var result = await _sender.Send(new DeleteUserCommand(new Id(id)));

        if (result.IsFailed)
            return NotFound(result);
        
        return Ok(result);
    }
    
    
    [HttpPut("{id}")]
    public async Task<ActionResult<CustomGenericResult>> Put(Guid id,[FromBody] UserDto value)
    {
        var command = new UpdateUserCommand(
            new Id(id),
            value.FullName,
            value.Username,
            value.Password,
            value.Email
            );
    
        var result = await _sender.Send(command);

        if (result.IsFailed)
        {
            return BadRequest(result);
        }
    
        return Ok(result);
    }
    
    
    

    
    


    
}