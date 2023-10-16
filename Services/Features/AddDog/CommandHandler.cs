using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using DB.Models;
using MediatR;
using Services.Interfaces;

namespace Services.Features.AddDog
{
    public partial class AddDog
    {
        public class CommandHandler : IRequestHandler<Command, Dog>
        {
            private readonly IMediator _mediator;
            private readonly IDogsRepository _repository;

            public CommandHandler(IMediator mediator, IDogsRepository repository)
            {
                _mediator = mediator;
                _repository = repository;
            }
            
            public async Task<Dog> Handle(Command request, CancellationToken cancellationToken)
            {
                return await _repository.Add(request);
            }
        }
    }
}