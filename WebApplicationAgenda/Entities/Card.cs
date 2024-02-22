using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplicationAgenda.Entities
{
    public class Card
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Title { get; set; }
        public string? Image { get; set; }
        public float? Price { get; set; }
        public int? CardStock { get; set; }
        //public string? Description { get; set; } 
        [ForeignKey("UserId")]
        public User User { get; set; }
        public Sale Sale { get; set; }
        public int UserId { get; set; }
        public int SaleId { get; set; }

        
    }
}
