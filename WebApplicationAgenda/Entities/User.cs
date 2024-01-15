using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplicationAgenda.Entities
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string UserName { get; set; }
        public ICollection<Contact> Contacts { get; set; }// Colección de contactos asociados a este User

        //public ICollection<Contact> BlockedContacts { get; set; }// Colección de contactos bloqueados asociados a este User
    }
}
