using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Robno.Models.Auth
{
    public class BusinessLayer
    {
        public UserStatus GetUserValidity(UserDetails u)   //from lab23 onw
        {
            if (u.UserName == "Admin" && u.Password == "Admin")
            {
                return UserStatus.AuthenticatedAdmin; //enum
            }
            else if (u.UserName == "Gordan" && u.Password == "Gordan")
            {
                return UserStatus.AuthenticatdUser;   //enum
            }
            else
            {
                return UserStatus.NonAuthenticatedUser;
            }
        }

    }
}

