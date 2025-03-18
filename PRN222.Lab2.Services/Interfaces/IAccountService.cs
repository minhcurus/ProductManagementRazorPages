using PRN222.Lab2.Repositories.Interfaces;
using PRN222.Lab2.Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN222.Lab2.Services.Interfaces
{
    public interface IAccountService
    {

        Task<List<AccountMember>> GetAllAccountMembers();

        Task<AccountMember> GetAccountMemberById(int id);

        Task AddAccountMember(AccountMember account);

        Task UpdateAccountMember(AccountMember account);

        Task DeleteAccountMember(AccountMember account);

    }
}
