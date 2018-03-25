using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using 答题竞赛.Models;
using 答题竞赛.ViewModels;
using 答题竞赛.DAL;
using System.IO;

namespace 答题竞赛.Controllers
{
    [Authorize]
    public class ADController : Controller
    {
        QuestionWebData db = new QuestionWebData();
        
        // GET: AD
        public ActionResult Index()
        {
            ViewBag.user = User.Identity.Name;
            AdminViewModel Avm = new AdminViewModel();
            var AllScores = (from s in db.Scores
                             group s by s.Userid into p
                             from k in p
                             orderby k.CorrectNumber descending
                             select k).Take(10);
            Avm.scores = new List<ScoreViewModel>();

            QuestionWebData qwd = new QuestionWebData();

            foreach (var item in AllScores)
            {
                var u = (from un in qwd.Users
                         where un.User_id == item.Userid
                         select un.User_Name).FirstOrDefault();
                ScoreViewModel svm0 = new ScoreViewModel();
                svm0.Add_Time = item.Add_Time;
                svm0.CorrectNumber = item.CorrectNumber;
                svm0.ScoreId = item.ScoreId;
                svm0.Userid = item.Userid;
                //u为根据id查询的昵称或姓名
                svm0.UserName = u;
                svm0.Use_Time = item.Use_Time;
                svm0.WrongNumber = item.WrongNumber;
                Avm.scores.Add(svm0);
            }
            QuestionWebData qwd1 = new QuestionWebData();
            var usernumber = (from un1 in qwd1.Users
                              select un1).Count();
            Avm.UserNumber = usernumber;
            QuestionWebData qwd2 = new QuestionWebData();
            var QuestionNumber0 = (from qtk in qwd2.Questions
                                   select qtk).Count();
            Avm.QuestionNumber = QuestionNumber0;
            if (qwd.Scores.Count()!=0)
            {
                QuestionWebData qwd3 = new QuestionWebData();
                var max_score = (from ms in qwd3.Scores
                                 select ms.CorrectNumber).Max();
                Avm.The_Max_score = max_score;
            }
            
            ViewBag.which_selected = 0;


            return View(Avm);
        }

        public ActionResult Management_of_questions(string searchstring)
        {
            ViewBag.user = User.Identity.Name;
            ManageOfQuestion mfo = new ManageOfQuestion();
            ViewBag.which_selected = 1;
            var tk = from t in db.Questions
                     select t;
            if(!string.IsNullOrEmpty(searchstring))
            {
                tk = tk.Where(a => a.Q_content.Contains(searchstring)).Select(b=>b);
            }
            mfo.questions = new List<QuestionViewModel>();
            int i = 0;
            foreach (var item in tk)
            {
                i++;
                QuestionViewModel q = new QuestionViewModel();
                q.Qid = item.Qid;
                q.Q_content = item.Q_content;
                q.Q_Select_A = item.Q_Select_A;
                q.Q_Select_B = item.Q_Select_B;
                q.Q_Select_C = item.Q_Select_C;
                q.Q_Select_D = item.Q_Select_D;
                q.Answer = item.Answer;
                q.temp_id = i;
                mfo.questions.Add(q);
            }
            return View(mfo);
        }
        [HttpPost]
        public ActionResult Upload(FileUploadViewModel model)
        {
            List<Question> questions = GetQuestions(model);
            db.Questions.AddRange(questions);
            db.SaveChanges();
            return RedirectToAction("Management_of_questions");
        }

        private List<Question> GetQuestions(FileUploadViewModel model)
        {
            List<Question> lqs = new List<Question>();
            StreamReader csvreader = new StreamReader(model.fileupload.InputStream,System.Text.Encoding.Default);
            csvreader.ReadLine();
            while(!csvreader.EndOfStream)
            {
                var line = csvreader.ReadLine();
                var neirong = line.Split(',');
                Question qu = new Question();
                qu.Qid = 1;
                qu.Q_content = neirong[0];
                qu.Q_Select_A = neirong[1];
                qu.Q_Select_B = neirong[2];
                qu.Q_Select_C = neirong[3];
                qu.Q_Select_D = neirong[4];
                qu.Answer = neirong[5];
                lqs.Add(qu);
            }
            return lqs;
        }

        public ActionResult EditQuestion(int id)
        {
            ViewBag.user = User.Identity.Name;
            ViewBag.which_selected = 1;
            var detial = from d in db.Questions
                         where d.Qid == id
                         select d;
            QuestionViewModel qvm = new QuestionViewModel();
            foreach (var item in detial)
            {
                qvm.Qid = item.Qid;
                qvm.Q_content = item.Q_content;
                qvm.Q_Select_A = item.Q_Select_A;
                qvm.Q_Select_B = item.Q_Select_B;
                qvm.Q_Select_C = item.Q_Select_C;
                qvm.Q_Select_D = item.Q_Select_D;
                qvm.Answer = item.Answer;
            }
            return View(qvm);
        }

        public ActionResult DetialOfQuestion(int id)
        {
            ViewBag.user = User.Identity.Name;
            ViewBag.which_selected = 1;
            var detial = from d in db.Questions
                         where d.Qid == id
                         select d;
            QuestionViewModel qvm = new QuestionViewModel();
            foreach (var item in detial)
            {
                qvm.Qid = item.Qid;
                qvm.Q_content = item.Q_content;
                qvm.Q_Select_A = item.Q_Select_A;
                qvm.Q_Select_B = item.Q_Select_B;
                qvm.Q_Select_C = item.Q_Select_C;
                qvm.Q_Select_D = item.Q_Select_D;
                qvm.Answer = item.Answer;
            }
            return View(qvm);
        }

        public ActionResult ManageOfUser()
        {
            ViewBag.user = User.Identity.Name;
            ViewBag.which_selected = 2;
            var us = from i in db.Users
                     where i.isAdmin != 1
                     select i;
            
            return View(us);
        }

        public ActionResult EditUser(int uid)
        {
            ViewBag.user = User.Identity.Name;
            ViewBag.which_selected = 2;
            var userdetial = (from ud in db.Users
                              where ud.User_id == uid
                              select ud).FirstOrDefault();


            return View(userdetial);

        }

        public ActionResult Userinfor(int uid)
        {
            ViewBag.which_selected = 2;
            var u_info = (from uinfo in db.Users
                          where uinfo.User_id == uid
                          select uinfo).FirstOrDefault();
            UserViewModel u_info_view = new UserViewModel();
            u_info_view.Add_Time = u_info.Add_Time;
            u_info_view.isAdmin = u_info.isAdmin;
            u_info_view.PassWords = u_info.PassWords;
            u_info_view.UserStateid = u_info.UserStateid;
            u_info_view.User_id = u_info.User_id;
            u_info_view.User_Name = u_info.User_Name;

            
            u_info_view.integral = 999;
            u_info_view.Make_a_breakthrough = 2;
            return View(u_info_view);
        }

        [Authorize]
        public ActionResult ShopManage()
        {
            ViewBag.which_selected = 3;
            var goods = from g in db.ShopGoods
                        select g;
            return View(goods);
        }

        public ActionResult CreateShopGood()
        {
            ViewBag.which_selected = 3;
            return View();
        }

        [HttpPost]
        public ActionResult CreateShopGood(ShopGoods good)
        {
            if(ModelState.IsValid)
            {
                db.ShopGoods.Add(good);
                db.SaveChanges();
            }
            else
            {
                return View();
            }
            return RedirectToAction("ShopManage");
        }


    }
}