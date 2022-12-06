using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using ToyChange.Controllers;
using ToyChange.Data;
using Xunit;


namespace ToyChange.Tests.ControllersTest
{
    public class BlogPostTests
    {

        private static ApplicationDbContext GetDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "BlogPosts" + Guid.NewGuid().ToString())
                .Options;

            var databaseContext = new ApplicationDbContext(options);
            databaseContext.Database.EnsureCreated();
            if (databaseContext.BlogPost.Count() < 0)
            {
                for (int i = 0; i < 10; i++)
                {
                    databaseContext.BlogPost.Add(
                        new Models.BlogPost()
                        {
                            BlogId = 0,
                            BlogTitle = "Test Item Title",
                            BlogContent = "Test Item Description",
                            BlogImageUrl = "https://upload.wikimedia.org/wikipedia/en/9/95/Test_image.jpg"

                        });
                    databaseContext.SaveChanges();
                }

            }
            return databaseContext;
        }
        


        [Fact]
        public async Task Index_Create_ReturnsAViewResult()
        {
            //Arrange
            var mockDb = GetDbContext();
            var controller = new BlogPostsController(mockDb);
            //Act

            var result = await controller.Index();


            //Assert
            Assert.NotNull(result);
            Assert.IsAssignableFrom<ViewResult>(result);


        }


        [Fact]
        public void Create_BlogPost_ReturnSuccess()
        {
            //Arrange
            var mockDb = GetDbContext();
            var controller = new BlogPostsController(mockDb);
            //Act

            var result = controller.Create();


            //Assert
            Assert.NotNull(result);
            Assert.IsAssignableFrom<ViewResult>(result);



        }

        [Fact]
        public async Task Create_BlogPostWithDetails_ReturnSuccess()
        {
            //Arrange
            Models.BlogPost blogPost = new Models.BlogPost()
            {
                BlogId = 10,
                BlogTitle = "Test Blog Title",
                BlogContent = "Test Blog Description",
                BlogImageUrl = "https://upload.wikimedia.org/wikipedia/en/9/95/Test_image.jpg"
            };
            var mockDb = GetDbContext();
            var controller = new BlogPostsController(mockDb);
            //Act

            var result = await controller.Create(blogPost);

      

            //Assert
            Assert.NotNull(result);

            Assert.Equal(10, blogPost.BlogId);
            Assert.IsAssignableFrom<RedirectToActionResult>(result);


        }

        [Fact]
        public async Task Details_BlogPost_ReturnNotFound()
        {
            //Arrange
            var mockDb = GetDbContext();
            var controller = new BlogPostsController(mockDb);
            //Act

            var result = await controller.Details(100);


            //Assert
            Assert.NotNull(result);
            Assert.IsAssignableFrom<NotFoundResult>(result);



        }

        [Fact]
        public async Task Details_BlogPost_ReturnFound()
        {
            var blogPost = new Models.BlogPost()
            {
                BlogId = 10,
                BlogTitle = "Test Blog Title",
                BlogContent = "Test Blog Description",
                BlogImageUrl = "https://upload.wikimedia.org/wikipedia/en/9/95/Test_image.jpg"
            };
            var mockDb = GetDbContext();
            var controller = new BlogPostsController(mockDb);
            //Act

            var result = await controller.Create(blogPost);

            var details = await controller.Details(10);


            //Assert
            Assert.NotNull(details);
            Assert.IsAssignableFrom<ViewResult>(details);
        }

        [Fact]
        public async Task Edit_BlogPost_ReturnFound()
        {
            var blogPost = new Models.BlogPost()
            {
                BlogId = 10,
                BlogTitle = "Test Blog Title",
                BlogContent = "Test Blog Description",
                BlogImageUrl = "https://upload.wikimedia.org/wikipedia/en/9/95/Test_image.jpg"
            };
            var mockDb = GetDbContext();
            var controller = new BlogPostsController(mockDb);
            //Act

            var result = await controller.Create(blogPost);

            var edit = await controller.Edit(10);


            //Assert
            Assert.NotNull(edit);
            Assert.IsAssignableFrom<ViewResult>(edit);
        }
        [Fact]
        public async Task Edit_BlogPostObject_ReturnActionResult()
        {
            var blogPost = new Models.BlogPost()
            {
                BlogId = 10,
                BlogTitle = "Test Blog Title",
                BlogContent = "Test Blog Description",
                BlogImageUrl = "https://upload.wikimedia.org/wikipedia/en/9/95/Test_image.jpg"
            };
            var mockDb = GetDbContext();
            var controller = new BlogPostsController(mockDb);
            //Act

            var result = await controller.Create(blogPost);

            var edit = await controller.Edit(10, blogPost);


            //Assert
            Assert.NotNull(edit);
            Assert.IsAssignableFrom<RedirectToActionResult>(edit);
        }

        [Fact]
        public async Task Delete_BlogPost_ReturnFound()
        {
            var blogPost = new Models.BlogPost()
            {
                BlogId = 10,
                BlogTitle = "Test Blog Title",
                BlogContent = "Test Blog Description",
                BlogImageUrl = "https://upload.wikimedia.org/wikipedia/en/9/95/Test_image.jpg"
            };
            var mockDb = GetDbContext();
            var controller = new BlogPostsController(mockDb);
            //Act

            var result = await controller.Create(blogPost);

            var delete = await controller.Delete(10);


            //Assert
            Assert.NotNull(delete);
            Assert.IsAssignableFrom<ViewResult>(delete);
        }

        [Fact]
        public async Task DeleteConfirmed_BlogPost_ReturnSuccess()
        {
            var blogPost = new Models.BlogPost()
            {
                BlogId = 1,
                BlogTitle = "Test Blog Title",
                BlogContent = "Test Blog Description",
                BlogImageUrl = "https://upload.wikimedia.org/wikipedia/en/9/95/Test_image.jpg"
            };
            var mockDb = GetDbContext();
            var controller = new BlogPostsController(mockDb);
            //Act

            var result = await controller.Create(blogPost);

            var delete = await controller.DeleteConfirmed(10);


            //Assert
            Assert.NotNull(delete);
            Assert.IsAssignableFrom<RedirectToActionResult>(delete);
        }


    }
}
