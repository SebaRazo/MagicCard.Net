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

        public async Task Create(CreateAndUpdateCard dto)
        {
            var new_card = new Card()
            {
                
                Title = dto.Title,
                Price = dto.Price,
                Image = dto.Image,
                CardStock = dto.CardStock,
                



            };
            await _context.Cards.AddAsync(new_card);
            await _context.SaveChangesAsync();



        }

        public async Task Delete(int cardId)
        {
            var card = await _context.Cards.SingleAsync(c => c.Id == cardId);
            if (card != null)
            {
                _context.Cards.Remove(card);
                await _context.SaveChangesAsync();

            }
        }

        public async Task<List<Card>> GetAll()
        {

            return await _context.Cards.ToListAsync();
        }

       public async Task<Card> GetById(int cardId)
        {
            return await _context.Cards.FindAsync(cardId);
        }


        public async Task Update(int cardId, CreateAndUpdateCard dto)
        {
            int card_id = cardId;
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

       
 




    }
            
      
}
