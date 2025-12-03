using EbookBackend.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EbookBackend.Application.Dto
{
    public class UserRegisterRequestDto
    {
        public string Name { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PlainPassword { get; set; } = string.Empty;
        public List<int> IdRoles { get; set; } = new List<int>();
    }
}