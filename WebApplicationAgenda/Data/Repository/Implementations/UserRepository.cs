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
            _context.Users.Add(_mapper.Map<User>(dto));
            await _context.SaveChangesAsync();
        }

        public async Task Update(int id_user, CreateAndUpdateUser dto)
        {
            var id = id_user;
            var userItem = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
            var user_update = dto;

            if (userItem != null)
            {
                userItem.Name = user_update.Name;
                userItem.LastName = user_update.LastName;
                userItem.UserName = user_update.UserName;
                userItem.Email = user_update.Email;
                userItem.Password = user_update.Password;
                
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<User>> GetAll()
        {
            return await _context.Users.ToListAsync();    
        }                

        public async Task<User> GetById(int userId)
        {
            return await _context.Users.FindAsync(userId);
        }                 

        public async Task Delete(int id)
        {
            _context.Users.Remove(_context.Users.Single(u => u.Id == id));
            
            await _context.SaveChangesAsync();
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

