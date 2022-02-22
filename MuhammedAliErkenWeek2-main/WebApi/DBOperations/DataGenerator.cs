using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WebApi.Common;
using WebApi.Entities;

namespace WebApi.DBOperations
{
    public class DataGenerator
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context =
                   new BookShopDB(serviceProvider
                       .GetRequiredService<DbContextOptions<BookShopDB>>()))
            {
                if (context.Users.Any()) // Db'de User var mı?
                    return;

                context.Users.AddRange(
                    new User
                    {
                        Email = "mahmut.tuncer@mail.com",
                        FirstName = "Mahmut",
                        LastName = "Tuncer",
                        UserRole = UserEnum.Specialist
                    },
                    new User
                    {
                        Email = "nihat.dogan@mail.com",
                        FirstName = "Nihat",
                        LastName = "Doğan",
                        UserRole = UserEnum.Specialist
                    },
                    new User
                    {
                        Email = "ismail.turut@mail.com",
                        FirstName = "İsmail",
                        LastName = "Türüt",
                        UserRole = UserEnum.Specialist
                    },
                    new User
                    {
                        Email = "selcuk.bilgen@mail.com",
                        FirstName = "Selçuk",
                        LastName = "Bilgen",
                        UserRole = UserEnum.Client
                    },
                    new User
                    {
                        Email = "hakki.bulut@mail.com",
                        FirstName = "Hakkı",
                        LastName = "Bulut",
                        UserRole = UserEnum.Client
                    }
                );

                context.SaveChanges();
            }
        }
    }
}