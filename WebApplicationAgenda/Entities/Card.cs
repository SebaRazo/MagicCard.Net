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
        public ICollection<Sale> Sales { get; set; }

    }
}
