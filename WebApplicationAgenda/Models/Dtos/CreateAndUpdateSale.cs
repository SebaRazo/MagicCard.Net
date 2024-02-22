using Newtonsoft.Json;
using WebApplicationAgenda.Entities;

namespace WebApplicationAgenda.Models.Dtos
{
    public class CreateAndUpdateSale
    {
        public int Id { get; set; }
        public int CardId { get; set; }
        public int Total { get; set; }
        public DateTime Date { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }
        //public List<Call> Calls { get; set; }
        
    }
}
