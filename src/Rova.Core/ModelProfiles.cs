using AutoMapper;
using Rova.Core.Extensions;
using Rova.Core.Features.Customers.CreateCustomer;
using Rova.Model.Domain;

namespace Rova.Core
{
    public class ModelProfiles : Profile
    {
        public ModelProfiles()
        {
            CreateMap<CreateCustomerCommand, Customer>()
                .ForMember(d => d.ParentCustomer,
                    opt => opt.MapFrom(s => s.ParentCustomer.ToGuid()))
                .ForMember(d => d.OpeningBalanceAt,
                    opt => opt.MapFrom(s => s.OpeningBalanceAt.ToDateTimeTz()))
                .ForMember(d => d.OpeningBalance,
                    opt => opt.MapFrom(s => s.OpeningBalance.ToDecimal()));

        }
    }
}

