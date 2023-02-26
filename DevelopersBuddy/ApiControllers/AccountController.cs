using DevelopersBuddyProject.ServiceLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DevelopersBuddy.ApiControllers
{
    public class AccountController : ApiController
    {
        readonly IUsersService userService;
        public AccountController(IUsersService us)
        {
            this.userService = us;
        }

        public string Get(string email)
        {
            if (this.userService.GetUsersByEmail(email) != null)
            {
                return "Found";
            }
            else
            {
                return "Not Found";
            }

        }
    }
}
