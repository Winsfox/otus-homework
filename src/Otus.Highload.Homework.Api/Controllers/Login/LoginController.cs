using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Otus.Highload.Homework.Api.Controllers.Login.Contracts;
using Otus.Highload.Homework.Api.Cryptography;
using Otus.Highload.Homework.Users;

namespace Otus.Highload.Homework.Api.Controllers.Login;

[ApiController]
public class LoginController : ControllerBase
{
    private readonly IUserRepository _userRepository;

    public LoginController(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    [HttpPost("login")]
    [ProducesResponseType(typeof(LoginsResp), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Login(LoginReq req, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetAsync(req.Id, cancellationToken);
        if (user?.Password == HashGetter.Get(req.Password))
        {
            var token = Guid.NewGuid().ToString();
            return Ok(new LoginsResp(token));
        }

        return NotFound();
    }
}
