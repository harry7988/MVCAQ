using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using 答题竞赛.Models;

namespace 答题竞赛.DAL
{
    public class QuestionWebData:DbContext
    {
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Question>().ToTable("tblQuestion");
            modelBuilder.Entity<Score>().ToTable("tblScore");
            modelBuilder.Entity<State>().ToTable("tblState");
            modelBuilder.Entity<User>().ToTable("tblUser");
            modelBuilder.Entity<History_Score>().ToTable("tblHistory_Score");
            modelBuilder.Entity<ShopGoods>().ToTable("tblShopGoods");
            modelBuilder.Entity<guan_ka>().ToTable("tblguan_ka");
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Question> Questions { get; set; }
        public DbSet<Score> Scores { get; set; }
        public DbSet<State> States { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<History_Score> History_Scores { get; set; }
        public DbSet<ShopGoods> ShopGoods { get; set; }
        public DbSet<guan_ka> Guan_Ka { get; set; }

        public System.Data.Entity.DbSet<答题竞赛.ViewModels.QuestionViewModel> QuestionViewModels { get; set; }
    }
}