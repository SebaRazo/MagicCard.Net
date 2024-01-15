using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApplicationAgenda.Data.Repository.Interfaces;
using WebApplicationAgenda.Entities;
using WebApplicationAgenda.Models.Dtos;

namespace WebApplicationAgenda.Data.Repository.Implementations
{
    public class UserRepository : IUserRepository
    {
        private readonly AgendaContext _context;  // Inyectamos el Context y el Mapper
        private readonly IMapper _mapper;
        public UserRepository(AgendaContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task Create(CreateAndUpdateUser dto)
        {
            _context.Users.Add(_mapper.Map<User>(dto));//se mapea de dto a user 
            await _context.SaveChangesAsync();
        }

        public async Task Update(int id_user, CreateAndUpdateUser dto)
        {
            var id = id_user;
            var userItem = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);//consulta asincrónica para obtener el primer usuario con el ID proporcionado
            var user_update = dto;

            if (userItem != null)//verifica si se encontró un usuario con el ID proporcionado
            {
                userItem.Name = user_update.Name;
                userItem.LastName = user_update.LastName;
                userItem.UserName = user_update.UserName;
                userItem.Email = user_update.Email;
                userItem.Password = user_update.Password;
                //se actualizan las propiedades del userItem con los valores proporcionados en el objeto dto
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<User>> GetAll()
        {
            return await _context.Users.ToListAsync(); //obtiene los usuarios en BD y los devuelve como lista   
        }                //consulta asincrónica

        public async Task<User> GetById(int userId)
        {
            return await _context.Users.FindAsync(userId);
        }                 //consulta asincrónica

        public async Task Delete(int id)
        {
            _context.Users.Remove(_context.Users.Single(u => u.Id == id));
            //consulta asincrónica
            await _context.SaveChangesAsync();//guarda los cambios en BD
        }


        public async Task<User> Validate(AuthenticationRequestBody authRequestBody)
        {
            return await _context.Users.FirstAsync(p => p.UserName == authRequestBody.UserName && p.Password == authRequestBody.Password);
        }

        public async Task<bool> UserExists(int userId)
        {
            return await _context.Users.AnyAsync(u => u.Id == userId);
        }
    }
}

