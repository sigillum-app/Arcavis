using Microsoft.AspNetCore.Mvc;
using Sigillum.Arcavis.Core.Application.Abstraction.Dispatcher;
using Sigillum.Arcavis.Core.Application.Features.Users.Commands.RegisterUser;
using Sigillum.Arcavis.Core.Application.Features.Users.Queries.GetAllUsers;

namespace Sigillum.Arcavis.Presentation.WebApi.V1.Controllers.Users;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly ICommandDispatcher _commandDispatcher;
    private readonly IQueryDispatcher _queryDispatcher;

    public UsersController(
        ICommandDispatcher commandDispatcher, 
        IQueryDispatcher queryDispatcher)
    {
        _commandDispatcher = commandDispatcher;
        _queryDispatcher = queryDispatcher;
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterUserCommand command)
    {
        var id = await _commandDispatcher.SendAsync(command);
        return Ok(id);
    }

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] GetAllUsersQuery query)
    {
        var users = await _queryDispatcher.SendAsync(query);
        return Ok(users);
    }
}
