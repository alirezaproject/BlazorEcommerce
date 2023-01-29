using System.Security.Cryptography;
using System.Text;

namespace BlazorEcommerce.Server.Services.AuthService;

public class AuthService : IAuthService
{
    private readonly DataBaseContext _context;

    public AuthService(DataBaseContext context)
    {
        _context = context;
    }

    public async Task<ServiceResponse<int>> Register(User user, string password)
    {
        if (await UserExists(user.Email))
        {
            return new ServiceResponse<int>() { Success = false ,Message = "User already exists."};
        }

        CreatePasswordHash(password,out byte[] passwordHash,out byte[] passwordSalt);

        user.PasswordHash = passwordHash;
        user.PasswordSalt = passwordSalt;

        _context.Users.Add(user);
        await _context.SaveChangesAsync();
        return new ServiceResponse<int>() { Data = user.Id ,Message = "Registration successfull !"};

    }


    public async Task<bool> UserExists(string email) => await _context.Users.AnyAsync(u => u.Email.ToLower().Equals(email.ToLower()));


    private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
    {
        using var hmac = new HMACSHA512();
        passwordSalt = hmac.Key;
        passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

    }
}