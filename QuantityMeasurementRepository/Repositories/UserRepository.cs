using QuantityMeasurementModel.Entities;
using QuantityMeasurementRepository.Context;
using QuantityMeasurementRepository.Interfaces;
using System.Linq;

namespace QuantityMeasurementRepository.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void AddUser(UserEntity user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public UserEntity? GetByEmail(string email)
        {
            return _context.Users.FirstOrDefault(u => u.Email == email);
        }

        public IEnumerable<UserEntity> GetAllUsers()
        {
            return _context.Users.ToList();
        }
    }
}