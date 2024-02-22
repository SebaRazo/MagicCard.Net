using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApplicationAgenda.Data.Repository.Interfaces;
using WebApplicationAgenda.Entities;
using WebApplicationAgenda.Models.Dtos;

namespace WebApplicationAgenda.Data.Repository.Implementations
{
    public class CardRepository: ICardRepository

    {
        private readonly AgendaContext _context;
        private readonly IMapper _mapper;
        public CardRepository(AgendaContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task Create(CreateAndUpdateCard dto, int userId)
        {
            var new_card = new Card()
            {
                
                Title = dto.Title,
                Price = dto.Price,
                Image = dto.Image,
                CardStock = dto.CardStock,
                UserId = userId,



            };
            await _context.Cards.AddAsync(new_card);
            await _context.SaveChangesAsync();



        }

        public async Task Delete(int id)
        {
            var card = await _context.Cards.SingleAsync(c => c.Id == id);
            if (card != null)
            {
                _context.Cards.Remove(card);
                await _context.SaveChangesAsync();

            }
        }

        public async Task<List<Card>> GetAll(int userId)
        {

            return await _context.Cards.Where(x => x.UserId == userId).Include(x => x.Sale).ToListAsync();
        }//revisar

        public async Task<List<Card>> GetAllByUser(int id)
        {
            return await _context.Cards.Where(c => c.UserId == id).ToListAsync();
        }


        public async Task Update(int id, CreateAndUpdateCard dto)
        {
            int card_id = id;
            var cardItem = await _context.Cards.FirstOrDefaultAsync(c => c.Id == card_id);

            if (card_id != null)
            {
                var card_map = _mapper.Map<Card>(dto);
                cardItem.Title = card_map.Title;
                cardItem.Price = card_map.Price;
                cardItem.CardStock = card_map.CardStock;
                cardItem.Image = card_map.Image;



                await _context.SaveChangesAsync();
            }
        }

       
        public async Task<Card> GetCardByContactId(int userId)
        {
            return await _context.Cards
                .FirstOrDefaultAsync(c => c.UserId == userId);
        }

        public async Task DeleteCardByContactId(int userId)
        {
            var cards = await _context.Cards
                .Where(c => c.UserId == userId)
                .ToListAsync();

            _context.Cards.RemoveRange(cards);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Card>> FindAllByUserWithCards(int userId)
        {
            return await _context.Cards.Where(c => c.UserId == userId).ToListAsync();

        }




    }
            
      
}
