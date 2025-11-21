using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EbookBackend.Application.Dto
{
    public class AuthResultDto
    {
        public string AccessToken { get; set; } = "";
        public string RefreshToken { get; set; } = "";
        public DateTime ExpiresAt { get; set; }
        public bool Success { get; set; }
        public string? ErrorMessage { get; set; }
    }
}
