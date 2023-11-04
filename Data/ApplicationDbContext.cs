using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using ASP_Final.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.DotNet.Scaffolding.Shared.Project;

namespace ASP_Final.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Vaccine> Vaccine { get; set; }
        public DbSet<Models.Type> Type { get; set; }
        public DbSet<VaccineSchedule> Schedule { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Models.Type>().HasData(
                new Models.Type
                { Id = 1, Name = "Inactivated", CreateAt = new DateTime(2023, 10, 31, 2, 40, 50) },
                new Models.Type
                { Id = 2, Name = "Live-attenuated", CreateAt = new DateTime(2023, 10, 31, 3, 44, 12) },
                new Models.Type
                { Id = 3, Name = "Messenger RNA (mRNA)", CreateAt = new DateTime(2023, 10, 31, 4, 55, 23) },
                new Models.Type
                { Id = 4, Name = "Subunit, recombinant, polysaccharide, and conjugate", CreateAt = new DateTime(2023, 10, 31, 5, 22, 34) });
        }
    }
}
