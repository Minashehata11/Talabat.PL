using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities.Identity;

namespace Talabat.Repository.IdentityData
{
    public static class SeedUser
    {
        public async static Task CreateUser(UserManager<AppUser> userManger)
        {
            if(!userManger.Users.Any())
            {
                AppUser user = new AppUser()
                {
                    FullName = "Mina Shehata",
                    PhoneNumber = "0155515",
                    Email = "Mina@Gmail.com",
                    UserName = "Mina"

                };
             await  userManger.CreateAsync(user,"P@ssW0rd");
            }
        }
    }
}
