using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestBlazorIdentity.Server.Data;
using TestBlazorIdentity.Server.Models;
using TestBlazorIdentity.Shared;

namespace TestBlazorIdentity.Server.Controllers
{

    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class SuperHeroController : ControllerBase
    {
        //private readonly SignInManager<SuperHero> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;
        //private readonly IUserStore<SuperHero> _heroStore;
        // private readonly IUserEmailStore<SuperHero> _emailStore;
        public SuperHeroController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            this._context = context;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<ActionResult<SuperHero>> GetHeroAsync()
        {
            var user = await _userManager.FindByIdAsync(User.FindFirstValue(ClaimTypes.NameIdentifier) ?? string.Empty);
            // var user = await _context.Users.Include(u => u.SuperHeros)
            // .FirstOrDefaultAsync(u => u.Id == User.FindFirstValue(ClaimTypes.NameIdentifier));
            if (user == null)
                return NotFound();

            return Ok(user.SuperHeros);
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] SuperHero superHero)
        {
            await _context.AddAsync(superHero);
            await _context.SaveChangesAsync();
            if (!_context.ChangeTracker.HasChanges())
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        private SuperHero CreateHero()
        {
            try
            {
                return Activator.CreateInstance<SuperHero>();
            }
            catch
            {
                throw new InvalidOperationException($"Can't create an instance of '{nameof(SuperHero)}'. " +
                    $"Ensure that '{nameof(SuperHero)}' is not an abstract class and has a parameterless constructor, or alternatively " +
                    $"override the register page in /Areas/Identity/Pages/Account/Register.cshtml");
            }
        }
    }
}