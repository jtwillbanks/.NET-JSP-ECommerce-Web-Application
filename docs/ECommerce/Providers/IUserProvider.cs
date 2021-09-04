using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Providers
{
    public interface IUserProvider
    {
        string GetUserId(string email);
    }
}
