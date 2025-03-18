using PRN222.Lab2.Repositories.Interfaces;
using PRN222.Lab2.Repositories.Models;
using PRN222.Lab2.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN222.Lab2.Services
{
    public class AccountService : IAccountService
    {
        private readonly IUnitOfWork _unitOfWork;
        public AccountService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<List<AccountMember>> GetAllAccountMembers()
        {
            return await _unitOfWork.Accounts.GetAllAsync();
        }

        public async Task<AccountMember> GetAccountMemberById(int id)
        {
            return await _unitOfWork.Accounts.GetByIdAsync(id);
        }
        public async Task AddAccountMember(AccountMember account)
        {
            await _unitOfWork.Accounts.AddAsync(account);
        }
        public async Task UpdateAccountMember(AccountMember account)
        {
            await _unitOfWork.Accounts.UpdateAsync(account);
        }
        public async Task DeleteAccountMember(AccountMember account)
        {
            await _unitOfWork.Accounts.DeleteAsync(account);
        }
    }
}