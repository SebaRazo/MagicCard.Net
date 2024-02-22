namespace WebApplicationAgenda.Models.Dtos
{
    public class UserWithCardInfoDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        
        public CardInfoDto CardInfo { get; set; }
    }
}
