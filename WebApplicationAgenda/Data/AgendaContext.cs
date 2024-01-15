using Microsoft.EntityFrameworkCore;
using WebApplicationAgenda.Entities;

namespace WebApplicationAgenda.Data
{
    public class AgendaContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Call> Calls { get; set; }

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
                //IsBlocked = true,

                
            };

            Contact pepeC = new Contact()
            {
                Id = 2,
                Name = "Pepe",
                CelularNumber = 34156978,
                Description = "Papa",
                TelephoneNumber = 422568,
                UserId = luis.Id,
                //IsBlocked=false,
            };

            Contact mariaC = new Contact()
            {
                Id = 3,
                Name = "Maria",
                CelularNumber = 011425789,
                Description = "Jefa",
                TelephoneNumber = null,
                UserId = karen.Id,
                //IsBlocked=true,
                
            };

            Contact juanfer = new Contact()
            {
                Id = 4,
                Name = "Juanfer",
                CelularNumber = 34156,
                Description = "?",
                TelephoneNumber = 42256,
                UserId = seba.Id,
                //IsBlocked=true,
            };


            /*Call pepeCall = new Call
            {
                
                ContactId = pepeC.Id,
                CountCall = 2,  
                TimeCall = DateTime.Now.AddHours(-1),  
            };*/

            modelBuilder.Entity<User>().HasData(
                karen, luis, seba);

            modelBuilder.Entity<Contact>().HasData(
                 jaimitoC, pepeC, mariaC, juanfer
                 );

            //modelBuilder.Entity<Call>().HasData(pepeCall);


            //nuevo
            modelBuilder.Entity<User>()
            .HasMany(u => u.Contacts)
            .WithOne(c => c.User)
            .HasForeignKey(c => c.UserId)
            .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Contact>()
            .HasOne(c => c.User)
            .WithMany(u => u.Contacts)
            .HasForeignKey(c => c.UserId)
            .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Call>()
            .HasOne(c => c.Contact)
            .WithMany(c => c.Calls)
            .HasForeignKey(c => c.ContactId);
            /*modelBuilder.Entity<Call>()
            .Property(c => c.Id)
            .ValueGeneratedOnAdd();*/


            base.OnModelCreating(modelBuilder);
        }
    }
}