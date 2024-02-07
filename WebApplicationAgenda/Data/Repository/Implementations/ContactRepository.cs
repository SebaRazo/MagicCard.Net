using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApplicationAgenda.Data;
using WebApplicationAgenda.Data.Repository.Interfaces;
using WebApplicationAgenda.Entities;
using WebApplicationAgenda.Models.Dtos;

namespace WebApplicationAgenda.Data.Repository.Implementations
{
    public class ContactRepository : IContactRepository 
    {
        private readonly AgendaContext _context;
        private readonly IMapper _mapper;
        public ContactRepository(AgendaContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task Create(CreateAndUpdateContact dto, int userId)
        {
            var new_contact = new Contact() 
            {
                CelularNumber = dto.CelularNumber,
                Description = dto.Description,
                Name = dto.Name,
                TelephoneNumber = dto.TelephoneNumber,
                UserId = userId,
                IsBlocked = false


            };
            await _context.Contacts.AddAsync(new_contact);
            await _context.SaveChangesAsync();
            
            await CreateCall(new_contact.Id);

        }


        public async Task Delete(int id)
        {
            var contact =await _context.Contacts.SingleAsync(c => c.Id == id);
            if (contact != null)
            {
                _context.Contacts.Remove(contact);
                await _context.SaveChangesAsync();
                
            }
        }



        public async Task<List<Contact>> GetAll(int userId)
        {
            
            return await _context.Contacts.Where(x => x.UserId == userId).Include(x => x.Calls).ToListAsync();
        }

        public async Task<List<Contact>> GetAllByUser(int id)
        {
            return await _context.Contacts.Where(c=> c.UserId == id).ToListAsync();
        } 

        public async Task Update(int id, CreateAndUpdateContact dto)
        {
            int contac_id = id;
            var contacItem=await _context.Contacts.FirstOrDefaultAsync(c => c.Id == contac_id);

            if(contacItem != null)
            {
                var contac_map=_mapper.Map<Contact>(dto); 
                contacItem.Name = contac_map.Name;
                contacItem.CelularNumber = contac_map.CelularNumber;
                contacItem.TelephoneNumber = contac_map.TelephoneNumber;
                contacItem.Description = contac_map.Description;

                await _context.SaveChangesAsync();
            }
        }




        public async Task<List<Contact>> GetBlockedContacts(int userId)//sin uso
        {
            
            var blockedContacts = await _context.Contacts
                .Where(c => c.UserId == userId && c.IsBlocked)
                .ToListAsync();

            return blockedContacts;
        }

        public async Task BlockContact(int id)
        {
            var contact = _context.Contacts.FirstOrDefault(c => c.Id == id && !c.IsBlocked);

            if (contact == null)
            {
                
                throw new InvalidOperationException($"No se encontró un contacto con Id {id} que no esté bloqueado.");
            }

            
            contact.IsBlocked = true;

            
            await _context.SaveChangesAsync();

        }
        public async Task UnblockContact(int id)
        {
            var contact = _context.Contacts.FirstOrDefault(c => c.Id == id && c.IsBlocked);
            if(contact !=null)
            {
                contact.IsBlocked = false;
                await _context.SaveChangesAsync(); 
            }
        }

        public async Task<List<Contact>> FindAllBlockedByUser(int userId)
        {
            return await _context.Contacts
                .Where(c => c.UserId == userId && c.IsBlocked)
                .ToListAsync();
        }

        public async Task<List<Contact>> FindAllNotBlockedByUser(int userId)
        {
            return await _context.Contacts
                .Where(c => c.UserId == userId && !c.IsBlocked)
                .OrderBy(c => c.Name)
                .ToListAsync();
        }

        public async Task<List<Contact>> FindAllBlockedByUserWithCalls(int userId)
        {
            var contacts = await _context.Contacts
                .Include(c => c.Calls)
                .Where(c => c.UserId == userId && c.IsBlocked)
                .ToListAsync();

           
            contacts = contacts.OrderByDescending(c => c.Calls.Sum(call => call.CountCall)).ToList();

            return contacts;

        }

        public async Task CreateCall(int contactId)
        {
            var call = new Call
            {
                ContactId = contactId,
                CountCall = new Random().Next(1, 101),
                TimeCall = DateTime.Now
            };

            _context.Calls.Add(call);
            await _context.SaveChangesAsync();
        }

        public async Task<Call> GetCallByContactId(int contactId)
        {
            return await _context.Calls
                .FirstOrDefaultAsync(c => c.ContactId == contactId);
        }

        public async Task DeleteCallsByContactId(int contactId)
        {
            var calls = await _context.Calls
                .Where(c => c.ContactId == contactId)
                .ToListAsync();

            _context.Calls.RemoveRange(calls);
            await _context.SaveChangesAsync();
        }


    }
}





