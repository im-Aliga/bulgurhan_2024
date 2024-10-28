using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using QRMenu.Database;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;

using QRMenu.Areas.Client.ViewModels;

namespace QRMenu.Areas.Client.Controllers
{
    [Area("client")]
    [Route("auth")]
    public class AuthController : Controller
    {
        private readonly DataContext _dbContext;

        public AuthController(DataContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet("login", Name = "client-auth-login")]
        public async Task<IActionResult> LoginAsync()
        {
            ClaimsPrincipal claimsPrincipal = HttpContext.User;
            if (claimsPrincipal.Identity.IsAuthenticated)
                return RedirectToRoute("admin-product-list");

            var admin = new LoginViewModel();
            return View(admin);
        }




        [HttpPost("login", Name = "client-auth-login")]
        public async Task<IActionResult> LoginAsync(LoginViewModel user)
        {

            var admin = await _dbContext.Users
                .FirstOrDefaultAsync(x => x.Username == user.Username && x.Password == user.Password);

            if (admin is not null)
            {
                List<Claim> claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.NameIdentifier,admin.Username)
                };

                ClaimsIdentity identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(identity));

                return RedirectToRoute("admin-product-list");
            }

            return View(user);
        }
    }
}
