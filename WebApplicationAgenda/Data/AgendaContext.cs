using Microsoft.EntityFrameworkCore;
using WebApplicationAgenda.Entities;

namespace WebApplicationAgenda.Data
{
    public class AgendaContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<Card> Cards { get; set; }

        public AgendaContext(DbContextOptions<AgendaContext> options) : base(options) //Acá estamos llamando al constructor de DbContext que es el que acepta las opciones
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            User karen = new User()
            {
                Id = 1,
                Name = "Karen",
                LastName = "Lasot",
                Password = "Pa$$w0rd",
                Email = "karenbailapiola@gmail.com",
                UserName = "karenpiola",
                Role = ERole.USER

            };
            User seba = new User()
            {
                Id = 3,
                Name = "Seba",
                LastName = "Razo",
                Password = "seba",
                Email = "sr@gmail.com",
                UserName = "sr",
                Role = ERole.ADMIN
            };

            User luis = new User()
            {
                Id = 2,
                Name = "Luis Gonzalez",
                LastName = "Gonzales",
                Password = "lamismadesiempre",
                Email = "elluismidetotoras@gmail.com",
                UserName = "luismitoto",
                Role = ERole.SELLER
            };

  
        

            modelBuilder.Entity<User>().HasData(
                karen, luis, seba);

         
            


            
            modelBuilder.Entity<User>()
            .HasMany(u => u.Sales)
            .WithOne(c => c.User)
            .HasForeignKey(c => c.UserId)
            .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Sale>()
            .HasOne(c => c.User)
            .WithMany(u => u.Sales)
            .HasForeignKey(c => c.UserId)
            .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Card>()
            .HasOne(c => c.Sale)
            .WithMany(c => c.Cards)
            .HasForeignKey(c => c.SaleId)
            .OnDelete(DeleteBehavior.Cascade);



            base.OnModelCreating(modelBuilder);
        }
    }
}