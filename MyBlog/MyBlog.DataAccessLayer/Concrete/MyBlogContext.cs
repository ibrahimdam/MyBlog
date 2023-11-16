using Microsoft.EntityFrameworkCore;
using MyBlog.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.DataAccessLayer.Concrete
{
    public class MyBlogContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<BlogUser> BlogUsers { get; set; }
        public DbSet<Note> Notes { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Liked> Likes { get; set; }



        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=DESKTOP-MCJ9JKH;Database=MyBlogDB;Integrated Security=true");
        }

    }
}
