using IslamicSchool.Entities;

namespace IslamicSchool.DataTransferObjects
{
    public class AuthResponseDto
    {
        public bool IsAuthSuccessful { get; set; }
        public string? ErrorMessage { get; set; }
        public string? Token { get; set; }
        public IList<string>? Roles { get; set; }
        public AppUser AppUser { get; set; }
    }
}
