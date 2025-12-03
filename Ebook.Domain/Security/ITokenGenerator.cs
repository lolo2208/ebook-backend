using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EbookBackend.Domain.Security
{
    public interface ITokenGenerator
    {
        public string GenerateToken(int userId, string email, IEnumerable<string> roles);
        public (int userId, string email, IEnumerable<string> role)? ReadToken(string token);
    }
}
