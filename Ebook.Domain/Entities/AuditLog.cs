using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EbookBackend.Domain.Entities
{
    public class AuditLog
    {
        public int IdAuditLog { get; set; }
        public int IdUser { get; set; }
        public string Action { get; set; } = string.Empty;
        public string TableName { get; set; } = string.Empty; 
        public string RecordId { get; set; } = string.Empty;
        public string OldValues { get; set; } = string.Empty;
        public string NewValues { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
    }
}
