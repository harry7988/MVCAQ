using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using 答题竞赛.Models;

namespace 答题竞赛.ViewModels
{
    public class AdminViewModel
    {
        public List<ScoreViewModel> scores { get; set; }
        //public int which_selected { get; set; }
        public int UserNumber { get; set; }
        public int QuestionNumber { get; set; }
        public int The_Max_score { get; set; }

    }
}