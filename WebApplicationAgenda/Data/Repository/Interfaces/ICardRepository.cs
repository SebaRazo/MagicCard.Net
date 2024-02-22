using WebApplicationAgenda.Entities;
using WebApplicationAgenda.Models.Dtos;

namespace WebApplicationAgenda.Data.Repository.Interfaces
{
    public interface ICardRepository
    {
        Task<List<Card>> GetAll();
        Task<Card> GetById(int cardId);
        Task Create(CreateAndUpdateCard dto);
        Task Update(int cardId, CreateAndUpdateCard dto);
        Task Delete(int cardId);
        //estos tres ya estan en sale
        //Task<List<Card>> FindAllByUserWithCards(int userId);

        //Task<Card> GetCardByContactId(int userId);

        //Task DeleteCardByContactId(int userId);


    }
}
