using Microsoft.EntityFrameworkCore;
using SurveyAccessor.Models;

namespace SurveyAccessor.Context
{
    public class SurveysDbContext : DbContext
    {
        public DbSet<Survey> Surveys { get; set; }
        public DbSet<SurveyOption> SurveyOptions {get;set;}

        public SurveysDbContext(DbContextOptions<SurveysDbContext> options) : base(options){  }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Survey>(e => {
                e.Property(p => p.SurveyId).IsRequired().ValueGeneratedOnAdd();
                e.HasKey(p => p.SurveyId);
                e.Property(p => p.Description).HasMaxLength(255);
                e.Property(p => p.SurveyName).HasMaxLength(50).IsRequired();
                e.Property(p => p.CreatedOn).IsRequired().HasDefaultValueSql("getdate()");
            });

            modelBuilder.Entity<Survey>().HasMany(e => e.SurveyOptions).WithOne(e => e.Survey).HasForeignKey(e => e.Fk_SurveyId);

            modelBuilder.Entity<SurveyOption>(e => {
                e.Property(p => p.SurveyOptionId).IsRequired().ValueGeneratedOnAdd();
                e.HasKey(p => p.SurveyOptionId);
                e.Property(p => p.TotalVotes).IsRequired();
                e.Property(p => p.ImagePath).HasMaxLength(255);
                e.Property(p => p.Description).HasMaxLength(255).IsRequired();
            
            });
        }
    }
}
