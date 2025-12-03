using EbookBackend.API.Dto;
using EbookBackend.API.Model;
using EbookBackend.Application.Dto;
using EbookBackend.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EbookBackend.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public async Task<ActionResult<ApiResponse<AuthResultDto>>> Login(LoginDto login)
        {
            try
            {
                var result = await _authService.LoginAsync(login.Email, login.Password);
                return Ok(new ApiResponse<AuthResultDto>
                {
                    Success = true,
                    Data = result,
                    Message = "Login succesful"
                });
            }
            catch(Exception ex)
            {
                return BadRequest(new ApiError
                {
                    Message = "An error occurred during login",
                    Detail = ex.Message
                });
            }
        }

    }
}
