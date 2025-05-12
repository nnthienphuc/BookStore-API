using BookStoreAPI.Services.StaffService.Interfaces;
using BookStoreAPI.Services.StaffService.Repositories;

namespace BookStoreAPI.Services.StaffService
{
    public class StaffService : IStaffService
    {
        private readonly IStaffRepository _staffRepository;

        public StaffService(IStaffRepository staffRepository)
        {
            _staffRepository = staffRepository;
        }


    }
}
