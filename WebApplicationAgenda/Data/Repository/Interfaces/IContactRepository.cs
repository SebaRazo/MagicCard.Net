using WebApplicationAgenda.Entities;
using WebApplicationAgenda.Models.Dtos;

namespace WebApplicationAgenda.Data.Repository.Interfaces
{
    public interface IContactRepository
    {
        Task <List<Contact>> GetAll();
        Task<List<Contact>> GetAllByUser(int id);
        Task Create(CreateAndUpdateContact dto, int userId);
        Task Update(int id, CreateAndUpdateContact dto);
        Task Delete(int id);
        //Metodo para BlockedContacts
        Task <Contact> BlockContact (int id);
        Task<List<Contact>> GetBlockedContacts(int userId);
    }
}
