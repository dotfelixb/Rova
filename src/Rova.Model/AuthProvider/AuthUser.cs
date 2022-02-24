using Microsoft.AspNetCore.Identity;

namespace Rova.Model.AuthProvider
{
    public class AuthUser : IdentityUser<Guid>
    {
        public Guid InstallId { get; set; }
        public Guid EmployeeId { get; set; }
        public bool Enabled { get; set; }
    }
}