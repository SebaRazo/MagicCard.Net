using WebApplicationAgenda.Entities;

namespace WebApplicationAgenda.Models.Dtos
{
    public class CreateAndUpdateCard
    {
        //public int Id { get; set; }
        public string Title { get; set; }
        public string? Image { get; set; }
        public float? Price { get; set; }
        public int? CardStock { get; set; }
        public User User ;
        public Sale Sale;

    }
}
