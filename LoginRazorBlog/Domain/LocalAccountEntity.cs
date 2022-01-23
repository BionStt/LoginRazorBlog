using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginRazorBlog.Domain
{
    public class LocalAccountEntity
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public DateTime? LastLoginTimeUtc { get; set; }
        public string? LastLoginIp { get; set; }
        public DateTime CreateTimeUtc { get; set; }
    }
}
