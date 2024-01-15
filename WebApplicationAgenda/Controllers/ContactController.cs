﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebApplicationAgenda.Data;
using WebApplicationAgenda.Data.Repository.Interfaces;
using WebApplicationAgenda.Entities;
using WebApplicationAgenda.Models.Dtos;

namespace WebApplicationAgenda.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ContactController : ControllerBase
    {
        private readonly IContactRepository _contactRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        //inyecta las dependencias 
        public ContactController(IContactRepository contactRepository, IUserRepository userRepository, IMapper mapper)
        {
            _contactRepository = contactRepository;
            _userRepository = userRepository;
            _mapper = mapper;

        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            int userId = Int32.Parse(HttpContext.User.Claims.FirstOrDefault(x => x.Type.Contains("nameidentifier")).Value);
            var contacts= await _contactRepository.GetAll(userId);
            return Ok(contacts); 
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOne(int id)
        {
            try
            {
                int userId = Int32.Parse(HttpContext.User.Claims.FirstOrDefault(x => x.Type.Contains("nameidentifier")).Value);
                List<Contact> contacts = await _contactRepository.GetAllByUser(userId);
                Contact contact = contacts.FirstOrDefault(x => x.Id == id);
                if (contact != null)
                {
                    return Ok(contact);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        public async Task<IActionResult> CreateContact(CreateAndUpdateContact createContactDto)
        {
            try
            {

                int userId = Int32.Parse(HttpContext.User.Claims.FirstOrDefault(x => x.Type.Contains("nameidentifier")).Value);
                await _contactRepository.Create(createContactDto, userId);



                return Created("Created", createContactDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }


        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateContact(int id, CreateAndUpdateContact dto)
        {
            try
            {
                await _contactRepository.Update(id, dto);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

           

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContactById(int id)
        {
            try
            {
                await _contactRepository.Delete(id);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }

            return NoContent();
        }


        [HttpGet("blocked")]
        public async Task<IActionResult> GetAllBlockedByCurrentUser()
        {
            try 
            { 
            //var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            int userId = Int32.Parse(HttpContext.User.Claims.FirstOrDefault(x => x.Type.Contains("nameidentifier")).Value);

            var contacts = await _contactRepository.FindAllBlockedByUserWithCalls(userId);
            if (contacts == null)
            {
                // Manejar el caso en el que no se obtienen contactos bloqueados
                return NotFound("No se encontraron contactos bloqueados.");
            }
            var blockedContactDtos = _mapper.Map<List<BlockedContactWithCallInfoDto>>(contacts);//System.NullReferenceException: 'Object reference not set to an instance of an object.'
            return Ok(blockedContactDtos);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("block/{id}")]
        public async Task<IActionResult> BlockContact(int id)
        {
            try
            {
                _contactRepository.BlockContact(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPost("unblock/{id}")]
        public async Task<IActionResult> UnblockContact(int id)
        {
            try
            {
                _contactRepository.UnblockContact(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("getcall/{contactId}")]
        public async Task<IActionResult> GetCallByContactId(int contactId)
        {
            var call = await _contactRepository.GetCallByContactId(contactId);

            if (call == null)
            {
                return NotFound("No se encontró ninguna llamada con el ID de contacto proporcionado.");
            }

            var callInfoDto = _mapper.Map(call, new CallInfoDto());//<CallInfoDto>(call);//System.NullReferenceException: 'Object reference not set to an instance of an object.'
            if (callInfoDto == null)
            {
               
                return BadRequest("Error al mapear la llamada a CallInfoDto.");
            }


            return Ok(callInfoDto);
        }


        [HttpDelete("deletecalls/{contactId}")]
        public async Task<IActionResult> DeleteCallsByContactId(int contactId)
        {
            try
            {
                _contactRepository.DeleteCallsByContactId(contactId);
                return Ok("Se eliminaron las llamadas asociadas al ID de contacto proporcionado.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }












    }
}


//AGREGADO NUEVO



/*[HttpGet("block/{id}")]//borrar posiblemente
public async Task<IActionResult> BlockContact(int id)
{
    try
    {
        int userId = Int32.Parse(HttpContext.User.Claims.FirstOrDefault(x => x.Type.Contains("nameidentifier")).Value);
        var blockedContact = await _contactRepository.BlockContact(id);

        // Mapear la entidad Contact a un DTO
        var blockedContactDTO = new CreateAndUpdateContact
        {
            Name = blockedContact.Name,
            CelularNumber = blockedContact.CelularNumber,
            TelephoneNumber = blockedContact.TelephoneNumber,
            Description = blockedContact.Description,
            User = blockedContact.User,
            IsBlocked = blockedContact.IsBlocked
        };

        return  Ok(blockedContactDTO);
    }
    catch (Exception ex)
    {
        return BadRequest($"Error al bloquear el contacto: {ex.Message}");
    }
}*/








//REVISAR

/*[HttpGet("blocked")]//borrar posiblemente
public async Task<IActionResult> GetBlockedContacts()
{
    try
    {
        int userId = Int32.Parse(HttpContext.User.Claims.FirstOrDefault(x => x.Type.Contains("nameidentifier")).Value);
        var blockedContacts = await _contactRepository.GetBlockedContacts(userId);

        //
        var blockedContactsDTO = blockedContacts
            .Where(contact=>contact.IsBlocked)
            .Select(contact => new CreateAndUpdateContact
        {
            Name = contact.Name,
            CelularNumber = contact.CelularNumber,
            TelephoneNumber = contact.TelephoneNumber,
            Description = contact.Description,
            User = contact.User,
            IsBlocked = contact.IsBlocked
        }).ToList();


        return Ok(blockedContactsDTO);
    }
    catch(Exception ex)
    {
        return BadRequest($"Error: {ex.Message}");
    }
}*/

