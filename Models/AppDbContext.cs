using Microsoft.EntityFrameworkCore;
using HRMIS_API.Models;



namespace HRMIS_API.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Inst_Region> Inst_Regions { get; set; }

        
    }

}
