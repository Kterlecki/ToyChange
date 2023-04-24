
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Infrastructure;
using ToyChange.Data;
using ToyChange.Models;

namespace ToyChange;

public class Seed
{
    private readonly ApplicationDbContext _dataContext;

    public Seed(ApplicationDbContext dataContext )
    {
        _dataContext = dataContext;
    }
    public async void SeedDataContext(string passwordSecret)
    {
        if(!_dataContext.Item.Any())
        {
            var user = new User()
            {
                Email = "123@gmail.com",
                UserName = "John123",
                PasswordHash = "abc"
            };
            var password = passwordSecret;
            var userManager = _dataContext.GetService<UserManager<IdentityUser>>();
            await userManager.CreateAsync(user, password);
            var items = new List<Item>()
            {
                new Item{
                    Title = "Porsche 911",
                    Description = "Porsche 911 lego replica. Great model for all Porsche lovers",
                    Price = 50,
                    ImageUrl = "https://www.lego.com/cdn/cs/set/assets/blt468d63d0eb6c81a8/42096.jpg",
                    ItemCategory = Data.Enums.ItemCategory.Technic,
                    Order = new Order{OrderDate = DateTime.Now}
                },
                new Item{
                    Title = "Jurassic park",
                    Description = "Jurassic world themed Lego Set. Contains a T-rex, buy at your own risk!!!!",
                    Price = 90,
                    ImageUrl = "https://fs-prod-cdn.nintendo-europe.com/media/images/10_share_images/games_15/nintendo_switch_4/H2x1_NSwitch_LegoJurassicWorld_image1280w.jpg",
                    ItemCategory = Data.Enums.ItemCategory.Jurassic_World,
                    Order = new Order{OrderDate = DateTime.Now}
                },
                new Item{
                    Title = "McLaren Formula 1",
                    Description = "This LEGO Technic F1 set for adults features a detailed model replica car of McLaren’s 2022 F1 car, perfect for car enthusiasts and LEGO collectors",
                    Price = 10,
                    ImageUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRPUzKbtysa_VByyfevU-3Yt21Ovlp_QUJFattLTAiYYU-vTjd5tTPoLywUf0ZVtjYMuu0&usqp=CAU",
                    ItemCategory = Data.Enums.ItemCategory.City,
                    Order = new Order{OrderDate = DateTime.Now}
                }
            };
            var order = new Order()
            {
                OrderDate = DateTime.Now,
                Item = items[0],
                User = user
            };
            //_dataContext.User.Add(user);
            _dataContext.Item.AddRange(items);
            _dataContext.Order.Add(order);
            _dataContext.SaveChanges();
        }
    }
}