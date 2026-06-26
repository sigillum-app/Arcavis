using Microsoft.AspNetCore.Mvc;
using Sigillum.Arcavis.Core.Application.Abstraction.Dispatcher;
using Sigillum.Arcavis.Core.Application.Features.Users.Commands.RegisterUser;
using Sigillum.Arcavis.Core.Application.Features.Users.Queries.GetAllUsers;

namespace Sigillum.Arcavis.Presentation.WebApi.V1.Controllers.Users;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly IAppCommandDispatcher _commandDispatcher;
    private readonly IAppQueryDispatcher _queryDispatcher;

    public UsersController(
        IAppCommandDispatcher commandDispatcher, 
        IAppQueryDispatcher queryDispatcher)
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
