using BookStoreAPI.Services.StaffService.Entities;

namespace BookStoreAPI.Services.AuthService.Repositories
{
    public interface IAuthRepository
    {
        Task<Staff?> GetByEmailAsync(string email);
        Task<Staff?> GetByPhoneAsync(string phone);
        Task<Staff?> GetByCitizenIdentificationAsync(string citizenIdentification);
        Task AddSync(Staff staff);
        Task<bool> SaveChangesAsync();
        Task<Staff?> GetByIdAsync(Guid id);
    }
}
