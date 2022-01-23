using LoginRazorBlog.Infrastructure.Context;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginRazorBlog.ServiceApplication.Commands
{
    public class LogSuccessLoginCommandHandler : IRequestHandler<LogSuccessLoginCommand>
    {
        private readonly AuthContext _dbContext;

        public LogSuccessLoginCommandHandler(AuthContext dbContext)
        {
            _dbContext=dbContext;
        }

        public async Task<Unit> Handle(LogSuccessLoginCommand request, CancellationToken cancellationToken)
        {
            var entity = await _dbContext.LocalAccount.FindAsync(request.Id);

            if (entity is not null)
            {
                entity.LastLoginIp = request.IpAddress.Trim();
                entity.LastLoginTimeUtc = DateTime.UtcNow;
                _dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                await _dbContext.SaveChangesAsync();

            }

            return Unit.Value;
        }
    }

  
}
