using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EbookBackend.Domain.Entities
{
    public class UserToken
    {
        public int IdUserToken { get; set; }
        public int IdUser { get; set; }
        public string TokenHash { get; set; } = string.Empty;  // SHA256(token)
        public DateTime ExpiresAt { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? RevokedAt { get; set; }
        public string? ReplacedByTokenHash { get; set; }

        // Navigation
        public User? User { get; set; }

        // Propiedad calculada
        public bool IsActive => RevokedAt == null && DateTime.UtcNow < ExpiresAt;
    }
}
