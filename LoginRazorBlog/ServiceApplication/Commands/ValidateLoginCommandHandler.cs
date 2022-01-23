using LoginRazorBlog.Infrastructure.Context;
using LoginRazorBlog.Utils;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginRazorBlog.ServiceApplication.Commands
{
    public class ValidateLoginCommandHandler : IRequestHandler<ValidateLoginCommand, Guid>
    {
        private readonly AuthContext _dbContext;

        public ValidateLoginCommandHandler(AuthContext dbContext)
        {
            _dbContext=dbContext;
        }

        public async Task<Guid> Handle(ValidateLoginCommand request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(request.Username))
            {
                throw new ArgumentNullException(nameof(request.Username), "value must not be empty.");
            }
            
            request.InputPassword=String.Empty; // uji test

            if (string.IsNullOrWhiteSpace(request.InputPassword))
            {
                throw new ArgumentNullException(nameof(request.InputPassword), "value must not be empty.");
            }

            var account = await _dbContext.LocalAccount.FirstOrDefaultAsync(p => p.Username == request.Username);
            if (account is null) return Guid.Empty;

            var valid = account.PasswordHash == Helper.HashPassword(request.InputPassword.Trim());
            return valid ? account.Id : Guid.Empty;
        }
    }
}
