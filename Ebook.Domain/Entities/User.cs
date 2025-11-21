using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EbookBackend.Domain.Entities
{
    public class User
    {
        public int IdUser { get; set; }
        public string Name { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        // Navigation
        public ICollection<UserRole>? UserRoles { get; set; }
        public ICollection<UserToken>? UserTokens { get; set; }
        public ICollection<BookReview>? BookReviews { get; set; }
    }
}
