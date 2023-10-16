using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using DB;
using DB.Models;
using Microsoft.EntityFrameworkCore;
using Services.Interfaces;

namespace Services.Services
{
    public class DogsService : IDogsRepository
    {
        private ApplicationContext db;

        public DogsService(ApplicationContext context)
        {
            db = context;
        }

        public async Task<List<Dog>> Get(string attribute, string order, int pageNumber, int pageSize)
        {
                List<Dog> dogs = await db.Dogs.ToListAsync();
            
                Type type = typeof(Dog);

                PropertyInfo? propertyInfo = type.GetProperty(attribute);

                if (propertyInfo != null)
                {
                    if (order == "desc")
                    {
                        dogs = dogs.OrderByDescending(d => propertyInfo.GetValue(d)).ToList();
                    }
                    else if (order == "asc" || order == "")
                    {
                        dogs = dogs.OrderBy(d => propertyInfo.GetValue(d)).ToList();
                    }
                    else
                    {
                        throw new Exception("Wrong order definition.");
                    }
                }
                else if(attribute != "")
                {
                    throw new Exception($"Property with name {attribute} was not found.");
                }

                if (pageNumber > 0 && pageSize > 0)
                {
                    int startIndex = pageSize * (pageNumber - 1);

                    if (dogs.Count - startIndex < pageSize)
                    {
                        int cnt = dogs.Count - startIndex;
                        if (cnt == 1)
                        {
                            return  dogs.Skip(startIndex).Take(1).ToList();
                        }
                        else return  dogs.GetRange(startIndex, cnt);
                    }
                    else return  dogs.GetRange(startIndex, pageSize);
                }
                else if (pageNumber < 0 || pageSize < 0)
                {
                    throw new Exception("Invalid paging params.");
                }
                else return dogs;
        }

        public async Task<Dog> Add(Dog dog)
        {
            if (db.Dogs.SingleOrDefault(d => d.name == dog.name) == null)
            {
                db.Dogs.Add(dog);
                await db.SaveChangesAsync();
            }
            else
            {
                throw new Exception("This entity is already exist.");
            }

            return dog;
        }
    }
}