namespace WebApplicationAgenda.Models.Dtos
{
    public class ReportUserCardsDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Title { get; set; }
        public string? Image { get; set; }
        public float? Price { get; set; }
        public int? CardStock { get; set; }
    }
}
