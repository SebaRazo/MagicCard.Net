using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplicationAgenda.Entities
{
    public class Contact
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public int? CelularNumber { get; set; }
        public int? TelephoneNumber { get; set; }
        public string Description = String.Empty;
        [ForeignKey("UserId")]// Clave foránea que referencia al usuario dueño de este contacto
        public User User { get; set; }
        public int UserId { get; set; }// Identificador del usuario dueño de este contacto
        public bool IsBlocked { get; set; }//Contacto bloqueado
        [ForeignKey("BlockedByUserId")]
        public int? BlockedByUserId { get; set; }

        
    }
}
// Nueva propiedad de navegación para la relación de contactos bloqueados
/*[ForeignKey("BlockedByUserId")]
public User BlockedByUser { get; set; }

public int? BlockedByUserId { get; set; } // Identificador del usuario que bloqueó este contacto*/
