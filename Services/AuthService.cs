using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using htmos.contract;
using htmos.dtos;
using Microsoft.IdentityModel.Tokens;
using producer.model;

namespace htmos.services;

public class AuthService(IConfiguration _configuration, IUserRepository userRepository)
{
    // create jwt token with  user datails 
    public async Task<string> GetTokenAsync(User user)
    {
        // what are the claims to use ??
        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, user.ID.ToString()),
            new(ClaimTypes.Name,user.Name),
        };
        foreach (var rp in user.Role.RolePermissions)
        {
            claims.Add(new Claim("Permission", rp.Permission.Name));
        }


        // security key to be used
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetValue<string>("Jwt:Secret")!));
        // create token credentials with the key now
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        // now create tokens with the credentials 
        var token = new JwtSecurityToken(
            issuer: _configuration.GetValue<string>("Valid:iss"),
            audience: _configuration.GetValue<string>("Valid:aud"),
            expires: DateTime.UtcNow.AddHours(1),
            signingCredentials: credentials, claims: claims);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public async Task<User>? GetUserAsync(UserDTO user_)
    {
        User? user = await userRepository.GetUserByEmailAsync(user_.Email);
        return user?.Password == user_.Password ? new User
        {
            ID = user.ID,
            Email = user.Email,
            Phone = user.Phone,
            Name = user.Name,
            RoleID = user.RoleID,
            Role = user.Role

        } : null!;
    }
}