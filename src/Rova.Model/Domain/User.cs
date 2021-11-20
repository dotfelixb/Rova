using System;

namespace Rova.Model.Domain
{
    public class User : DbAudit
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string DisplayName { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public bool Enabled { get; set; } = true;
    }

    public class UserExtended : User
    {
        private new string PasswordHash { get; set; }
    }

    public class UserAuditLog : DbAuditLog
    {
        
    }
}