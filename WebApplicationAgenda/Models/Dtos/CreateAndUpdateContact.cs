using System.ComponentModel.DataAnnotations;
using WebApplicationAgenda.Entities;

namespace WebApplicationAgenda.Models.Dtos
{
    public class CreateAndUpdateContact
    {
        [Required]
        public string Name { get; set; }
        public int? CelularNumber { get; set; }
        public int? TelephoneNumber { get; set; }
        public string Description { get; set; } = String.Empty;
        public User? User;
        public bool IsBlocked { get; set; }
    }
}
