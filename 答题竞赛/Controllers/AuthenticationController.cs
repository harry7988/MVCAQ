using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using 答题竞赛.Models;
using 答题竞赛.DAL;
using System.ComponentModel.DataAnnotations;

namespace 答题竞赛.Controllers
{
    public class AuthenticationController : Controller
    {
        QuestionWebData db = new QuestionWebData();
        // GET: Authentication
        [Authorize]
        public ActionResult Index()
        {
            var C = User.Identity.Name;
            var de = from d in db.Users
                     where d.User_Name == C
                     select d;
            User u = de.First();
            return View(u);
        }


        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(User u)
        {
            
            if (isValidUser(u))
            {
                FormsAuthentication.SetAuthCookie(u.User_Name, true);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.err_login = "输入的账号或密码有误！";
                return View();
            }
        }

        public bool isValidUser(User u)
        {
            var U = from i in db.Users
                    where i.User_Name == u.User_Name && i.PassWords == u.PassWords
                    select i;
            if (U.ToList().Count() == 0)
            {
                
                return false;
            }
            else
            {
                return true;
            }
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult register(User user)
        {
            var usercount = (from u in db.Users
                             where u.User_Name.Equals(user.User_Name)
                             select u).Count();

            if(ModelState.IsValid  && usercount<1)
            {
                user.Add_Time = DateTime.Now.ToString();
                user.UserStateid = 1;
                db.Users.Add(user);
                db.SaveChanges();
                FormsAuthentication.SetAuthCookie(user.User_Name, true);
                return RedirectToAction("index", "Home");
            }

            //User u = new Models.User();
            //u.User_Name = user.User_Name;
            //u.PassWords = user.PassWords;
            //u.Add_Time = DateTime.Now.ToShortDateString();
            //State state = new State();
            //state.UserStateid = 1;
            //state.Means = "正常";
            //u.UserState = state;
            //db.Users.Add(u);
            //db.SaveChanges();
            //FormsAuthentication.SetAuthCookie(u.User_Name, true);
            ViewBag.registr_message = "您输入的用户名已经存在，请输入新的用户名";
            return View();
        }
    }
}