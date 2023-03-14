using IslamicSchool.Entities;

namespace IslamicSchool.DataTransferObjects
{
    public class GetuserWithRolesDto
    {
        public Guid id { get; set; }
        public ICollection<string> Roles { get; set; }

    }
}
