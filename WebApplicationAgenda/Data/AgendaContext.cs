using Microsoft.EntityFrameworkCore;
using WebApplicationAgenda.Entities;

namespace WebApplicationAgenda.Data
{
    public class AgendaContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Contact> Contacts { get; set; }

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
                UserName = "karenpiola"
            };
            User luis = new User()
            {
                Id = 2,
                Name = "Luis Gonzalez",
                LastName = "Gonzales",
                Password = "lamismadesiempre",
                Email = "elluismidetotoras@gmail.com",
                UserName = "luismitoto"
            };

            Contact jaimitoC = new Contact()
            {
                Id = 1,
                Name = "Jaimito",
                CelularNumber = 341457896,
                Description = "Plomero",
                TelephoneNumber = null,
                UserId = karen.Id,
            };

            Contact pepeC = new Contact()
            {
                Id = 2,
                Name = "Pepe",
                CelularNumber = 34156978,
                Description = "Papa",
                TelephoneNumber = 422568,
                UserId = luis.Id,
            };

            Contact mariaC = new Contact()
            {
                Id = 3,
                Name = "Maria",
                CelularNumber = 011425789,
                Description = "Jefa",
                TelephoneNumber = null,
                UserId = karen.Id,
            };

            modelBuilder.Entity<User>().HasData(
                karen, luis);

            modelBuilder.Entity<Contact>().HasData(
                 jaimitoC, pepeC, mariaC
                 );

            modelBuilder.Entity<User>()
              .HasMany<Contact>(u => u.Contacts)
              .WithOne(c => c.User);

            base.OnModelCreating(modelBuilder);
        }
    }
}
