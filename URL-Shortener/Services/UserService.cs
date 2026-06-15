using URL_Shortener.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace URL_Shortener.Services
{
    public class UserService(AppDbContext db, JwtService jwtService)
    {
        private readonly AppDbContext _db = db;
        private readonly JwtService _jwtService = jwtService;

        public async Task<string> RegisterAsync(string userName, string email, string password, CancellationToken cancellationToken = default)
        {
            if (await _db.Users.FirstOrDefaultAsync(u => u.Email == email, cancellationToken) != null)
                throw new Exception("This user already exists.");

            var user = new User()
            {
                Name = userName,
                Email = email
            };
            user.HashedPassword = new PasswordHasher<User>().HashPassword(user, password);

            await _db.Users.AddAsync(user, cancellationToken);
            await _db.SaveChangesAsync(cancellationToken);

            return _jwtService.GenerateToken(user);
        }

        public async Task<string> LoginAsync(string email, string password, CancellationToken cancellationToken = default)
        {
            var user = await _db.Users.FirstOrDefaultAsync(u => u.Email == email, cancellationToken) ??
                throw new Exception("User not found.");

            var res = new PasswordHasher<User>().VerifyHashedPassword(user, user.HashedPassword, password);
            if (res == PasswordVerificationResult.Failed)
                throw new Exception("Incorrect password.");

            return _jwtService.GenerateToken(user);
        }
    }
}
