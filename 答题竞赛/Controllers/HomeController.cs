using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using 答题竞赛.Models;
using 答题竞赛.DAL;
using System.ComponentModel.DataAnnotations;
using 答题竞赛.ViewModels;
using System.Security;


namespace 答题竞赛.Controllers
{
    public class HomeController : Controller
    {
        QuestionWebData db = new QuestionWebData();


        public ActionResult Index()
        {
            return View();
        }

        
        public ActionResult AnswerQuestions()
        {
            ViewBag.UserName = User.Identity.Name;
            var q = (from t in db.Questions
                     orderby Guid.NewGuid()
                     select t).Take(10);
            
            List<QuestionViewModel> list_qvm = new List<QuestionViewModel>();
            int tmp_id = 1;
            foreach (Question item in q)
            {
                QuestionViewModel qvm = new QuestionViewModel();
                qvm.Qid = item.Qid;
                qvm.Q_content = item.Q_content;
                qvm.Q_Select_A = item.Q_Select_A;
                qvm.Q_Select_B = item.Q_Select_B;
                qvm.Q_Select_C = item.Q_Select_C;
                qvm.Q_Select_D = item.Q_Select_D;
                qvm.temp_id = tmp_id;
                tmp_id++;
                list_qvm.Add(qvm);
            }
            ViewBag.set_timmer = 600;
            return View(list_qvm);
            
        }

        public JsonResult get_questions()
        {
            ViewBag.UserName = User.Identity.Name;
            var q = (from t in db.Questions
                     orderby Guid.NewGuid()
                     select t).Take(10);

            List<QuestionViewModel> list_qvm = new List<QuestionViewModel>();
            int tmp_id = 1;
            foreach (Question item in q)
            {
                QuestionViewModel qvm = new QuestionViewModel();
                qvm.Qid = item.Qid;
                qvm.Q_content = item.Q_content;
                qvm.Q_Select_A = item.Q_Select_A;
                qvm.Q_Select_B = item.Q_Select_B;
                qvm.Q_Select_C = item.Q_Select_C;
                qvm.Q_Select_D = item.Q_Select_D;
                qvm.temp_id = tmp_id;
                tmp_id++;
                list_qvm.Add(qvm);
            }
            ViewBag.set_timmer = 600;
            return Json(list_qvm, JsonRequestBehavior.AllowGet);
        }


        public int CheckAnswer(string s,int QID)
        {

            var result = from r in db.Questions
                         where r.Qid == QID && r.Answer==s.Substring(0,1)
                         select r.Answer;
            List<string> re = result.ToList();
            if (re.Count==0)
            {
                return 0;
            }
            return 1;
        }

        /// <summary>
        /// 答题结束接受回传数据，保存至数据库
        /// </summary>
        /// <param name="score">得分</param>
        /// <param name="Use_time">答题用时</param>
        /// <param name="selectitems">用户选择的选项</param>
        /// <returns>返回历史得分列表</returns>
        /// 创建：沈浩
        /// 2018年2月13日 14:55:58
        public ActionResult receive_result(string score,string Use_time,string selectitems)
        {
            if (!string.IsNullOrEmpty(score)==true && !string.IsNullOrEmpty(Use_time))
            {
                string username = User.Identity.Name;
                var uid = from i in db.Users
                          where i.User_Name == username
                          select i;
                User F_uid = uid.First();
                Score sco = new Score();
                sco.Userid = F_uid.User_id;
                sco.CorrectNumber = int.Parse(score);
                sco.WrongNumber = 10 - int.Parse(score);
                sco.Use_Time = Use_time;
                sco.Add_Time = DateTime.Now.ToShortDateString();
                sco.earn_integral = int.Parse(score);
                db.Scores.Add(sco);
                db.SaveChanges();

                //获取scoreID
                var scoreID = (from m in db.Scores
                               select m.ScoreId).Max();


                string[] a= selectitems.Split(',');
                string[] results = a.Where(m => (m != "")).ToArray();
                int[] tem_Qid = new int[10];
                string[] tem_Selectitem = new string[10];
                for (int i = 0; i < results.Length; i++)
                {
                    string[] b = results[i].Split(':');
                    tem_Qid[i] = int.Parse(b[0]);
                    tem_Selectitem[i] = b[1];
                }
                string str_Qid = string.Join(",", tem_Qid);
                string str_Selectitem = string.Join(",", tem_Selectitem);
                History_Score HS = new History_Score();
                HS.Selected_Item = str_Selectitem;
                HS.Questionsid = str_Qid;
                HS.score = score;
                HS.sc = scoreID;
                db.History_Scores.Add(HS);
                db.SaveChanges();
                
                return RedirectToAction("index", "ScoreList");
            }
            else
            {
                ViewBag.err_submat = "提交失败请重试";
                return View("AnswerQuestions");
            }
        }

        public ActionResult AnswerAgain(int Sid)
        {

            var old_list = from i in db.History_Scores
                           where i.sc == Sid
                           select i;
            History_Score O_list_find = old_list.First();
            string Question_arry = O_list_find.Questionsid;
            string [] a=Question_arry.Split(',');
            List<QuestionViewModel> list_qvm = new List<QuestionViewModel>();

            for (int i = 0; i < a.Length; i++)
            {
                int search_id = int.Parse(a[i]);
                var o_Qusetion = from k in db.Questions
                                 where k.Qid == search_id
                                 select k;
                Question o_question = o_Qusetion.First();
                QuestionViewModel qvm = new QuestionViewModel();
                qvm.Qid = o_question.Qid;
                qvm.Q_content = o_question.Q_content;
                qvm.Q_Select_A = o_question.Q_Select_A;
                qvm.Q_Select_B = o_question.Q_Select_B;
                qvm.Q_Select_C = o_question.Q_Select_C;
                qvm.Q_Select_D = o_question.Q_Select_D;
                qvm.Answer = o_question.Answer;
                qvm.temp_id = i+1;
                list_qvm.Add(qvm);
            }
            ViewBag.set_timmer = 600;
            return View("AnswerQuestions",list_qvm);
        }
        
    }
}