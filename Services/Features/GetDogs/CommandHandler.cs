using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using DB.Models;
using MediatR;
using Services.Interfaces;

namespace Services.Features.GetDogs
{
    public partial class GetDogs
    {
        public class CommandHandler : IRequestHandler<Command, List<Dog>>
        {
            private readonly IMediator _mediator;
            private readonly IDogsRepository _repository;

            public CommandHandler(IMediator mediator, IDogsRepository repository)
            {
                _mediator = mediator;
                _repository = repository;
            }
            
            public async Task<List<Dog>> Handle(Command request, CancellationToken cancellationToken)
            {
                return await _repository.Get(request.attribute, request.order, request.pageNumber, request.pageSize);
            }
        }
    }
}