using BookStoreAPI.Services.AuthService.DTOs;
using BookStoreAPI.Services.AuthService.Repositories;
using BCrypt.Net;
using BookStoreAPI.Services.StaffService.Entities;
using BookStoreAPI.Services.AuthService.Interfaces;
using BookStoreAPI.Services.EmailService;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc.Routing;

namespace BookStoreAPI.Services.AuthService
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _authRepository;
        private readonly IConfiguration _config;
        private readonly EmailSenderService _emailService;

        public AuthService(IAuthRepository authRepository, IConfiguration config, EmailSenderService emailService)
        {
            _authRepository = authRepository;
            _config = config;
            _emailService = emailService;
        }

        public async Task<bool> RegisterAsync (RegisterDTO registerDTO)
        {
            var existingByEmail = await _authRepository.GetByEmailAsync(registerDTO.Email);
            var existingByPhone = await _authRepository.GetByPhoneAsync(registerDTO.Phone);
            var existingByCitizenIdentification = await _authRepository.GetByCitizenIdentificationAsync(registerDTO.CitizenIdentification);

            if (existingByEmail != null || existingByPhone != null || existingByCitizenIdentification != null)
                return false;                

            if (registerDTO.Password != registerDTO.ConfirmPassword)
                return false;

            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(registerDTO.Password);

            var staff = new Staff
            {
                FamilyName = registerDTO.FamilyName,
                GivenName = registerDTO.GivenName,
                DateOfBirth = registerDTO.DateOfBirth,
                Address = registerDTO.Address,
                Phone = registerDTO.Phone,
                Email = registerDTO.Email,
                CitizenIdentification = registerDTO.CitizenIdentification,
                HashPassword = hashedPassword,
                Gender = registerDTO.Gender,
                Role = registerDTO.Role,
                IsActived = false
            };

            await _authRepository.AddSync(staff);

            var saved = await _authRepository.SaveChangesAsync();

            if (saved == false)
                return false;

            var token = GenerateActivationToken(staff.Id);
            var activationLink = $"http://localhost:5208/api/auth/activate?token={token}";

            await _emailService.SendEmailAsync(staff.Email, "Activate your account",
                $"Click on the following link to activate your account.: <a href='{activationLink}'>Verify account</a>");

            Console.WriteLine($"Send email confirm to: {staff.Email} successfully.");

            return true;
        }

        public async Task<bool> ActivateAccountAsync(string token)
        {
            var jwtKey = Environment.GetEnvironmentVariable("JWT_SECRET_KEY") ?? _config["Jwt:Key"];

            var handler = new JwtSecurityTokenHandler();

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey)),
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true
            };

            var principal = handler.ValidateToken(token, tokenValidationParameters, out var validatedToken);
            var staffIdClaim = principal.FindFirst("staffId");
            if (staffIdClaim == null) return false;

            var staffId = Guid.Parse(staffIdClaim.Value);
            var staff = await _authRepository.GetByIdAsync(staffId);
            if (staff == null) return false;

            if (staff.IsActived) return false;

            staff.IsActived = true;
            return await _authRepository.SaveChangesAsync();
        }


        private string GenerateActivationToken(Guid staffId)
        {
            var jwtKey = Environment.GetEnvironmentVariable("JWT_SECRET_KEY")
                      ?? _config["Jwt:Key"]
                      ?? throw new Exception("JWT key is missing");

            var jwtIssuer = _config["Jwt:Issuer"];
            var jwtAudience = _config["Jwt:Audience"];
            var expireMinutes = int.Parse(_config["Jwt:ExpireMinutes"] ?? "60");

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[] { new Claim("staffId", staffId.ToString()) };

            var token = new JwtSecurityToken(
                issuer: jwtIssuer,
                audience: jwtAudience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(expireMinutes),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<String?> LoginAsync(LoginDTO loginDTO)
        {
            var staff = await _authRepository.GetByEmailAsync(loginDTO.Email);

            if (staff == null)
                throw new UnauthorizedAccessException("Invalid account, please check your email or create new account.");

            if (staff.IsDeleted)
                throw new UnauthorizedAccessException("Your account has been deleted. Please contact the administrator.");

            if (!staff.IsActived)
                throw new UnauthorizedAccessException("Your account has not been activated. Please check your email to active.");

            bool isValidPassword = BCrypt.Net.BCrypt.Verify(loginDTO.Password, staff.HashPassword);

            if (!isValidPassword)
                throw new UnauthorizedAccessException("Invalid password. Please try again.");

            var token = GenerateJwtToken(staff);

            return token;
        }

        private string GenerateJwtToken(Staff user)
        {
            var jwtKey = Environment.GetEnvironmentVariable("JWT_SECRET_KEY") ?? _config["Jwt:Key"];
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("staffId", user.Id.ToString()),
                new Claim("email", user.Email),
                new Claim("role", user.Role.ToString())
            };

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(int.Parse(_config["Jwt:ExpireMinutes"] ?? "60")),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<bool> ResetPasswordAsync(ResetPasswordDTO resetPasswordDTO)
        {
            var staff = await _authRepository.GetByEmailAsync(resetPasswordDTO.Email);

            if (staff == null || staff.IsDeleted == true || staff.IsActived == false)
                return false;

            var token = GenerateResetPasswordToken(staff.Id);
            var resetLink = $"http://localhost:5208/api/auth/reset-password?token={token}";

            await _emailService.SendEmailAsync(staff.Email, "Reset Password",
                $"Click the following link to reset your password to default.: <a href='{resetLink}'>Reset Password</a>");

            return true;
        }

        public async Task<bool> ResetPasswordFromTokenAsync(string token)
        {
            var jwtKey = Environment.GetEnvironmentVariable("JWT_SECRET_KEY") ?? _config["Jwt:Key"];
            var handler = new JwtSecurityTokenHandler();

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey)),
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true
            };

            try
            {
                var principal = handler.ValidateToken(token, tokenValidationParameters, out var validatedToken);
                var staffIdClaim = principal.FindFirst("staffId");
                if (staffIdClaim == null) return false;

                var staffId = Guid.Parse(staffIdClaim.Value);
                var staff = await _authRepository.GetByIdAsync(staffId);
                if (staff == null || staff.IsDeleted) return false;

                staff.HashPassword = BCrypt.Net.BCrypt.HashPassword("123456");
                return await _authRepository.SaveChangesAsync();
            }
            catch
            {
                return false;
            }
        }

        private string GenerateResetPasswordToken(Guid staffId)
        {
            var jwtKey = Environment.GetEnvironmentVariable("JWT_SECRET_KEY") ?? _config["Jwt:Key"];
            var expireMinutes = 15;

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim("staffId", staffId.ToString()),
                new Claim("purpose", "reset_password")
            };

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(expireMinutes),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
