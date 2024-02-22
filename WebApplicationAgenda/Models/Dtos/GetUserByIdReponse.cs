using WebApplicationAgenda.Entities;

namespace WebApplicationAgenda.Models.Dtos
{
    public class GetUserByIdReponse
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public ERole Role { get; set; }
    }
}
