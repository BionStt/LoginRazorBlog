using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginRazorBlog.ServiceApplication.Commands
{
    public class LogSuccessLoginCommand : IRequest
    {
        public LogSuccessLoginCommand(Guid id, string ipAddress)
        {
            Id=id;
            IpAddress=ipAddress;
        }

        public Guid Id { get; set; }
        public string IpAddress { get; set; }
    }
}
