using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using 答题竞赛.Models;

namespace 答题竞赛.ViewModels
{
    public class UserViewModel
    {
        [Key]
        public int User_id { get; set; }
        [Display(Name = "用户名")]
        public string User_Name { get; set; }
        [Display(Name = "密 码")]
        public string PassWords { get; set; }
        [Display(Name = "加入时间")]
        public string Add_Time { get; set; }
        [Display(Name = "账号状态")]
        public int UserStateid { get; set; }
        /// <summary>
        /// 是否管理员
        /// </summary>
        [Display(Name ="是否设为管理员")]
        public int isAdmin { get; set; }
        /// <summary>
        ///个人总积分
        /// </summary>
        [Display(Name ="个人总积分")]
        public int integral { get; set; }
        /// <summary>
        /// 回答的正确数
        /// </summary>
        [Display(Name ="累计回答正确数")]
        public int correct_numb { get; set; }
        /// <summary>
        /// 已闯关数
        /// </summary>
        [Display(Name ="已解锁关卡数")]
        public int Make_a_breakthrough { get; set; }

    }
}