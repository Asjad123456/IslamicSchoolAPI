using IslamicSchool.Entities;
using IslamicSchool.Helpers;
using IslamicSchool.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace IslamicSchool.Repository
{
    public class DeanRepository : IDeanRepository
    {
        private readonly UserManager<AppUser> userManager;

        public DeanRepository(UserManager<AppUser> userManager)
        {
            this.userManager = userManager;
        }

        public async Task<PagedList<AppUser>> GetUsersWithRoles(UserParams userParams)
        {
            var query = userManager.Users
                 .AsNoTracking();
            return await PagedList<AppUser>.CreateAsync(query, userParams.PageNumber, userParams.PageSize);
        }
    }
}
