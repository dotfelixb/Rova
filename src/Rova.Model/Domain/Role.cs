using System;

namespace Rova.Model.Domain
{
    public class Role : DbAudit
    {
        public Guid Id { get; set; }
        public string Rolename { get; set; }
        public bool Enabled { get; set; } = true;
    }

    public class RoleExtended : Role
    {
        
    }

    public class RoleAuditLog : DbAuditLog
    {
        
    }
}