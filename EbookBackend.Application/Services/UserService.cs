using AutoMapper;
using EbookBackend.Application.Dto;
using EbookBackend.Application.Interfaces;
using EbookBackend.Domain.Entities;
using EbookBackend.Domain.Interfaces;
using EbookBackend.Domain.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace EbookBackend.Application.Services
{
    public class UserService : BaseService<User>, IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IPasswordHasher _passwordHasher;

        public UserService(IUnitOfWork unitOfWork, IMapper mapper, IPasswordHasher passwordHasher) : base(unitOfWork.Users, unitOfWork) 
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _passwordHasher = passwordHasher;
        }

        public async Task<UserDto> TransactionRegisterUser(UserRegisterRequestDto userRegister)
        {
            try
            {
                var userObj = _mapper.Map<User>(userRegister);
                userObj.CreatedAt = DateTime.Now;
                userObj.Password = _passwordHasher.Hash(userRegister.PlainPassword);

                List<UserRole> userRoles = new List<UserRole>();
                userRegister.IdRoles.ForEach((idRol) =>
                {
                    userRoles.Add(new UserRole()
                    {
                        IdRole = idRol,
                        CreatedAt = DateTime.Now
                    });
                });

                //Init transacción
                await _unitOfWork.BeginTransactionAsync();
                
                //Save user
                User user = await _unitOfWork.Users.AddAsync(userObj);

                //Save user roles
                var futures = new List<Task>();
                foreach (var item in userRoles)
                {
                    item.IdUser = user.IdUser;
                    futures.Add(_unitOfWork.UserRoles.AddAsync(item));
                }
                await Task.WhenAll(futures);

                //Save auditoring log
                await _unitOfWork.AuditLogs.AddAsync(new AuditLog
                {
                    Action = "REGISTER",
                    TableName = "Users",
                    CreatedAt = DateTime.UtcNow,
                    RecordId = $"User {user.IdUser}",
                    NewValues = JsonSerializer.Serialize(user),
                    Description = "User register"
                });

                //Save state
                await _unitOfWork.SaveChangesAsync();

                //Commit transaction
                await _unitOfWork.CommitAsync();

                return _mapper.Map<UserDto>(user);
            }
            catch (Exception)
            {
                await _unitOfWork.RollbackAsync();
                throw;
            }
        }

    }
}
