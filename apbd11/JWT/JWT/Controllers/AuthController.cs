using System.ComponentModel.DataAnnotations;
using JWT.Contexts;
using JWT.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;
using System.Text;
using JWT.Helpers;
using Microsoft.AspNetCore.Authorization;

namespace JWT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly AppDbContext _context;
        
        public AuthController(IConfiguration config, AppDbContext context)
        {
            _config = config;
            _context = context;
        }
    
        
        
        /*
         * 1. Logowanie api/auth/login
         * Input: username (email), password
         * - Sprawdzenie poprawnosci danych uzytkownika
         * - if(true) => generujemy token z krotkim czasem zycia + refresh token z dlugim czasem zycia => 200
         * - if(false) => 401 niepoprny login lub haslo
         * Output: tokeny
         */
        [HttpPost("login")]
        public IActionResult Login(LoginRequestModel model) 
        {

            if(!(model.UserName.ToLower() == "kacper" && model.Password == "hello-world"))
            {
                return Unauthorized("Wrong username or password");
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescription = new SecurityTokenDescriptor
            {
                Issuer = _config["JWT:Issuer"],
                Audience = _config["JWT:Audience"],
                Expires = DateTime.UtcNow.AddMinutes(15),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:Key"]!)),
                    SecurityAlgorithms.HmacSha256
                )
            };
            var token = tokenHandler.CreateToken(tokenDescription);
            var stringToken = tokenHandler.WriteToken(token);

            var refTokenDescription = new SecurityTokenDescriptor
            {
                Issuer = _config["JWT:RefIssuer"],
                Audience = _config["JWT:RefAudience"],
                Expires = DateTime.UtcNow.AddDays(3),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:RefKey"]!)),
                    SecurityAlgorithms.HmacSha256
                )
            };
            var refToken = tokenHandler.CreateToken(refTokenDescription);
            var stringRefToken = tokenHandler.WriteToken(refToken);
            
            return Ok(new LoginResponseModel
            {
                Token = stringToken,
                RefreshToken = stringRefToken
            });
        }
        
        
        /*
         * 2. Refreshowanie sesji api/auth/refresh
         * Input: refresh token
         * - Sprawdzenie czy refresh token czy jest poprawny
         * - if(true) -> generujemy token z krotkim czasem zycia + refresh token z dlugim czasem zycia => 200
         * - if(false) => 401 Invalid token
         * Output: tokeny
         */
        [HttpPost("refresh")]
        public IActionResult RefreshToken(RefreshTokenRequestModel requestModel)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            try
            {
                tokenHandler.ValidateToken(requestModel.RefreshToken, new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = _config["JWT:RefIssuer"],
                    ValidAudience = _config["JWT:RefAudience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:RefKey"]!))
                }, out SecurityToken validatedToken);
                // return Ok(true + " " + validatedToken);
                //return Ok();
                // generowanie kolejnego tokenu oraz refresh tokenu
                var tokenDescription = new SecurityTokenDescriptor
                {
                    Issuer = _config["JWT:Issuer"],
                    Audience = _config["JWT:Audience"],
                    Expires = DateTime.UtcNow.AddMinutes(15),
                    SigningCredentials = new SigningCredentials(
                        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:Key"]!)),
                        SecurityAlgorithms.HmacSha256
                    )
                };
                var token = tokenHandler.CreateToken(tokenDescription);
                var stringToken = tokenHandler.WriteToken(token);

                var refTokenDescription = new SecurityTokenDescriptor
                {
                    Issuer = _config["JWT:RefIssuer"],
                    Audience = _config["JWT:RefAudience"],
                    Expires = DateTime.UtcNow.AddDays(3),
                    SigningCredentials = new SigningCredentials(
                        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:RefKey"]!)),
                        SecurityAlgorithms.HmacSha256
                    )
                };
                var refToken = tokenHandler.CreateToken(refTokenDescription);
                var stringRefToken = tokenHandler.WriteToken(refToken);
                
                return Ok(new RefreshTokenResponseModel
                {
                    Token = stringToken,
                    RefreshToken = stringRefToken
                });
                
            }
            catch
            {
                return Unauthorized();
            }
        }
        /*
         * 3. Rejestacja uzytkownika api/auth/register
         * - Input: username, password
         * - Sprawdzamy czy nazwa uzytkownika jest unikalna
         * - Walidujemy zapytanie
         * - Hashujemy haslo
         * - Tworzymy nowy rekord dla uzytkownika w bazie ktory bedzie zawieral jego username oraz hash ktory wygenerowalismy w ramach hasla
         */
        [HttpPost("register")]
        public IActionResult Register(RegisterRequestModel registerRequestModel)
        {
            if (_context.Users.Any(u=>u.UserName==registerRequestModel.UserName)!=null)
            {
                return Conflict("User with this username already exists.");
            }

            if (registerRequestModel.UserName==null || registerRequestModel.Password==null)
            {
                return BadRequest("not valid user name or password");
            }
            var hashedPasswordAndSalt = SecurityHelpers.GetHashedPasswordAndSalt(registerRequestModel.Password);
            var user = new AppUser
            {
                UserName = registerRequestModel.UserName,
                PasswordHash = hashedPasswordAndSalt.Item1
            };

            _context.Users.Add(user);
            _context.SaveChanges();
            
            return Ok();
        }
        
        
        [HttpGet("authorized")]
        [Authorize]
        public IActionResult TestAuthorize()
        {
            return Ok("authorized");
        }
        
        
        
        
    }
}
