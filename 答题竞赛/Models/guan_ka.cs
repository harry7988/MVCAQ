using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace 答题竞赛.Models
{
    public class guan_ka
    {
        [Key]
        public int guanka_id { get; set; }//关卡编号
        public int Need_Score { get; set; }//解锁卡关需要达到的分数
    }
}