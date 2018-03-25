using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace 答题竞赛.Models
{
    public class State
    {
        [Key]
        public int UserStateid { get; set; }
        public string Means { get; set; }

    }
}