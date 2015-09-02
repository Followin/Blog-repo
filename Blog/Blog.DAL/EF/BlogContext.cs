using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blog.DAL.Entities;

namespace Blog.DAL.EF
{
    public class BlogContext : DbContext
    {
        public DbSet<Article> Articles { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserArticleMark> UserArticleMarks { get; set; }
        public BlogContext(String connectionString) : base(connectionString)
        {
            
        }

        static BlogContext()
        {
            System.Data.Entity.Database.SetInitializer(new BlogContextInitializer());
        }
    }

    public class BlogContextInitializer : DropCreateDatabaseAlways<BlogContext>
    {
        protected override void Seed(BlogContext db)
        {
            db.Articles.Add(new Article {Author = "FirstAuthor", DateTime = DateTime.Now, Theme = "First Theme", Text = "Kinda text goes here"});
            db.Articles.Add(new Article {Author = "SecondAuthor", DateTime = DateTime.Now, Theme = "Second Theme", Text = "Another One"});
            db.Articles.Add(new Article
            {
                Author = "Big writer",
                DateTime = DateTime.Now,
                Theme = "Big message",
                Text = new string('A', 400) + new string('B', 400)
            });
            db.Articles.Add(new Article { Author = "SecondAuthor", DateTime = DateTime.Now, Theme = "Second Theme", Text = "Another One" });
            db.Articles.Add(new Article { Author = "SecondAuthor", DateTime = DateTime.Now, Theme = "Second Theme", Text = "Another One" });
            db.Articles.Add(new Article { Author = "SecondAuthor", DateTime = DateTime.Now, Theme = "Second Theme", Text = "Another One" });


            db.Roles.Add(new Role { Name = "Member" });
            db.Roles.Add(new Role { Name = "Admin" });
            db.SaveChanges();
        }
    }

    public class BlogContextFactory : IDbContextFactory<BlogContext>
    {
        public BlogContext Create()
        {
            return new BlogContext("DefaultConnection");
        }
    }
}
