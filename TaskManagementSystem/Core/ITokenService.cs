using TaskManagementSystem.Core.Models;

namespace TaskManagementSystem.Core
{
    public interface ITokenService
    {
        string CreateToken(AppUser user);

    }
}
