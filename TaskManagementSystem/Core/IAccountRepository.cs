using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TaskManagementSystem.DTOs;

namespace TaskManagementSystem.Core
{
    public interface IAccountRepository
    {
        Task<ActionResult<UserDto>> Register(LoginDto registerDto);
        Task<bool> Login(LoginDto loginDto);
    }
}