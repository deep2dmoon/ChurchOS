using htmos.contract;
using htmos.data;
using htmos.model;
using Microsoft.EntityFrameworkCore;
using producer.model;

namespace htmos.services.repository;

public class UserRepository(DatabaseContext _context) : IUserRepository
{
    public async Task<List<User>> GetUsersAsync()
    {
        return [.. _context.Users];
    }

    public async Task<string> GetUserIDAsync(int Id)
    {
        User? user = await _context.Users.FirstOrDefaultAsync(u => u.ID == Id)!;
        return user?.Name!;
    }

    public async Task<User> GetUserByEmailAsync(string email)
    {
        User? user = await _context.Users
        .Include(u => u.Role)
        .ThenInclude(r => r.RolePermissions)
        .ThenInclude(rp => rp.Permission)
        .FirstOrDefaultAsync(u => u.Email == email);
        return user!;
    }
    public async Task<User> FindUserByPhoneAsync(string phone)
    {
        User? user = await _context.Users.FirstOrDefaultAsync(m => m.Phone == phone);
        return user!;
    }

    public async Task<object> RegisterUserAsync(NewUser member_)
    {
        try
        {
            User user = new() { Email = member_.Email, Name = member_.Name, Password = member_.Password, Phone = member_.Phone, RoleID = member_.RoleID };
            User? existing = await _context.Users.FirstOrDefaultAsync(m => m.Phone == member_.Phone || m.Email == member_.Email);
            if (existing != null)
            {
                return new { response = $"Member with the phone {member_.Phone} already exist" };
            }
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return new { response = "New member successfully registered" };
        }
        catch (System.Exception)
        {
            throw;
        }
    }


    public async Task<object> RegisterBranchAdmin(NewBranchDto branchAdmin_)
    {
        try
        {
            Branch branch = new() { Name = branchAdmin_.Branch };

            User user = new()
            {
                Name = branchAdmin_.Name,
                RoleID = branchAdmin_.RoleID,
                Email = branchAdmin_.Email,
                Password = branchAdmin_.Password,
                Phone = branchAdmin_.Phone,
            };
            BranchAdmin branchAdmin = new() { User = user, Branch = branch };

            User? existing = await _context.Users.FirstOrDefaultAsync(m => m.Email == branchAdmin_.Email || m.Phone == branchAdmin_.Phone);
            if (existing != null)
            {
                return new { response = $"Branch adminstratiton with the phone {branchAdmin_.Phone} already exist" };
            }
            _context.BranchAdmins.Add(branchAdmin);
            await _context.SaveChangesAsync();

            return new { response = "New admin successfully registered" };
        }
        catch (System.Exception)
        {
            throw;
        }
    }

    public async Task<bool> DeleteUserAsync(int ID)
    {
        try
        {
            User? user = await _context.Users.FirstOrDefaultAsync(u => u.ID == ID);
            if (user != null)
            {
                _context.Users.Remove(user!);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
        catch (System.Exception)
        {
            throw;
        }
    }
}