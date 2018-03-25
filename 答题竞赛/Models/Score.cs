using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace 答题竞赛.Models
{
    public class Score
    {
        [Key]
        public int ScoreId { get; set; }
        public int Userid { get; set; }
        [Display(Name ="耗时")]
        public string Use_Time { get; set; }
        [Display(Name ="正确数")]
        public int CorrectNumber { get; set; }
        [Display(Name ="错误数")]
        public int WrongNumber { get; set; }
        [Display(Name = "挑战时间")]
        public string Add_Time { get; set; }
        [Display(Name ="获得的积分")]
        public int earn_integral { get; set; }
    }
}