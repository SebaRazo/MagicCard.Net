namespace WebApplicationAgenda.Models.Dtos
{
    public class BlockedContactWithCallInfoDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public long? CelularNumber { get; set; }
        public long? TelephoneNumber { get; set; }
        public string Description { get; set; }
        public bool IsBlocked { get; set; }
        public CallInfoDto CallInfo { get; set; }
    }
}
