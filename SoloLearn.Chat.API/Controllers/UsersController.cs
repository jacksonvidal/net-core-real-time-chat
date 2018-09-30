using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SoloLearn.Chat.API.Models;
using SoloLearn.Chat.Core.Entities;
using SoloLearn.Chat.Core.Service;

namespace SoloLearn.Chat.API.Controllers
{
    [Produces("application/json")]
    [Route("api/Users")]
    public class UsersController : Controller
    {
        public IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Create new User based on a form page
        /// </summary>
        /// <param name="user">User viewModel</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Post([FromForm]UserViewModel user)
        {
            try
            {
                _userService.Add(new User
                {
                    Email = user.Email,
                    PasswordHash = user.Password,
                    UserName = user.UserName
                });

                return Redirect("/login.html");

            }
            catch (Exception ex)
            {

                return Redirect(string.Format("/login.html?m={0}", ex.Message));
            }
        }

        /// <summary>
        /// Authenticate an User
        /// </summary>
        /// <param name="loginData">User ViewModel</param>
        /// <returns></returns>
        [HttpPost("/Login")]
        public ActionResult Login([FromForm]UserViewModel loginData)
        {
            var user = new User
            {
                Email = loginData.Email,
                PasswordHash = loginData.Password,
                UserName = loginData.UserName
            };

            user = _userService.Authenticate(user);

            if (user != null)
            {

                var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme, ClaimTypes.NameIdentifier, ClaimTypes.Name);

                identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.UserName));
                identity.AddClaim(new Claim(ClaimTypes.Name, user.Email));

                var principal = new ClaimsPrincipal(identity);

                HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, new AuthenticationProperties { IsPersistent = true });

                return Redirect("/index.html");
            }
            else
                return Redirect("/login.html?m=failed");
        }

        /// <summary>
        /// Who Am I 
        /// </summary>
        /// <returns>Meesage</returns>
        [HttpGet]
        public JsonResult Get()
        {
            return Json(new { userName = HttpContext.User.Identity.Name });
        }

        /// <summary>
        /// Singout form the chat
        /// </summary>
        /// <returns></returns>
        [HttpGet("/Logout")]
        public ActionResult Logout()
        {
            HttpContext.SignOutAsync();

            return Redirect("login.html");
        }
    }
}