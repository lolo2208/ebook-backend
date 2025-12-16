using EbookBackend.API.Model;
using EbookBackend.Application.Dto;
using EbookBackend.Application.Interfaces;
using EbookBackend.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EbookBackend.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService) {
            _roleService = roleService;
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("GetRoles")]
        public async Task<ActionResult<ApiResponse<List<RoleDto>>>> GetRoles()
        {
            try
            {
                var roles = await _roleService.GetAllRolesAsync();

                return Ok(new ApiResponse<IEnumerable<RoleDto>>
                {
                    Success = true,
                    Data = roles,
                    Message = "Roles listed"
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiError
                {
                    Message = "An error occurred during getting roles",
                    Detail = ex.Message
                });
            }
        }

        [HttpPost]
        [Authorize(Roles="Admin")]
        [Route("saveRole")]
        public async Task<ActionResult<ApiResponse<RoleDto>>> SaveRole(RoleDto roleDto)
        {
            try
            {
                var role = await _roleService.SaveRole(roleDto);
                return Ok(new ApiResponse<RoleDto>
                {
                    Success = true,
                    Data = role,
                    Message = "Role registered correctly"
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiError
                {
                    Message = "An error occurred during role registration",
                    Detail = ex.Message
                });
            }
        }

        [HttpDelete]
        [Authorize(Roles="Admin")]
        [Route("deleteRole/{idRole}")]
        public async Task<ActionResult<ApiResponse<RoleDto>>> DeleteRole(int idRole)
        {
            try
            {
                var role = await _roleService.DeleteRole(idRole);
                return Ok(new ApiResponse<RoleDto>
                {
                    Success = true,
                    Data = role,
                    Message = "Role deleted correctly"
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiError
                {
                    Message = "An error occurred during role deleting",
                    Detail = ex.Message
                });
            }
        }
    }
}
