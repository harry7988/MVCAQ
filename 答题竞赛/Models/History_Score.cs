using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace 答题竞赛.Models
{
    public class History_Score
    {
        [Key]
        public int History_id { get; set; }
        [Display(Name ="成绩")]
        public string score { get; set; }
        [Display(Name ="问题队列")]
        public string Questionsid { get; set; }
        [Display(Name ="选择项队列")]
        public string Selected_Item { get; set; }
        [Display(Name ="成绩id")]
        public int sc { get; set; }
    }
}