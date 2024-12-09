using Microsoft.EntityFrameworkCore;

namespace WebTeploobmen.Data
{
    public class TeploobmenContext :DbContext
    {
        public DbSet<Variant> Variants { get; set; }

        public TeploobmenContext(DbContextOptions<TeploobmenContext> options) : base(options)
        {

        }
    }
}
