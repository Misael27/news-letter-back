using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using NewsLetter.Core.Entities;
using NewsLetter.Web.AuthConfigure;
using System.Security.Claims;

namespace NewsLetter.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _configuration;
        private readonly IJwtFactory _jwtFactory;

        public AuthController(UserManager<User> userManager, IConfiguration configuration, IJwtFactory jwtFactory)
        {
            _userManager = userManager;
            _configuration = configuration;
            _jwtFactory = jwtFactory;
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create(CreateCredentialsBindingModel user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            User appUser = new User
            {
                FullName = user.FullName,
                UserName = user.Email,
                Email = user.Email
            };

            IdentityResult result = await _userManager.CreateAsync(appUser, user.Password);
            if (!result.Succeeded)
            {
                return BadRequest();
            }
            return Ok();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(CredentialsBindingModel credentials)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var identity = await GetClaimsIdentity(credentials.Email, credentials.Password);
            if (identity == null)
            {
                throw new BadHttpRequestException("Bad credentials");
            }
            var response = await _jwtFactory.GenerateEncodedToken(credentials.Email, identity);
            return Ok(response);
        }

        private async Task<ClaimsIdentity> GetClaimsIdentity(string email, string password)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                throw new BadHttpRequestException("Bad credentials");
            }
            var userToVerify = await _userManager.FindByEmailAsync(email);
            var result = await _userManager.CheckPasswordAsync(userToVerify, password);

            if (!result)
            {
                throw new BadHttpRequestException("Bad credentials");
            }
            IList<string> userRole = await _userManager.GetRolesAsync(userToVerify);
            await _userManager.ResetAccessFailedCountAsync(userToVerify);
            var identity = await Task.FromResult(_jwtFactory.GenerateClaimsIdentity(userToVerify, userRole));
            return identity;
        }

    }
}
