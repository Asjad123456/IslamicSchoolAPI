using IslamicSchool.Entities;
using IslamicSchool.Helpers;

namespace IslamicSchool.Interfaces
{
    public interface IUserRepository
    {
        Task<PagedList<AppUser>> GetUsers(UserParams userParams);
    }
}
