using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EbookBackend.Application.Interfaces
{
    public interface IUserContextService
    {
        int UserId { get; }
        string? Email { get; }
        IReadOnlyList<string> Roles { get; }
    }
}
