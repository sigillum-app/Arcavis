using Microsoft.AspNetCore.Mvc;
using Sigillum.Arcavis.Core.Application.Contracts.Dispatcher;
using Sigillum.Arcavis.Core.Application.Features.Users.Commands.RegisterUser;

namespace Sigillum.Arcavis.Presentation.WebApi.V1.Controllers.Users;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly ICommandDispatcher _commandDispatcher;

    public UsersController(ICommandDispatcher commandDispatcher)
    {
        _commandDispatcher = commandDispatcher;
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterUserCommand command)
    {
        var id = await _commandDispatcher.SendAsync(command);
        return Ok(id);
    }
}
