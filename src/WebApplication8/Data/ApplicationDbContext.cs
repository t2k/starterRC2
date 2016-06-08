using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebApplication8.Models;

namespace WebApplication8.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<RiskItem> RiskItem { get; set; }
        public virtual DbSet<RiskClass> RiskClass { get; set; }
        public virtual DbSet<RiskCategory> RiskCategory { get; set; }
        public virtual DbSet<RiskReport> RiskReport { get; set; }
        public virtual DbSet<Blog> Blog { get; set; }
        public virtual DbSet<Post> Post { get; set; }
        public virtual DbSet<RRRI> RRRI { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<RRRI>()
                 .HasKey(t => new { t.RiskReportId, t.RiskItemId });

            modelBuilder.Entity<RRRI>()
                .HasOne(rr => rr.RiskReport)
                .WithMany(ri => ri.RRRIs)
                .HasForeignKey(rr => rr.RiskReportId);

            modelBuilder.Entity<RRRI>()
                .HasOne(ri => ri.RiskItem)
                .WithMany(rr => rr.RRRIs)
                .HasForeignKey(ri=>ri.RiskItemId);            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }
    }
}
