using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EbookBackend.Domain.Entities
{
    public class UserRole
    {
        public int IdUser { get; set; }
        public int IdRole { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        // Navigation
        public User UserObj { get; set; } = null!;
        public Role RoleObj { get; set; } = null!;
    }
}
