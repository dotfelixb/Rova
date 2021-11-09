using System;

namespace Rova.Model.Domain
{
    public class Lead : DbAudit
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string DisplayName { get; set; }
        public string Status { get; set; }
        public string Gender { get; set; }
        public string Source { get; set; }
        public Guid Campaign { get; set; }
        public string LeadType { get; set; } = "Company";
        public string Email { get; set; }
    }
    
    public class LeadExtended : Lead
    {
        public string CampaignName { get; set; }  
    }

    public class LeadAuditLog : DbAuditLog
    {
        
    }
}