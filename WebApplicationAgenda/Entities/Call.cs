using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace WebApplicationAgenda.Entities
{
    public class Call
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int ContactId { get; set; }

        [ForeignKey("ContactId")]
        [JsonIgnore]
        public Contact Contact { get; set; }

        public int CountCall { get; set; }

        public DateTime TimeCall { get; set; }
    }
}
