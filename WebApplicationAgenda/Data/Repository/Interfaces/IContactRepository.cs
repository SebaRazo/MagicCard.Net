using WebApplicationAgenda.Entities;
using WebApplicationAgenda.Models.Dtos;

namespace WebApplicationAgenda.Data.Repository.Interfaces
{
    public interface IContactRepository
    {
        Task <List<Contact>> GetAll(int userId);
        Task<List<Contact>> GetAllByUser(int id);
        Task Create(CreateAndUpdateContact dto, int userId);
        Task Update(int id, CreateAndUpdateContact dto);
        Task Delete(int id);

        //Metodos para Contactos Bloqueados y Calls
        Task BlockContact (int id);
        Task UnblockContact(int id);
        Task<List<Contact>> GetBlockedContacts(int userId);//sin uso
        Task<List<Contact>> FindAllBlockedByUser(int userId);
        Task<List<Contact>> FindAllNotBlockedByUser(int userId);
        Task<List<Contact>> FindAllBlockedByUserWithCalls(int userId);

        Task<Call> GetCallByContactId(int contactId);
        Task DeleteCallsByContactId(int contactId);

    }
}
