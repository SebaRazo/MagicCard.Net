using WebApplicationAgenda.Entities;
using WebApplicationAgenda.Models.Dtos;

namespace WebApplicationAgenda.Data.Repository.Interfaces
{
    public interface ICardRepository
    {
        Task<List<Card>> GetAll(int userId);
        Task<List<Card>> GetAllByUser(int id);
        Task Create(CreateAndUpdateCard dto, int userId);
        Task Update(int id, CreateAndUpdateCard dto);
        Task Delete(int id);
        //estos tres ya estan en sale
        Task<List<Card>> FindAllByUserWithCards(int userId);

        Task<Card> GetCardByContactId(int userId);

        Task DeleteCardByContactId(int userId);


    }
}
