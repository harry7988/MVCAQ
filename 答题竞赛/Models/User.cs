using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace 答题竞赛.Models
{
    /// <summary>
    /// 用户基类
    /// </summary>
    public class User
    {
        [Key]
        public int User_id { get; set; }
        [Display(Name ="用户名")]
        public string User_Name { get; set; }
        [Display(Name ="密 码")]
        public string PassWords { get; set; }
        [Display(Name ="加入时间")]
        public string Add_Time { get; set; }
        [Display(Name ="账号状态")]
        public int UserStateid { get; set; }
        /// <summary>
        /// 是否管理员
        /// </summary>
        public int isAdmin { get; set; }

    }
}