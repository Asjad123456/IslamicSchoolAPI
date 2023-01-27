using IslamicSchool.Entities;
using IslamicSchool.Helpers;
using IslamicSchool.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace IslamicSchool.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<AppUser> userManager;

        public UserRepository(UserManager<AppUser> userManager)
        {
            this.userManager = userManager;
        }

        public async Task<PagedList<AppUser>> GetUsers(UserParams userParams)
        {
            var query = userManager.Users
                .Include(r => r.UserRoles)
                .ThenInclude(r => r.Role)
                .OrderBy(u => u.UserName)
                .Select(u => new
                {
                    u.Id,
                    UserName = u.UserName,
                    Roles = u.UserRoles.Select(r => r.Role.Name).ToList()
                })
                .AsNoTracking();
            return await PagedList<AppUser>.CreateAsync((IQueryable<AppUser>)query, userParams.PageNumber, userParams.PageSize);
        }
    }
}
