using System;
using MediatR;
using Rova.Model.Domain;

namespace Rova.Core.Features.Leads.CreateLead
{
    public class CreateLeadCommand : Lead, IRequest<SingleResult<Guid>>
    {
        public new string Campaign { get; set; }
    }
}