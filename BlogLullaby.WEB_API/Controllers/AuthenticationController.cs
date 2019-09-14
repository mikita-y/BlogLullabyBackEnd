using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using BlogLullaby.BLL.AuthenticationService;
using BlogLullaby.BLL.AuthenticationService.DTO;
using BlogLullaby.BLL.EmailService;
using BlogLullaby.WEB_API.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace BlogLullaby.WEB_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : Controller
    {
        private IAuthenticationService _userService;
        private IEmailService _emailService;
        private JWTConfig jwtConfig;

        public AuthenticationController(IAuthenticationService authService, IOptions<JWTConfig> jwtConfig, IEmailService emailService)
        {
            _userService = authService;
            _emailService = emailService;
            this.jwtConfig = jwtConfig.Value;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(1);
        }

        // POST: api/Authentication
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> LogIn(LogInUserDTO user)
        {
            var result = await _userService.AuthenticateAsync(user);
            if (!result.IsSuccess)
                return BadRequest(result.Descriptions);
            var encodedJwt = GetToken(user.Login);
            var response = new
            {
                access_token = encodedJwt,
                username = user.Login
            };
            return Ok(response);
        }

        [HttpPost]
        [Route("registration")]
        public async Task<IActionResult> Registration(RegistrationUserDTO user)
        {
            var result = await _userService.CreateUserAsync(user);

            if (result.IsSuccess)
            {
                var code = await _userService.GenerateEmailConfirmationTokenAsync(user.Email);
                var callbackUrl = Url.Action(
                    "confirmemail",
                    "authentication",
                    new { email = user.Email, code },
                    protocol: HttpContext.Request.Scheme);
                await _emailService.SendEmailAsync(user.Email, "Confirm your account",
                    $"<p>Hello, {user.FirstName}!</p> <p>Confirm registration by clicking on the link: <a href='{callbackUrl}'>BloglullabyApi</a> </p><p>Sincerely, BlogLullaby website administration.</p>", $"{user.FirstName} {user.LastName}");
                var response = new
                {
                    username = user.Email,
                    password = user.Password
                };
                return Ok(response);
            }
            else
            {
                return BadRequest(result.Descriptions);
            }
        }

        [Authorize]
        [HttpPut("{username}")]
        public async Task<ActionResult> Put(string username)
        {
            var claimName = await HttpContext.GetUserNameAsync();
            var result = await _userService.ChangeUsernameAsync(claimName, username);
            if (!result.IsSuccess)
                return BadRequest(result.Descriptions);
            var encodedJwt = GetToken(username);
            var response = new
            {
                access_token = encodedJwt,
                username
            };
            return Ok(response);
        }

        [HttpGet]
        [Route("confirmemail")]
        public async Task<IActionResult> ConfirmEmail(string email, string code)
        {
            if (email == null || code == null)
            {
                return BadRequest();
            }
            var result = await _userService.ConfirmEmailAsync(email, code);
            if (result.IsSuccess)
                return Redirect("http://localhost:3000");
            else
                return BadRequest();
        }

        private string GetToken(string username)
        {
            var signingKey = jwtConfig.SymmetricSecurityKey;
            var expiryDuration = jwtConfig.LifeTime;

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = jwtConfig.Issuer,
                Audience = jwtConfig.Audience,
                IssuedAt = DateTime.UtcNow,
                NotBefore = DateTime.UtcNow,
                Expires = DateTime.UtcNow.AddMinutes(expiryDuration),
                Subject = new ClaimsIdentity(new List<Claim> {
                new Claim("username", username)
            }),
                SigningCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256Signature)
            };
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = jwtTokenHandler.CreateJwtSecurityToken(tokenDescriptor);
            var token = jwtTokenHandler.WriteToken(jwtToken);
            return token;
        }

        [HttpGet]
        [Authorize]
        [Route("changeusername")]
        public async Task<IActionResult> ChangeUsername(string newUsername)
        {
            var claimName = await HttpContext.GetUserNameAsync();
            var result = await _userService.ChangeUsernameAsync(claimName, newUsername);
            if(!result.IsSuccess)
                return BadRequest(result.Descriptions);
            var encodedJwt = GetToken(newUsername);
            var response = new
            {
                access_token = encodedJwt,
                username = newUsername
            };
            return Ok(response);
        }

    }
}
