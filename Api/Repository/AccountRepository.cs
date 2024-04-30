using Api.Dtos.Account;
using Api.Interfaces;
using Api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using static Api.Dtos.Account.ServiceResponses;

namespace Api.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _config;
        public AccountRepository(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration config) 
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _config = config;
        }

        public async Task<GeneralResponse> CreateAccount(RegisterDto registerDto)
        {
            if (registerDto == null) return new GeneralResponse(false, "Model is empty");
            var newUser = new AppUser()
            {
                Name = registerDto.Name,
                Email = registerDto.Email,
                PasswordHash = registerDto.Password,
                UserName = registerDto.Email
            };
            var user = await _userManager.FindByEmailAsync(newUser.Email);
            if (user != null) return new GeneralResponse(false, "User registered already");

            var createUser = await _userManager.CreateAsync(newUser!, registerDto.Password);
            if (!createUser.Succeeded) return new GeneralResponse(false, "Error occured.. please try again");

            //Assign Default Role : Admin to first registrar; rest is user
            var checkAdmin = await _roleManager.FindByNameAsync("Admin");
            if (checkAdmin == null)
            {
                await _roleManager.CreateAsync(new IdentityRole() { Name = "Admin" });
                await _userManager.AddToRoleAsync(newUser, "Admin");
                return new GeneralResponse(true, "Account Created");
            }
            else
            {
                var checkUser = await _roleManager.FindByNameAsync("User");
                if (checkUser == null)
                    await _roleManager.CreateAsync(new IdentityRole() { Name = "User" });
                await _userManager.AddToRoleAsync(newUser, "User");
                return new GeneralResponse(true, "Account Created");
            }
        }

        public async Task<LoginResponse> LoginAccount(LoginDto loginDto)
        {
            if (loginDto == null)
                return new LoginResponse(false, null!, "Login container is empty");

            var getUser = await _userManager.FindByEmailAsync(loginDto.Email);
            if (getUser == null)
                return new LoginResponse(false, null!, "User not found");

            var checkUserPasswords = await _userManager.CheckPasswordAsync(getUser, loginDto.Password);
            if (!checkUserPasswords)
                return new LoginResponse(false, null!, "Invalid email/password");

            var getUserRole = await _userManager.GetRolesAsync(getUser);
            var userSession = new UserSession(getUser.Id, getUser.Name, getUser.Email, getUserRole.First());
            string token = GenerateToken(userSession);
            return new LoginResponse(true, token!, "Login completed");
        }

        private string GenerateToken(UserSession user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:SigningKey"]!));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var userClaims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role)
            };
            var token = new JwtSecurityToken(
                issuer: _config["JWT:Issuer"],
                audience: _config["JWT:Audience"],
                claims: userClaims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: credentials
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
