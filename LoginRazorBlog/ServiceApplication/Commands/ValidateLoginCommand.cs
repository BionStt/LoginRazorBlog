using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginRazorBlog.ServiceApplication.Commands
{
    public class ValidateLoginCommand : IRequest<Guid>
    {
        public ValidateLoginCommand(string username, string inputPassword)
        {
            Username=username;
            InputPassword=inputPassword;
        }

        public string Username { get; set; }
        public string InputPassword { get; set; }
    }
}
