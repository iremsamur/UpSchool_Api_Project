using Microsoft.EntityFrameworkCore;
using UpSchool_WebApi.DAL.Entities;

namespace UpSchool_WebApi.DAL
{
    public class Context : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=LAPTOP-ISO96UVH\\SQLEXPRESS;Database=UpSchoolApiProjectDb;integrated security=True;");

        }
        public DbSet<Category> Categories { get; set; }
    }
}
