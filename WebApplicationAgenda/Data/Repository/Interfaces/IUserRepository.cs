using WebApplicationAgenda.Entities;
using WebApplicationAgenda.Models.Dtos;

namespace WebApplicationAgenda.Data.Repository.Interfaces
{
    public interface IUserRepository
    {
        Task <User> Validate(AuthenticationRequestBody authRequestBody);
        Task <User> GetById(int userId);
        Task<List<User>> GetAll();
        Task Create(CreateAndUpdateUser dto);
        Task Update(int id_user,CreateAndUpdateUser dto);
        Task Delete(int id);
    }
}
