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

            await _emailService.SendEmailAsync(staff.Email, "Kích hoạt tài khoản",
                $"Nhấn vào đường dẫn sau để kích hoạt tài khoản: <a href='{activationLink}'>Xác minh tài khoản</a>");

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


    }
}
