using FasgionGrid.UserService.Models.Dtos;
using FasgionGrid.UserService.Services;
using FashionGrid.UserService.Models.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FasgionGrid.UserService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        protected ResponseDto _responseDto;

        public UsersController(IUserService userService)
        {
            _userService = userService;
            _responseDto = new();
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegistrationRequestDto registrationRequestDto)
        {
            //Registering a user
            try
            {
                var errorMessage = await _userService.Register(registrationRequestDto);
                if (!string.IsNullOrEmpty(errorMessage))
                {
                    _responseDto.IsSuccess = false;
                    _responseDto.Message = errorMessage;
                    return BadRequest(_responseDto);
                }

                return Ok("User registered successfully");
            }
            catch (Exception e)
            {

                return StatusCode(500, new { Message = "An exception occurred while resigtering user" });
            }

        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequestDto)
        {
            try
            {
                var loginresponse = await _userService.Login(loginRequestDto);
                if (loginresponse.User == null)
                {

                    _responseDto.IsSuccess = false;
                    _responseDto.Message = "An error occured while logging in";
                    return BadRequest(_responseDto);
                }


                return Ok(loginresponse);
            }
            catch (Exception e)
            {

                return StatusCode(500, new { Message = "An exception occured while logging in" });
            }
        }

        [HttpPost("AssignRole")]
        public async Task<IActionResult> AssignRole([FromBody] RegistrationRequestDto registrationRequestDto)
        {

            try
            {
                var assignrolesuccess = await _userService.AssignRole(registrationRequestDto.Email, registrationRequestDto.Role.ToUpper());
                if (!assignrolesuccess)
                {
                    _responseDto.IsSuccess = false;
                    _responseDto.Message = "An error occured while assigning a role";
                    return BadRequest(_responseDto);
                    
                }

                return Ok("Role assigned successfully");
            }
            catch (Exception e)
            {

                return StatusCode(500, new { Message = "An exception occured while assigning a role" });
            }
        }
    }
}
