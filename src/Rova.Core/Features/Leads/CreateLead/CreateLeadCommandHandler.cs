using System;
using FluentValidation;
using MediatR;
using Rova.Model.Domain;

namespace Rova.Core.Features.Leads.CreateLead
{
    public class CreateLeadCommand : Lead, IRequest<SingleResult<Guid>>
    {
        public new string Campaign { get; set; }
    }
    
    public class CreateLeadCommandHandler
    {
        
    }

    public class CreateLeadCommandValidtor : AbstractValidator<CreateLeadCommand>
    {
        public CreateLeadCommandValidtor()
        {
            RuleFor(r => r.DisplayName)
                .NotNull();
        }
    }
}