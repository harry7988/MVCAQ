using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace 答题竞赛.Models
{
    public class Question
    {
        [Key]
        [Display(Name ="编号")]
        public int Qid { get; set; }
        [Display(Name ="问题")]
        public string  Q_content { get; set; }
        [Display(Name ="选项A")]
        public string Q_Select_A { get; set; }
        [Display(Name = "选项B")]
        public string Q_Select_B { get; set; }
        [Display(Name = "选项C")]
        public string Q_Select_C { get; set; }
        [Display(Name = "选项D")]
        public string Q_Select_D { get; set; }
        [Display(Name = "答案")]
        public string Answer { get; set; }

    }
}