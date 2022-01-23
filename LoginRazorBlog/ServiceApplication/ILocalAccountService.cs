using LoginRazorBlog.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginRazorBlog.ServiceApplication
{
    public interface ILocalAccountService
    {
        int Count();
        Task<Account> GetAsync(Guid id);
        Task<IReadOnlyList<Account>> GetAllAsync();
        Task<Guid> ValidateAsync(string username, string inputPassword);
        Task LogSuccessLoginAsync(Guid id, string ipAddress);
        bool Exist(string username);
        Task<Guid> CreateAsync(string username, string clearPassword);
        Task UpdatePasswordAsync(Guid id, string clearPassword);
        Task DeleteAsync(Guid id);
    }
}
