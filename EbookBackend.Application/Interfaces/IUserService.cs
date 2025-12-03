using EbookBackend.Application.Dto;
using EbookBackend.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EbookBackend.Application.Interfaces
{
    public interface IUserService : IBaseService<User>
    {
        Task<UserDto> TransactionRegisterUser(UserRegisterRequestDto user);
    }
}
