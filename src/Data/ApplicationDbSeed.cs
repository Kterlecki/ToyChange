namespace ToyChange.Data
{
    public class ApplicationDbSeed
    {

        //public static void Seed(IApplicationBuilder applicationBuilder)
        //{
        //    using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
        //    {
        //        var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();
        //        context.Database.EnsureCreated();

        //        if (context.User.FirstOrDefault() == null)
        //        {
        //            context.User.Add(new User { Id= "0", Email = "test@gmail.com", UserName = "test", } );
        //        }

        //        if (context.Item.FirstOrDefault() == null)
        //        {
        //            context.Item.Add(new Item { ItemId = 0, Title = "Test Item Title", Description = "Test Item Description", Price = 100, ItemCategory= ItemCategory.Technic ,ImageUrl = "https://upload.wikimedia.org/wikipedia/en/9/95/Test_image.jpg" });
        //        }

        //        if (context.BlogPost.FirstOrDefault() == null)
        //        {
        //            context.BlogPost.Add(new BlogPost { BlogId = 0, BlogTitle = "Test Blog", BlogContent = "Test Content", BlogImageUrl = "https://upload.wikimedia.org/wikipedia/en/9/95/Test_image.jpg" });
        //        }



        //        if (context.Order.FirstOrDefault() == null)
        //        {
        //            context.Order.Add(new Order { OrderId= 0, OrderDate = DateTime.Now, Id="0", ItemId = 0});
        //        }



        //        context.SaveChanges();

        //    }
        //}


    }
}
