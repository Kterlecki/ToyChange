using FluentAssertions;
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

namespace ToyChange.Tests;

public class ItemTests
{
    private static ApplicationDbContext GetDbContext()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        var databaseContext = new ApplicationDbContext(options);
        databaseContext.Database.EnsureCreated();
        if (databaseContext.Item.Count() < 0)
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
    //public void DbSetup()
    //{
    //    var db = GetDbContext();
    //    if (db.Item.Count() < 0)
    //    {
    //            db.Item.Add(
    //                new Item()
    //                {
    //                    ItemId = 0,
    //                    Title = "Test Item Title",
    //                    Description = "Test Item Description",
    //                    Price = 100,
    //                    ItemCategory = ItemCategory.Technic,
    //                    ImageUrl = "https://upload.wikimedia.org/wikipedia/en/9/95/Test_image.jpg"

    //                });
    //            db.SaveChanges();

    //        var item = new List<Item>
    //        {
    //            new Item()
    //                {
    //                    ItemId = 0,
    //                    Title = "Test Item Title",
    //                    Description = "Test Item Description",
    //                    Price = 100,
    //                    ItemCategory = ItemCategory.Technic,
    //                    ImageUrl = "https://upload.wikimedia.org/wikipedia/en/9/95/Test_image.jpg"

    //                },
    //            new Department()
    //            {
    //                Id = 5,
    //                Name ="TestDepartment5",
    //                Rate = 56.98M,
    //                CreateDateTime = new System.DateTime(),
    //                CreatedByUserId = "UserId" }

    //        };
    //        db.Item.AddRange(item);
    //        db.SaveChanges();

    //    }
    [Fact]
    public async Task ItemIndex_Create_ReturnsAViewResult()
    {
        //Arrange
        var mockDb = GetDbContext();
        var controller = new ItemsController(mockDb);
        //Act

        var result = await  controller.Index("","");

        //Assert
        Assert.NotNull(result);
        Assert.IsAssignableFrom<ViewResult>(result);
    }

    [Fact]
    public void Create_Item_ReturnSuccess()
    {
        //Arrange
        var mockDb = GetDbContext();
        var controller = new ItemsController(mockDb);
        //Act
        var result =  controller.Create();
        //Assert
        Assert.NotNull(result);
        Assert.IsAssignableFrom<ViewResult>(result);
    }

    [Fact]
    public async Task Create_ItemWithDetails_ReturnSuccess()
    {
        //Arrange
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
        //Act

        var result = await controller.Create(item);

        //Assert
        Assert.NotNull(result);
        Assert.Equal(10, item.ItemId);
        Assert.Equal(100, item.Price);
        Assert.IsAssignableFrom<RedirectToActionResult>(result);
    }

    [Fact]
    public async Task Details_Item_ReturnNotFound()
    {
        //Arrange
        var mockDb = GetDbContext();
        var controller = new ItemsController(mockDb);
        //Act

        var result = await controller.Details(100);
        //Assert
        Assert.NotNull(result);
        Assert.IsAssignableFrom<NotFoundResult>(result);
    }

    [Fact]
    public async Task Details_Item_ReturnFound()
    {
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
        //Act

        var result = await controller.Create(item);

        var details = await controller.Details(10);

        //Assert
        Assert.NotNull(details);
        Assert.IsAssignableFrom<ViewResult>(details);
    }

    [Fact]
    public async Task Edit_Item_ReturnFound()
    {
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
        //Act

        var result = await controller.Create(item);

        var edit = await controller.Edit(10);

        //Assert
        Assert.NotNull(edit);
        Assert.IsAssignableFrom<ViewResult>(edit);
    }
    [Fact]
    public async Task Edit_ItemObject_ReturnActionResult()
    {
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
        //Act

        var result = await controller.Create(item);

        var edit = await controller.Edit(10,item);

        //Assert
        Assert.NotNull(edit);
        Assert.IsAssignableFrom<RedirectToActionResult>(edit);
    }

    [Fact]
    public async Task Delete_Item_ReturnFound()
    {
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
        //Act

        var result = await controller.Create(item);

        var delete = await controller.Delete(10);

        //Assert
        Assert.NotNull(delete);
        Assert.IsAssignableFrom<ViewResult>(delete);
    }

    [Fact]
    public async Task DeleteConfirmed_Item_ReturnSuccess()
    {
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
        //Act

        var result = await controller.Create(item);

        var delete = await controller.DeleteConfirmed(10);

        //Assert
        Assert.NotNull(delete);
        Assert.IsAssignableFrom<RedirectToActionResult>(delete);
    }

    [Fact]
    public async Task DeleteConfirmed_Item_ReturnNotFound()
    {
        //Arrange
        var mockDb = GetDbContext();
        var controller = new ItemsController(mockDb);
        //Act
        var delete = await controller.Delete(200);

        //Assert
        Assert.NotNull(delete);
        Assert.IsAssignableFrom<NotFoundResult>(delete);
    }
}