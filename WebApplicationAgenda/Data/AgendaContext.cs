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
            User seba = new User()
            {
                Id = 3,
                Name = "Seba",
                LastName = "Razo",
                Password = "seba",
                Email = "sr@gmail.com",
                UserName = "sr"
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
                IsBlocked = true,

                //BlockedByUserId = luis.Id
            };

            Contact pepeC = new Contact()
            {
                Id = 2,
                Name = "Pepe",
                CelularNumber = 34156978,
                Description = "Papa",
                TelephoneNumber = 422568,
                UserId = luis.Id,
                IsBlocked=false,
            };

            Contact mariaC = new Contact()
            {
                Id = 3,
                Name = "Maria",
                CelularNumber = 011425789,
                Description = "Jefa",
                TelephoneNumber = null,
                UserId = karen.Id,
                IsBlocked=true,
                //BlockedByUserId = luis.Id
            };

            Contact juanfer = new Contact()
            {
                Id = 4,
                Name = "Juanfer",
                CelularNumber = 34156,
                Description = "?",
                TelephoneNumber = 42256,
                UserId = seba.Id,
                IsBlocked=true,
            };

            modelBuilder.Entity<User>().HasData(
                karen, luis, seba);

            modelBuilder.Entity<Contact>().HasData(
                 jaimitoC, pepeC, mariaC, juanfer
                 );

            /*modelBuilder.Entity<User>()
              .HasMany<Contact>(u => u.Contacts)
              .WithOne(c => c.User);*/ //como estaba


            //nuevo
            modelBuilder.Entity<User>()
            .HasMany(u => u.Contacts)
            .WithOne(c => c.User)
            .HasForeignKey(c => c.UserId)
            .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<User>()
            .HasMany(u => u.BlockedContacts)
            .WithOne()
            .HasForeignKey(c => c.BlockedByUserId) // Clave foránea para el usuario que bloqueó el contacto
            .OnDelete(DeleteBehavior.Restrict);



            modelBuilder.Entity<Contact>()
                .Property(c => c.IsBlocked)
                .HasColumnName("IsBlocked");






            base.OnModelCreating(modelBuilder);
        }
    }
}
/*//ver esto
            modelBuilder.Entity<Contact>()
            .Property(c => c.IsBlocked)
            .HasColumnName("IsBlocked");
            //ver esto
            modelBuilder.Entity<User>()
              .HasMany<Contact>(u => u.BlockedContacts)
              .WithOne(c=>c.User)
              .HasForeignKey(c => c.UserId)
              ;*/
/*
 
  modelBuilder.Entity<User>()
        .HasMany(u => u.Contacts)
        .WithOne(c => c.User)
        .HasForeignKey(c => c.UserId);

    modelBuilder.Entity<User>()
        .HasMany(u => u.BlockedContacts)
        .WithOne()  // No se especifica la propiedad de navegación en Contact
        .HasForeignKey(c => c.BlockedByUserId); // Utiliza la misma clave foránea que en Contact

    modelBuilder.Entity<Contact>()
        .Property(c => c.IsBlocked)
        .HasColumnName("IsBlocked");
 
 */