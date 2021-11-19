using System;

namespace Rova.Model.Domain
{
    public class Lead : DbAudit
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTimeOffset? BirthAt { get; set; } = null;
        public string DisplayName { get; set; }
        public string Gender { get; set; }
        public string Source { get; set; }
        public Guid Campaign { get; set; }
        public string Status { get; set; }
        public string LeadType { get; set; } = "Company";
        public string Company { get; set; }
        public string Phone { get; set; }
        public string Mobile { get; set; }
        public string Website { get; set; }
        public string Email { get; set; }
        public string AddressType { get; set; }
        public string AddressStreet { get; set; }
        public string AddressCity { get; set; }
        public string AddressState { get; set; }
        public string Market { get; set; }
        public string Industry { get; set; }
        public bool Subscribed { get; set; }
    }
    
    public class LeadExtended : Lead
    {
        public string CampaignName { get; set; }  
    }

    public class LeadAuditLog : DbAuditLog
    {
        
    }
}