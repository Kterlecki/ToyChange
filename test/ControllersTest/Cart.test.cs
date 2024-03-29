
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using ToyChange.Controllers;
using ToyChange.Data;
using ToyChange.Data.Enums;
using ToyChange.Models;
using Xunit;

namespace ToyChange.Tests.ControllersTest
{
    public class CartTests
    {
        private static ApplicationDbContext GetDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "cart" + Guid.NewGuid().ToString())
                .Options;

            var databaseContext = new ApplicationDbContext(options);
            databaseContext.Database.EnsureCreated();
            if (databaseContext.BlogPost.Count() < 0)
            {
                for (int i = 0; i < 10; i++)
                {
                    databaseContext.Item.Add(
                    new Item()
                    {
                        ItemId = 0,
                        Title = "Test Item Title",
                        Description = "Test Item Description",
                        Price = 100,
                        ItemCategory = ItemCategory.Technic,
                        ImageUrl = "https://upload.wikimedia.org/wikipedia/en/9/95/Test_image.jpg"
                    });
                    databaseContext.SaveChanges();
                }
            }
            return databaseContext;
        }

        [Fact]
        public async Task CartAdd_Cart_ReturnsNotNull()
        {
            //Act
            var item = new Item()
            {
                ItemId = 10,
                Title = "Test Item Title",
                Description = "Test Item Description",
                Price = 100,
                ItemCategory = ItemCategory.Technic,
                ImageUrl = "https://upload.wikimedia.org/wikipedia/en/9/95/Test_image.jpg"
            };
            var mockDb = GetDbContext();
            var controller = new ItemsController(mockDb);
            var cartController = new CartController(mockDb);

            //Arrange
            var result = await controller.Create(item);

            var cartResult = cartController.Add(10);

            //Assert
            Assert.NotNull(cartResult);
            Assert.IsAssignableFrom<RedirectToActionResult>(result);
        }

        [Fact]
        public async Task CartAdd_Cart_ReturnsNotFoundAsync()
        {
            ////Act

            //var mockDb = GetDbContext();

            //var cartController = new CartController(mockDb);

            ////Arrange

            //var cartResult = await cartController.Add(50);

            ////var result = Task.FromResult<TaskStatus>(status);
            ////Assert
            ////Assert.Null(cartResult);
            ////Assert.Equal(cartResult, TaskStatus.Faulted);
            //Assert.IsType<InvalidOperationException>(cartResult);

        }

        [Fact]
        public void CartRemove_Cart_ReturnsNotNull()
        {
            //Act
            var item = new Item()
            {
                ItemId = 10,
                Title = "Test Item Title",
                Description = "Test Item Description",
                Price = 100,
                ItemCategory = ItemCategory.Technic,
                ImageUrl = "https://upload.wikimedia.org/wikipedia/en/9/95/Test_image.jpg"
            };
            var mockDb = GetDbContext();
            var controller = new ItemsController(mockDb);
            var cartController = new CartController(mockDb);

            //Arrange

            var createItem =controller.Create(item);

            var cartAdd = cartController.Add(10);
            var cartRemove =  cartController.Remove(10);

            //Assert
            Assert.NotNull(cartRemove);
            Assert.IsAssignableFrom<Task<IActionResult>>(cartRemove);
        }
    }
}
