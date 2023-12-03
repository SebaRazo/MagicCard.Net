using WebApplicationAgenda.Entities;
using WebApplicationAgenda.Models.Dtos;

namespace WebApplicationAgenda.Data.Repository.Interfaces
{
    public interface IUserRepository
    {
        //public User? ValidateUser(AuthenticationRequestBody authRequestBody);
        public User? GetById(int userId);
        public List<User> GetAll();
        public void Create(CreateAndUpdateUser dto);
        public void Update(CreateAndUpdateUser dto);
        public void Delete(int id);
    }
}
