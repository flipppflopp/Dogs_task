using System.Collections.Generic;
using DB.Models;
using MediatR;

namespace Services.Features.GetDogs
{
    public partial class GetDogs
    {
        public class Command : IRequest<List<Dog>>
        {
            public string attribute { get; set; } = "";
            public string order { get; set;} = "";
            public int pageNumber { get; set;}
            public int pageSize { get; set;}
        }
    }
}