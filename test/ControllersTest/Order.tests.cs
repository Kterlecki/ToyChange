using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToyChange.Controllers;
using ToyChange.Data;
using ToyChange.Models;
using Xunit;

namespace ToyChange.Tests.ControllersTest
{
    public class OrderTests
    {
        private static ApplicationDbContext GetDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var databaseContext = new ApplicationDbContext(options);
            databaseContext.Database.EnsureCreated();
            if (databaseContext.Order.Count() < 0)
            {
                for (int i = 0; i < 10; i++)
                {
                    databaseContext.Order.Add(
                        new Order()
                        {
                            OrderId = 0+i,
                            OrderDate = DateTime.Now,
                            Id = "1",
                            ItemId = 1,
                        });
                    databaseContext.SaveChanges();
                }
            }
            return databaseContext;
        }

        [Fact]
        public async Task OrderIndex_Index_ReturnsAViewResult()
        {
            //Arrange
            var mockDb = GetDbContext();
            var controller = new OrdersController(mockDb);
            //Act

            var result = await controller.Index();

            //Assert
            Assert.NotNull(result);
            Assert.IsAssignableFrom<ViewResult>(result);
        }

        [Fact]
        public void Create_Order_ReturnSuccess()
        {
            //Arrange
            var mockDb = GetDbContext();
            var controller = new OrdersController(mockDb);
            //Act

            var result = controller.Create();

            //Assert
            Assert.NotNull(result);
            Assert.IsAssignableFrom<ViewResult>(result);
        }

        [Fact]
        public async Task Create_OrderWithDetails_ReturnSuccess()
        {
            //Arrange
            var order = new Order()
            {
                OrderId = 10,
                OrderDate = DateTime.Now,
                Id = "1",
                ItemId = 1,
            };
            var mockDb = GetDbContext();
            var controller = new OrdersController(mockDb);
            //Act

            var result = await controller.Create(order);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(10, order.OrderId);
            Assert.IsAssignableFrom<RedirectToActionResult>(result);
        }

        [Fact]
        public async Task Details_Order_ReturnNotFound()
        {
            //Arrange
            var mockDb = GetDbContext();
            var controller = new OrdersController(mockDb);
            //Act

            var result = await controller.Details(100);

            //Assert
            Assert.NotNull(result);
            Assert.IsAssignableFrom<NotFoundResult>(result);
        }

        //[Fact]
        //public async Task Details_Order_ReturnFound()
        //{
        //    //Arrange
        //    var order = new Order()
        //    {
        //        OrderId = 15,
        //        OrderDate = DateTime.Now,
        //        Id = "1",
        //        ItemId = 1,
        //    };
        //    var mockDb = GetDbContext();
        //    var controller = new OrdersController(mockDb);
        //    //Act

        //    var result = await controller.Create(order);

        //    var details = await controller.Details(10);


        //    //Assert
        //    Assert.NotNull(details);
        //    Assert.IsAssignableFrom<ViewResult>(details);
        //}

        [Fact]
        public async Task Edit_Order_ReturnFound()
        {
            //Arrange
            var order = new Order()
            {
                OrderId = 10,
                OrderDate = DateTime.Now,
                Id = "1",
                ItemId = 1,
            };
            var mockDb = GetDbContext();
            var controller = new OrdersController(mockDb);
            //Act

            var result = await controller.Create(order);

            var edit = await controller.Edit(10);

            //Assert
            Assert.NotNull(edit);
            Assert.IsAssignableFrom<ViewResult>(edit);
        }
        [Fact]
        public async Task Edit_OrderObject_ReturnActionResult()
        {
            //Arrange
            var order = new Order()
            {
                OrderId = 10,
                OrderDate = DateTime.Now,
                Id = "1",
                ItemId = 1,
            };
            var mockDb = GetDbContext();
            var controller = new OrdersController(mockDb);
            //Act

            var result = await controller.Create(order);

            var edit = await controller.Edit(10, order);

            //Assert
            Assert.NotNull(edit);
            Assert.IsAssignableFrom<RedirectToActionResult>(edit);
        }

        [Fact]
        public async Task Delete_Order_ReturnNotFound()
        {
          // arrange
            var mockDb = GetDbContext();
            var controller = new OrdersController(mockDb);
            //Act
            var delete = await controller.Delete(10);

            //Assert
            Assert.NotNull(delete);
            Assert.IsAssignableFrom<NotFoundResult>(delete);
        }

        [Fact]
        public async Task DeleteConfirmed_Order_ReturnSuccess()
        {
            //Arrange
            var order = new Order()
            {
                OrderId = 10,
                OrderDate = DateTime.Now,
                Id = "1",
                ItemId = 1,
            };
            var mockDb = GetDbContext();
            var controller = new OrdersController(mockDb);
            //Act

            var result = await controller.Create(order);
            var delete = await controller.DeleteConfirmed(10);

            //Assert
            Assert.NotNull(delete);
            Assert.IsAssignableFrom<RedirectToActionResult>(delete);
        }
    }
}
