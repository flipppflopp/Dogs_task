using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using DB.Models;
using MediatR;

namespace Services.Features.AddDog
{
    public partial class AddDog
    {
        public class Command : Dog, IRequest<Dog>
        {
            
        }
    }
}