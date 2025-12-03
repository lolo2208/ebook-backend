using EbookBackend.API.Model;
using EbookBackend.Application.Dto;
using EbookBackend.Application.Interfaces;
using EbookBackend.Application.Services;
using EbookBackend.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EbookBackend.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        [Route("registerUser")]
        public async Task<ActionResult<ApiResponse<UserDto>>> RegisterUser(UserRegisterRequestDto userRegister)
        {
            try
            {
                var result = await _userService.TransactionRegisterUser(userRegister);


                return Ok(new ApiResponse<UserDto>
                {
                    Success = true,
                    Data = result,
                    Message = "User registered correctly"
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiError
                {
                    Message = "An error occurred during user registration",
                    Detail = ex.Message
                });
            }
        }
    }
}
