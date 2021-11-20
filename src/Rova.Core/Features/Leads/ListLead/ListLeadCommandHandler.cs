using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Rova.Data.Repository;
using Rova.Model.Domain;

namespace Rova.Core.Features.Leads.ListLead
{
    public class ListLeadCommandHandler : IRequestHandler<ListLeadCommand, ListResult<LeadExtended>>
    {
        private readonly ILeadRepository _leadRepository; 
        
        public ListLeadCommandHandler(ILeadRepository leadRepository)
        {
            _leadRepository = leadRepository; 
        }
        
        public async Task<ListResult<LeadExtended>> Handle(
            ListLeadCommand request
            , CancellationToken cancellationToken)
        {
            var rst = await _leadRepository.List(request.Offset, request.Limit);

            return new ListResult<LeadExtended>
            {
                Data = rst,
                Ok = true,
                Type = "list"
            };
        }
    }
}