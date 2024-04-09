using Microsoft.AspNetCore.Identity;

namespace FasgionGrid.UserService.Services
{
    public interface IJWTTokenGenerator
    {
        string GenerateToken(IdentityUser user, IEnumerable<string> roles);
    }
}
