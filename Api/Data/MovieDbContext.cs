using Microsoft.EntityFrameworkCore;
using Api.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
namespace Api.Data
{
    public class MovieDbContext : IdentityDbContext<AppUser>
    {
        public MovieDbContext(DbContextOptions options) : base(options)
        { 
            
        }
        public DbSet<MovieSeries> MovieSeries { get; set; }
        public DbSet<MovieEpisode> MovieEpisode { get; set; }
        public DbSet<Genre> Genre { get; set; }
        public DbSet<Region> Region { get; set; }
        public DbSet<Year> Year { get; set; }
        public DbSet<Comment> Comment { get; set; }
        public DbSet<MovieSeriesGenre>MovieSeriesGenre { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<MovieSeriesGenre>()
                    .HasKey(p => new { p.MovieSeriesId, p.GenreId });

            /* modelBuilder.Entity<MovieSeries>()
                  .HasMany(x => x.MovieSeriesGenres)
                  .WithOne(x => x.MovieSeries);

             modelBuilder.Entity<Genre>()
                  .HasMany(x => x.MovieSeriesGenres)
                  .WithOne(x => x.Genre);*/
           
        }
    }
}
