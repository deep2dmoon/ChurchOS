using htmos.model;
using producer.model;

namespace htmos.contract;

public interface IUserRepository
{
    Task<List<User>> GetUsersAsync();
    Task<User> GetUserByEmailAsync(string email);
    Task<User> FindUserByPhoneAsync(string phone);
    Task<object> RegisterUserAsync(NewUser member_);
    Task<object> RegisterBranchAdmin(NewBranchDto branchAdmin);
    Task<bool> DeleteUserAsync(int ID);
    Task<string> GetUserIDAsync(int Id);
}