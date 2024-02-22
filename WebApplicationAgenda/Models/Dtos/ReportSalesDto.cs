namespace WebApplicationAgenda.Models.Dtos
{
    public class ReportSalesDto
    {
        public int Id { get; set; }
        public int? CardId { get; set; }
        public int? UserId { get; set; }
        public int? Total { get; set; }
        public DateTime Date { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public float Price { get; set; }
    }
}
