using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Otus.Highload.Homework.Api.Controllers.Users.Contracts;
using Otus.Highload.Homework.Api.Controllers.Users.Converters;
using Otus.Highload.Homework.Users;

namespace Otus.Highload.Homework.Api.Controllers.Users;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserRepository _userRepository;

    public UserController(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    [HttpPost("register")]
    [ProducesResponseType(typeof(RegisterResp), StatusCodes.Status200OK)]
    public async Task<RegisterResp> Register(RegisterReq req, CancellationToken cancellationToken)
    {
        var user = req.ToUser();
        var id = await _userRepository.AddAsync(user, cancellationToken);
        return new RegisterResp(id);
    }

    [HttpGet("get/{id}")]
    [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Register([FromRoute] long id, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetAsync(id, cancellationToken);
        return user is not null
            ? Ok(user.ToUserDto())
            : NotFound();
    }

}
