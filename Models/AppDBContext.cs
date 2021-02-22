
using Microsoft.EntityFrameworkCore;

namespace TodoApp.Models
{

    public class AppDBContext : DbContext
    {
        public AppDBContext(
                   DbContextOptions<AppDBContext> options)
                   : base(options)
        {
        }
        public DbSet<Todo> Todos { get; set; }
    }
}