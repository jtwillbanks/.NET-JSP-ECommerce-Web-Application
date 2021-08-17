using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Providers
{
    public class UserProvider : IUserProvider
    {
        private readonly UserManager<IdentityUser> _userManager;

        public UserProvider(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        public string GetUserId(string email)
        {
            var user = _userManager.Users.FirstOrDefault(x => x.Email == email);
            return user != null ? user.Id : string.Empty;
        }
    }
}
