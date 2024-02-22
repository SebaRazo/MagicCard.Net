using WebApplicationAgenda.Entities;
using WebApplicationAgenda.Models.Dtos;

namespace WebApplicationAgenda.Data.Repository.Interfaces
{
    public interface ISaleRepository
    {
        Task<List<Sale>> GetAll(int userId);
        Task<List<Sale>> GetAllByUser(int id);
        Task Create(CreateAndUpdateSale dto, int userId);
        Task Update(int id, CreateAndUpdateSale dto);
        Task Delete(int id);

        Task<List<ReportSalesDto>> SalesInMonth(int month, int year);

        //Task<List<Card>> FindAllByUserWithCards(int userId);

        //Task<Card> GetCardByContactId(int userId);

        //Task DeleteCardByContactId(int userId);
    }
}
