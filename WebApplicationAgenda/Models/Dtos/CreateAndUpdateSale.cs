using Newtonsoft.Json;
using WebApplicationAgenda.Entities;

namespace WebApplicationAgenda.Models.Dtos
{
    public class CreateAndUpdateSale
    {
        public int UserId { get; set; }
        public int CardId { get; set; }
        public int Total { get; set; }
        public DateTime Date { get; set; }
        
    }
}
