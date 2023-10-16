using System;
using System.Collections.Generic;
using System.Linq;
using DB;
using DB.Models;
using Microsoft.EntityFrameworkCore;
using Services.Services;
using Xunit;

namespace Tests
{
    public class UserServiceTests : IDisposable
    {
        private readonly ApplicationContext _context;
        private readonly DogsService _userService;

        public UserServiceTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            _context = new ApplicationContext(options);
            _userService = new DogsService(_context);
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        [Fact]
        public void Get_DogsSortedByNameInAscendingOrder_ReturnsSortedDogs()
        {
            // Arrange
            _context.Dogs.Add(new Dog { name = "Buddy", color = "black"});
            _context.Dogs.Add(new Dog { name = "Fido", color = "white" });
            _context.Dogs.Add(new Dog { name = "Max", color = "ginger" });
            _context.SaveChanges();

            // Act
            var result = _userService.Get("name", "asc", 1, 3);

            // Assert
            Assert.Equal(3, result.Result.Count);
            Assert.Equal("Buddy", result.Result[0].name);
            Assert.Equal("Fido", result.Result[1].name);
            Assert.Equal("Max", result.Result[2].name);
        }
        
        [Fact]
        public void Get_DogsSortedByNameInDescendingOrderWithoutPaging_ReturnsUnpagedDogs()
        {
            // Arrange
            _context.Dogs.Add(new Dog { name = "Buddy", color = "black"  });
            _context.Dogs.Add(new Dog { name = "Fido", color = "black"  });
            _context.Dogs.Add(new Dog { name = "Max", color = "black"  });
            _context.SaveChanges();

            // Act
            var result = _userService.Get("name", "desc", 0, 0);

            // Assert
            Assert.Equal(3, result.Result.Count);
            Assert.Equal("Max", result.Result[0].name);
            Assert.Equal("Fido", result.Result[1].name);
            Assert.Equal("Buddy", result.Result[2].name);
        }

        [Fact]
        public void Get_DogsSortedByNonExistingAttribute_ThrowsException()
        {
            // Arrange
            _context.Dogs.Add(new Dog { name = "Buddy", color = "black"  });
            _context.Dogs.Add(new Dog { name = "Fido", color = "black"  });
            _context.Dogs.Add(new Dog { name = "Max", color = "black"  });
            _context.SaveChanges();

            // Act
            var result = _userService.Get("non_existing_attribute", "asc", 1, 3);

            Assert.Equal("One or more errors occurred. (Property with name non_existing_attribute was not found.)", result.Exception.Message);
        }

        [Fact]
        public void Get_DogsSortedByNonExistingSorting_ThrowsException()
        {
            // Arrange
            _context.Dogs.Add(new Dog { name = "Buddy", color = "black"  });
            _context.Dogs.Add(new Dog { name = "Fido", color = "black"  });
            _context.Dogs.Add(new Dog { name = "Max", color = "black"  });
            _context.SaveChanges();

            // Act
            var result = _userService.Get("name", "asdada", 1, 3);

            Assert.Equal("One or more errors occurred. (Wrong order definition.)", result.Exception.Message);
        }
        
        [Fact]
        public void Get_DogsSortedByEmptySorting_ThrowsException()
        {
            // Arrange
            _context.Dogs.Add(new Dog { name = "Fido", color = "black"  });
            _context.Dogs.Add(new Dog { name = "Max", color = "black"  });
            _context.Dogs.Add(new Dog { name = "Buddy", color = "black"  });
            _context.SaveChanges();

            // Act
            var result = _userService.Get("name", "", 1, 3);

            Assert.Equal("Buddy", result.Result[0].name);
            Assert.Equal("Fido", result.Result[1].name);
            Assert.Equal("Max", result.Result[2].name);
        }





        [Fact]
        public void Add_NonExistingDog_AddsDogToDatabase()
        {
            // Arrange
            var newDog = new Dog { name = "Buddy", color = "black" };

            // Act
            var result = _userService.Add(newDog);

            // Assert
            //Assert.Equal("",result.Result);
            Assert.Equal(1, _context.Dogs.Count());
        }

        [Fact]
        public void Add_ExistingDog_ReturnsErrorMessage()
        {
            // Arrange
            var existingDog = new Dog { name = "Buddy", color = "black" };
            _context.Dogs.Add(existingDog);
            _context.SaveChanges();

            // Act
            var result = _userService.Add(existingDog);

            // Assert
            Assert.Equal("One or more errors occurred. (This entity is already exist.)", result.Exception.Message);
            Assert.Equal(1, _context.Dogs.Count());
        }
    }
}
