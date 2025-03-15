using IdentityServer.BusinessLogic.Services.Interfaces.User;
using IdentityServer.Common.Exceptions;
using IdentityServer.Entities.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace IdentityServer.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserDto dto, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _userService.CreateUserAsnyc(dto, cancellationToken);
                return CreatedAtAction(nameof(GetUserById), new { id = user.Id }, user);
            }
            catch (EntityAlreadyExistsException entityAlreadyExistsException)
            {
                return Conflict(entityAlreadyExistsException.Message);
            }
            catch (InvalidInputException invalidInputException)
            {
                return BadRequest(invalidInputException.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
            
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetUserById(Guid id, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _userService.GetUserByIdAsync(id, cancellationToken);
                return Ok(user);
            }
            catch (EntityNullException entityNullException)
            {
                return NotFound(entityNullException.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
            
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateUser(Guid id, [FromBody] UpdateUserDto dto,  CancellationToken cancellationToken)
        {
            try
            {
                var user = await _userService.UpdateUserAsync(id, dto, cancellationToken);
                return Ok(user);
            }
            catch (EntityNullException entityNullException)
            {
                return NotFound(entityNullException.Message);
            }
            catch (InvalidInputException invalidInputException)
            {
                return BadRequest(invalidInputException.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
            
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteUser(Guid id, CancellationToken cancellationToken)
        {
            try
            {
                await _userService.DeleteUserAsync(id, cancellationToken);
                return NoContent();
            }
            catch (EntityNullException entityNullException)
            {
                return NotFound(entityNullException.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
            
        }

        [HttpGet("email/{email}")]
        public async Task<IActionResult> GetUserByEmail(string email, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _userService.GetUserByEmailAsync(email, cancellationToken);
                return Ok(user);
            }
            catch (EntityNullException entityNullException)
            {
                return NotFound(entityNullException.Message);
            }
            catch (InvalidInputException invalidInputException)
            {
                return BadRequest(invalidInputException.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

    }
}
