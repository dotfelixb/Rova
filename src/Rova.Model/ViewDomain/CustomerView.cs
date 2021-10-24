using System;
namespace Rova.Model.ViewDomain
{
    public class CustomerView
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string DisplayName { get; set; }
        public string Phone { get; set; }
        public string Mobile { get; set; }
        public string CustomerType { get; set; } = "Company";

        public DateTimeOffset CreatedAt { get; set; }
    }
}

