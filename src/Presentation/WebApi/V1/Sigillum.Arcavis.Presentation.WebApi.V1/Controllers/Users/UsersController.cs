using Mediator;
using Microsoft.AspNetCore.Mvc;
using Sigillum.Arcavis.Core.Application.Features.Users.Commands.RegisterUser;
using Sigillum.Arcavis.Core.Application.Features.Users.Queries.GetAllUsers;

namespace Sigillum.Arcavis.Presentation.WebApi.V1.Controllers.Users;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly IMediator _mediator;

    public UsersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterUserCommand command)
    {
        var id = await _mediator.Send(command);
        return Ok(id);
    }

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] GetAllUsersQuery query)
    {
        var users = await _mediator.Send(query);
        return Ok(users);
    }
}
