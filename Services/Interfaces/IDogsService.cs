using System.Collections.Generic;
using System.Threading.Tasks;
using DB.Models;

namespace Services.Interfaces
{
    public interface IDogsRepository
    {
        public Task<List<Dog>> Get(string attribute, string order, int pageNumber, int pageSize);
        
        public Task<Dog> Add(Dog dog);
    }
}