using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using 答题竞赛.Models;
using 答题竞赛.DAL;
using System.ComponentModel.DataAnnotations;
using 答题竞赛.ViewModels;

namespace 答题竞赛.Controllers
{
    public class ScoreListController : Controller
    {
        QuestionWebData db = new QuestionWebData();
        // GET: ScoreList
        [Authorize]
        public ActionResult Index()
        {
            var user = User.Identity.Name;
            var myscores = from s in db.Scores
                           where s.Userid == (from q in db.Users
                                              where q.User_Name == user
                                              select q.User_id).FirstOrDefault()
                           select s;
            IEnumerable<Score> sc = myscores.ToList();
            ViewBag.UserName = user;
            
            return View(sc);
        }
    }
}