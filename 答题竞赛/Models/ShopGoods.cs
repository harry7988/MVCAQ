using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace 答题竞赛.Models
{
    public class ShopGoods
    {
        [Key]
        public int Good_id { get; set; }//商品编号
        [Display(Name ="道具名称")]
        public string GoodName { get; set; }//商品名称
        [Display(Name ="商品价格")]
        public int Good_cost { get; set; }//商品价格
        [Display(Name ="商品描述")]
        public string Good_Describe { get; set; }//商品描述
        [Display(Name ="是否上架")]
        public int can_buy { get; set; }//是否可以购买（上架）
    }
}