using Microsoft.AspNetCore.Mvc;
using IdentityServer.BusinessLogic.Services.Interfaces.User;
using IdentityServer.BusinessLogic.Services.Interfaces;
using IdentityServer.Common.Exceptions;
using IdentityServer.Common.Utility.Validator;
using IdentityServer.Entities.Dtos;

namespace IdentityServer.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ITokenProvider _tokenProvider;

        public AuthController(IUserService userService, ITokenProvider tokenProvider)
        {
            _userService = userService;
            _tokenProvider = tokenProvider;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _userService.GetUserByEmailAsync(dto.Email, cancellationToken);

                await _userService.VerifyPassword(user, dto.Password, cancellationToken);
                
                var token = _tokenProvider.Create(user);
                return Ok(new
                {
                    Token = token,
                    User = new UserDto
                    {
                        Id = user.Id,
                        Email = user.Email,
                        Name = user.Name
                    }
                });
            }
            catch (InvalidInputException invalidInputException)
            {
                return BadRequest(invalidInputException.Message);
            }
            catch (EntityNullException entityNullException)
            {
                return BadRequest(entityNullException.Message);
            }
            catch (InvalidPasswordException invalidPasswordException)
            {
                return Unauthorized(invalidPasswordException.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] CreateUserDto dto, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _userService.CreateUserAsnyc(dto, cancellationToken);

                //for automatic login after registration
                var token = _tokenProvider.Create(user);

                return Ok(new
                {
                    Token = token,
                    User = user
                });
            }
            catch (InvalidInputException invalidInputException)
            {
                return BadRequest(invalidInputException.Message);
            }
            catch (EntityAlreadyExistsException entityAlreadyExistsException)
            {
                return Conflict(entityAlreadyExistsException.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

    }
}
