using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Rova.Data.Repository;
using Rova.Model.Domain;

namespace Rova.Core.Features.Leads.ListLead
{
    public class ListLeadCommand : IRequest<ListResult<LeadExtended>>
    {
        public int Offset { get; set; } = 0;
        public int Limit { get; set; } = 1000;
    }
    
    public class ListLeadCommandHandler : IRequestHandler<ListLeadCommand, ListResult<LeadExtended>>
    {
        private readonly ILeadRepository _leadRepository;
        private readonly IMapper _mapper;
        
        public ListLeadCommandHandler(
            ILeadRepository leadRepository
            , IMapper mapper)
        {
            _leadRepository = leadRepository;
            _mapper = mapper;
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