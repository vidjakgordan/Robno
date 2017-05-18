﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Robno.Models.Auth;

namespace Robno.Models.Auth
{
    public class BusinessLayer
    {
        public UserStatus GetUserValidity(UserDetails u)   //from lab23 onw
        {
            RobnoContext db = new RobnoContext();

            User user = db.Users.Where(p => p.Username == u.UserName).SingleOrDefault();

                

            if (u.UserName == user.Username && PasswordStorage.VerifyPassword(u.UserName, user.Password) && user.isAdmin==true)
            {
                return UserStatus.AuthenticatedAdmin; //enum
            }
            else if (u.UserName == user.Username && PasswordStorage.VerifyPassword(u.UserName, user.Password)  && user.isAdmin==false)
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
