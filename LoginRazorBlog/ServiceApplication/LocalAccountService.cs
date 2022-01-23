using LoginRazorBlog.Domain;
using LoginRazorBlog.Dto;
using LoginRazorBlog.Infrastructure.Context;
using LoginRazorBlog.Utils;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginRazorBlog.ServiceApplication
{
    public class LocalAccountService : ILocalAccountService
    {
        private readonly ILogger<LocalAccountService> _logger;
        private readonly AuthContext _dbContext;

        public LocalAccountService(ILogger<LocalAccountService> logger, AuthContext dbContext)
        {
            _logger=logger;
            _dbContext=dbContext;
        }

        public int Count()
        {
            return _dbContext.LocalAccount.Count();
        }

        public async Task<Guid> CreateAsync(string username, string clearPassword)
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                throw new ArgumentNullException(nameof(username), "value must not be empty.");
            }

            if (string.IsNullOrWhiteSpace(clearPassword))
            {
                throw new ArgumentNullException(nameof(clearPassword), "value must not be empty.");
            }

            var uid = Guid.NewGuid();
            var account = new LocalAccountEntity
            {
                Id = uid,
                CreateTimeUtc = DateTime.UtcNow,
                Username = username.ToLower().Trim(),
                PasswordHash = Helper.HashPassword(clearPassword.Trim())
            };
            await _dbContext.LocalAccount.AddAsync(account);
            await _dbContext.SaveChangesAsync();    

            return uid;



        }

        public async Task DeleteAsync(Guid id)
        {
            var account = await _dbContext.LocalAccount.FindAsync(id);
            if (account is null)
            {
                throw new InvalidOperationException($"LocalAccountEntity with Id '{id}' not found.");
            }

             _dbContext.LocalAccount.Remove(account);
            await _dbContext.SaveChangesAsync();

        }

        public bool Exist(string username)
        {
            var exist = _dbContext.LocalAccount.Any(p => p.Username == username.ToLower());
            return exist;
        }

        public async Task<IReadOnlyList<Account>> GetAllAsync()
        {
            var list = _dbContext.LocalAccount.Select(p=>new Account
            {
                Id = p.Id,
                CreateTimeUtc = p.CreateTimeUtc,
                LastLoginIp = p.LastLoginIp,
                LastLoginTimeUtc = p.LastLoginTimeUtc,
                Username = p.Username

            }).AsNoTracking().ToListAsync();
          
            return (IReadOnlyList<Account>)list;
        }

        public async Task<Account> GetAsync(Guid id)
        {
            var entity = await _dbContext.LocalAccount.FindAsync(id);

            var item = EntityToAccountModel(entity);
            return item;

        }
        private static Account EntityToAccountModel(LocalAccountEntity entity)
        {
            if (entity is null) return null;

            return new()
            {
                Id = entity.Id,
                CreateTimeUtc = entity.CreateTimeUtc,
                LastLoginIp = entity.LastLoginIp.Trim(),
                LastLoginTimeUtc = entity.LastLoginTimeUtc.GetValueOrDefault(),
                Username = entity.Username.Trim()
            };
        }

        public async Task LogSuccessLoginAsync(Guid id, string ipAddress)
        {
            var entity = await _dbContext.LocalAccount.FindAsync(id);

            if (entity is not null)
            {
                entity.LastLoginIp = ipAddress.Trim();
                entity.LastLoginTimeUtc = DateTime.UtcNow;
                _dbContext.Entry(entity).State = EntityState.Modified;
                await _dbContext.SaveChangesAsync();
              
            }
        }

        public async Task UpdatePasswordAsync(Guid id, string clearPassword)
        {
            if (string.IsNullOrWhiteSpace(clearPassword))
            {
                throw new ArgumentNullException(nameof(clearPassword), "value must not be empty.");
            }

            var account = await _dbContext.LocalAccount.FindAsync(id);
            if (account is null)
            {
                throw new InvalidOperationException($"LocalAccountEntity with Id '{id}' not found.");
            }

            account.PasswordHash = Helper.HashPassword(clearPassword);
            _dbContext.Entry(account).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();

            
        }

        public async Task<Guid> ValidateAsync(string username, string inputPassword)
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                throw new ArgumentNullException(nameof(username), "value must not be empty.");
            }

            if (string.IsNullOrWhiteSpace(inputPassword))
            {
                throw new ArgumentNullException(nameof(inputPassword), "value must not be empty.");
            }

            var account = await _dbContext.LocalAccount.FirstOrDefaultAsync(p => p.Username == username);
            if (account is null) return Guid.Empty;

            var valid = account.PasswordHash == Helper.HashPassword(inputPassword.Trim());
            return valid ? account.Id : Guid.Empty;
        }
    }
}
