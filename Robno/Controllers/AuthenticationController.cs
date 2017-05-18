using System.Web;
using System.Web.Mvc;
using System.Web.Security; //added
using Robno.Models.Auth; //added

namespace Robno.Controllers
{
    
    public class AuthenticationController : Controller
    {
        // GET: Authentication
        public ActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Home"); //ako je vec logiran, onda idemo odma na glavni izbornik
            return View(); // inace view za unos login podataka
        }

        [HttpPost]
        public ActionResult DoLogin(UserDetails u)
        {
            if (ModelState.IsValid)
            {

                BusinessLayer bal = new BusinessLayer();
                UserStatus status = bal.GetUserValidity(u);
                bool IsAdmin = false;
                if (status == UserStatus.AuthenticatedAdmin)
                {
                    IsAdmin = true;
                }
                else if (status == UserStatus.AuthenticatdUser)
                {
                    IsAdmin = false;
                }
                else
                {
                    ModelState.AddModelError("CredentialError", "Invalid Username or Password");
                    return View("Login");
                }
                FormsAuthentication.SetAuthCookie(u.UserName, false);
                
                
                Session["IsAdmin"] = IsAdmin;
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View("Login");
            }
        }

        public ActionResult Logout()
        {
            Session["isAdmin"] = false;
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }
    }
}