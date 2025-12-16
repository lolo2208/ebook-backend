using AutoMapper;
using EbookBackend.Application.Dto;
using EbookBackend.Application.Interfaces;
using EbookBackend.Domain.Entities;
using EbookBackend.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace EbookBackend.Application.Services
{
    public class RoleService : IRoleService 
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IUserContextService _userContextService;

        public RoleService(IUnitOfWork unitOfWork, IMapper mapper, IUserContextService userContextService) {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userContextService = userContextService;
        }

        public async Task<RoleDto> DeleteRole(int idRole)
        {
            await _unitOfWork.BeginTransactionAsync();

            try
            {
                Role? role = await _unitOfWork.Roles.GetByIdAsync(idRole);
                if(role == null)
                {
                    throw new Exception("Role not founded");
                }else
                {
                    _unitOfWork.Roles.Delete(role);

                    await _unitOfWork.AuditLogs.AddAsync(new AuditLog
                    {
                        Action = "DELETE",
                        TableName = "Roles",
                        CreatedAt = DateTime.UtcNow,
                        RecordId = $"Role {idRole}",
                        Description = "Role delete"
                    });

                    await _unitOfWork.SaveChangesAsync();
                    await _unitOfWork.CommitAsync();

                    return _mapper.Map<RoleDto>(role);
                }
            }catch(Exception ex)
            {
                await _unitOfWork.RollbackAsync();
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<RoleDto>> GetAllRolesAsync()
        {
            try
            {
                var roles = await _unitOfWork.Roles.GetAllAsync();
                var rolesDto = roles.Select(_mapper.Map<RoleDto>).ToList();
                return rolesDto;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<RoleDto> SaveRole(RoleDto roleDto)
        {
            // Init transaction
            await _unitOfWork.BeginTransactionAsync();

            try
            {
                Role roleObj;

                if (roleDto.IdRole.HasValue && roleDto.IdRole.Value > 0)
                {
                    //Update role
                    var currentRole = await _unitOfWork.Roles.GetByIdAsync(roleDto.IdRole.Value);
                    if (currentRole == null)
                        throw new Exception("El rol no existe");

                    var oldValues = JsonSerializer.Serialize(currentRole);

                    _mapper.Map(roleDto, currentRole);
                    currentRole.UpdatedAt = DateTime.Now;

                    _unitOfWork.Roles.Update(currentRole);
                    roleObj = currentRole;

                    await _unitOfWork.AuditLogs.AddAsync(new AuditLog
                    {
                        Action = "UPDATE",
                        TableName = "Roles",
                        CreatedAt = DateTime.UtcNow,
                        RecordId = $"Role {roleObj.IdRole}",
                        OldValues = oldValues,
                        NewValues = JsonSerializer.Serialize(roleObj),
                        Description = "Role update"
                    });
                }
                else
                {
                    //Register role
                    roleObj = _mapper.Map<Role>(roleDto);
                    roleObj.CreatedAt = DateTime.Now;

                    roleObj = await _unitOfWork.Roles.AddAsync(roleObj);

                    await _unitOfWork.AuditLogs.AddAsync(new AuditLog
                    {
                        Action = "REGISTER",
                        IdUser = _userContextService.UserId,
                        TableName = "Roles",
                        CreatedAt = DateTime.UtcNow,
                        RecordId = $"Role {roleObj.IdRole}",
                        NewValues = JsonSerializer.Serialize(roleObj),
                        Description = "Role register"
                    });
                }

                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.CommitAsync();

                return _mapper.Map<RoleDto>(roleObj);
            }
            catch
            {
                await _unitOfWork.RollbackAsync();
                throw;
            }
        }

    }
}
