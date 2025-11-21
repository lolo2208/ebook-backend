using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EbookBackend.Application.Dto
{
    public class RevokeRequestDto
    {
        public string RefreshToken { get; set; } = string.Empty;
    }
}
