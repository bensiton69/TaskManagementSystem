using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskManagementSystem.Core.Models;
using TaskManagementSystem.DTOs;
using TaskManagementSystem.Persistence;

namespace TaskManagementSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public UsersController(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<KeyValuePairDto>>> GetUsers()
        {
            var users = await _context.Users.ToListAsync();
            return _mapper.Map<IEnumerable<AppUser>, List<KeyValuePairDto>>(users);
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<KeyValuePairDto>> GetUser(Guid id)
        {
            return _mapper.Map<AppUser, KeyValuePairDto>(await _context.Users.FindAsync(id));
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteSystemTask(Guid id)
        {

            var user = await _context.Users.FindAsync(id);
            if (user == null)
                return NotFound();
            _context.Remove(user);
            await _context.SaveChangesAsync();
            return Ok(id);
        }
    }
}
