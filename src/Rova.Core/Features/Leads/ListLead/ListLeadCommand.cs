using MediatR;
using Rova.Model.Domain;

namespace Rova.Core.Features.Leads.ListLead
{
    public class ListLeadCommand : DbList, IRequest<ListResult<LeadExtended>>
    {
    }
}