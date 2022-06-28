using InformationPersonnelle.Server.Entities;
using InformationPersonnelle.Server.Entities.User;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace InformationPersonnelle.Server.Data
{
    public class InformationPersonnelleDbContext : IdentityDbContext<ApplicationUser>
    {
        private readonly IConfiguration configuration;

        public InformationPersonnelleDbContext(DbContextOptions<InformationPersonnelleDbContext> options, IConfiguration configuration) : base(options)
        {
            this.configuration = configuration;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Seed();
        }

        public virtual DbSet<Anniversaire> Anniversaires { get; set; }
        public virtual DbSet<Categorie> Categories { get; set; }
        public virtual DbSet<Document> Documents { get; set; }
        public virtual DbSet<Tag> Tags { get; set; }
        public virtual DbSet<DocumentTag> DocumentTags { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => //optionsBuilder.UseNpgsql("Host=localhost;Database=test;Username=postgres;Password=postgres");
           optionsBuilder
        //.UseLazyLoadingProxies().UseNpgsql("Host=localhost;Database=test;Username=postgres;Password=postgres");//.UseSqlServer("Host=localhost;Database=test;Username=postgres;Password=postgres");
       .UseLazyLoadingProxies().UseNpgsql(configuration.GetConnectionString(nameof(InformationPersonnelleDbContext)));//.UseSqlServer("Host=localhost;Database=test;Username=postgres;Password=postgres");

    }
}
