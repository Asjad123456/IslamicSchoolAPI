using IslamicSchool.Entities;
using IslamicSchool.Helpers;

namespace IslamicSchool.Interfaces
{
    public interface IDeanRepository
    {
        Task<PagedList<AppUser>> GetUsersWithRoles(UserParams userParams);
    }
}
