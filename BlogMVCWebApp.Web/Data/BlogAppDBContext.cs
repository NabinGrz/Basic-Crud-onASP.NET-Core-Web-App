using BlogMVCWebApp.Web.Models.DomainModel;
using Microsoft.EntityFrameworkCore;

namespace BlogMVCWebApp.Web.Data
{
    public class BlogAppDBContext : DbContext
    {
        public BlogAppDBContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<BlogPost> BlogPost { get; set; }
        public DbSet<Tag> Tags { get; set; }
    }
}
