using CalorieDiaryCalculator.Server.Data.Models;
using CalorieDiaryCalculator.Server.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CalorieDiaryCalculator.Server.Features {
    public class IdentityController : ApiController {
        private readonly UserManager<CalorieDiaryCalculatorUser> userManager;
        private readonly AppSettings appSettings;

        public IdentityController(UserManager<CalorieDiaryCalculatorUser> userManager, IOptions<AppSettings> appSettings) {
            this.userManager = userManager;
            this.appSettings = appSettings.Value;

        }

        [Route(nameof(Register))]
        public async Task<ActionResult> Register(RegisterRequestModel model) {
            var user = new CalorieDiaryCalculatorUser {
                UserName = model.UserName,
                Email = model.Email
            };
            var result = await this.userManager.CreateAsync(user, model.Password);

            if (result.Succeeded) {
                return this.Ok("Created");
            }

            return this.BadRequest(result.Errors);
        }

        [Route(nameof(Login))]
        public async Task<ActionResult<object>> Login(LoginRequestModel model) {
            var user = await this.userManager.FindByNameAsync(model.UserName);

            if (user == null) {
                return Unauthorized("no such user");
            }

            var passwordValid = await this.userManager.CheckPasswordAsync(user, model.Password);

            if (passwordValid == false) {
                return Unauthorized("incorrect password");
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor {
                Subject = new ClaimsIdentity(new [] {
                    new Claim(ClaimTypes.NameIdentifier, user.Id),
                    new Claim(ClaimTypes.Name, user.UserName)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var encryptedToken = tokenHandler.WriteToken(token);

            return new { Token = encryptedToken };
        }
    }
}
