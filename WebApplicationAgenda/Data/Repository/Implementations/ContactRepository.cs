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
            var new_contact = new Contact() //mapeo de dto a Contact
            {
                CelularNumber = dto.CelularNumber,
                Description = dto.Description,
                Name = dto.Name,
                TelephoneNumber = dto.TelephoneNumber,
                UserId = userId

            };
            await _context.Contacts.AddAsync(new_contact);//Se agrega el nuevo contacto al contexto de la base de datos de manera asincrónica 
            await _context.SaveChangesAsync();

        }

        

        public async Task Delete(int id)
        {
            _context.Contacts.Remove(await _context.Contacts.SingleAsync(c => c.Id == id));//consulta asincrónica para encontrar el contacto en la bd con el ID proporcionado
            await _context.SaveChangesAsync();//se guardan los cambios en la base de datos
        }

        public async Task<List<Contact>> GetAll()
        {
            return await _context.Contacts.ToListAsync();
        }

        public async Task<List<Contact>> GetAllByUser(int id)
        {
            return await _context.Contacts.Where(c=> c.UserId == id).ToListAsync();//filtrar los contactos en base al ID del usuario
        } //se convierte el resultado de la consulta en una lista de contactos y se retorna

        public async Task Update(int id, CreateAndUpdateContact dto)
        {
            int contac_id = id;
            var contacItem=await _context.Contacts.FirstOrDefaultAsync(c => c.Id == contac_id);

            if(contacItem != null)
            {
                var contac_map=_mapper.Map<Contact>(dto); //mapea desde dto a Contact
                contacItem.Name = contac_map.Name;
                contacItem.CelularNumber = contac_map.CelularNumber;
                contacItem.TelephoneNumber = contac_map.TelephoneNumber;

                await _context.SaveChangesAsync();
            }
        }
        public async Task<List<Contact>> GetBlockedContacts(int userId)
        {
            // Obtener la lista de contactos bloqueados para un usuario específico
            var blockedContacts = await _context.Contacts
                .Where(c => c.UserId == userId && c.IsBlocked)
                .ToListAsync();

            return blockedContacts;
        }

        public async Task<Contact> BlockContact(int id)
        {
            var contact = _context.Contacts.FirstOrDefault(c => c.Id == id && !c.IsBlocked);

            if (contact == null)
            {
                // Manejar el caso en el que no se encuentra el contacto
                throw new InvalidOperationException($"No se encontró un contacto con Id {id} que no esté bloqueado.");
            }

            // Marcar el contacto como bloqueado
            contact.IsBlocked = true;

            // Guardar los cambios en la base de datos
            await _context.SaveChangesAsync();

            // Devolver el contacto actualizado
            return contact;
        }


    }
}//podemos simplificar lo de arriba utilizando automapper

/*
        public async Task Update(int id, CreateAndUpdateContact dto)
        {
        var contactItem = await _context.Contacts.FirstOrDefaultAsync(c => c.Id == id);

         if (contactItem != null)
            {
            _mapper.Map(dto, contactItem);
            await _context.SaveChangesAsync();
            }
            }


 */



/*

void IContactRepository.Create(CreateAndUpdateContact dto)
{
    throw new NotImplementedException();
}

void IContactRepository.Delete(int id)
{
    throw new NotImplementedException();
}

List<Contact> IContactRepository.GetAll()
{
    throw new NotImplementedException();
}

void IContactRepository.Update(CreateAndUpdateContact dto)
{
    throw new NotImplementedException();
}*/


