using Wang.Seamas.Web.Interfaces;

namespace Wang.Seamas.RBAC.Application.Services;

public class BcryptPasswordHasher : IPasswordHasher
{
    public string HashPassword(string password) => BCrypt.Net.BCrypt.HashPassword(password);
    public bool VerifyPassword(string password, string hashedPassword) => BCrypt.Net.BCrypt.Verify(password, hashedPassword);
}