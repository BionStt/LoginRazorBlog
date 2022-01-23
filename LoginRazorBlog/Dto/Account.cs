using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginRazorBlog.Dto
{
    public class Account
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public DateTime? LastLoginTimeUtc { get; set; }
        public string LastLoginIp { get; set; }
        public DateTime CreateTimeUtc { get; set; }

    }
}
