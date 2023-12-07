using WebApplicationAgenda.Entities;
using WebApplicationAgenda.Models.Dtos;

namespace WebApplicationAgenda.Data.Repository.Interfaces
{
    public interface IContactRepository
    {
        Task <List<Contact>> GetAll();
        Task<List<Contact>> GetAllByUser(int id);
        Task Create(CreateAndUpdateContact dto);
        Task Update(int id, CreateAndUpdateContact dto);
        Task Delete(int id); 

        //podriamos hacer uno para los contactos en lista negra Task <List<Contact>> GetBloked(int id)
    }
}
