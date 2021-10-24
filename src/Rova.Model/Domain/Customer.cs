using System;
using System.Text.Json.Serialization;

namespace Rova.Model.Domain
{
    public class Customer : DbAudit
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTimeOffset? BirthAt { get; set; } = null;
        public string Gender { get; set; }
        public string DisplayName { get; set; }
        public string Company { get; set; }
        public string Phone { get; set; }
        public string Mobile { get; set; }
        public string Website { get; set; }
        public string Email { get; set; }
        public string CustomerType { get; set; } = "Company";
        public Guid? FromLead { get; set; } = null;

        public bool SubCustomer { get; set; }
        public Guid? ParentCustomer { get; set; } = null;
        public bool BillParent { get; set; }
        public bool IsInternal { get; set; }

        public string BillingStreet { get; set; }
        public string BillingCity { get; set; }
        public string BillingState { get; set; }

        public string ShippingStreet { get; set; }
        public string ShippingCity { get; set; }
        public string ShippingState { get; set; }
        public bool ShipBilling { get; set; }

        public string PreferredMethod { get; set; }
        public string PreferredDelivery { get; set; }
        public decimal? OpeningBalance { get; set; } = null;
        public DateTimeOffset? OpeningBalanceAt { get; set; } = null;

        public string TaxId { get; set; }
        public bool TaxExempted { get; set; }
    }

    public class CustomerAuditLog : DbAuditLog
    {

    }
}

