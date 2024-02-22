using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApplicationAgenda.Data.Repository.Interfaces;
using WebApplicationAgenda.Entities;
using WebApplicationAgenda.Models.Dtos;

namespace WebApplicationAgenda.Data.Repository.Implementations
{
    public class SaleRepository: ISaleRepository
    {
        private readonly AgendaContext _context;
        private readonly IMapper _mapper;
        public SaleRepository(AgendaContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task Create(CreateAndUpdateSale dto, int userId)
        {
            _context.Sales.Add(_mapper.Map<Sale>(dto));
            await _context.SaveChangesAsync();            

        }

        public async Task Delete(int userId)
        {
            var sale = await _context.Sales.SingleAsync(c => c.Id == userId);
            if (sale != null)
            {
                _context.Sales.Remove(sale);
                await _context.SaveChangesAsync();

            }
        }

        public async Task<List<Sale>> GetAll(int userId)
        {

            return await _context.Sales.ToListAsync();
        }

        public async Task<List<Sale>> GetAllByUser(int id)
        {
            return await _context.Sales.ToListAsync();
        }


        public async Task Update(int id, CreateAndUpdateSale dto)
        {
            int sale_id = id;
            var saleItem = await _context.Sales.FirstOrDefaultAsync(c => c.Id == sale_id);

            if (sale_id != null)
            {
                var sale_map = _mapper.Map<Sale>(dto);
                saleItem.UserId = sale_map.UserId;
                saleItem.CardId = sale_map.CardId;
                saleItem.Total = sale_map.Total;
                saleItem.Date = sale_map.Date;
              

                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<ReportSalesDto>> SalesInMonth(int month, int year)
        {
            return await _context.Sales
                .Include(r => r.User)
                .Where(r => r.Date.Month == month && r.Date.Year == year)
                .Select(r => new ReportSalesDto
                {
                    Id = r.Id,
                    Date = r.Date,
                    Total = r.Total,
                    Name = r.User.Name,
                    LastName = r.User.LastName,
                    CardId = r.CardId
                    
                })
                .ToListAsync();
        }



    }
}
