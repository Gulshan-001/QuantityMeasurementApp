using QuantityMeasurementModel.Entities;

namespace QuantityMeasurementRepository.Interfaces
{
    public interface IUserRepository
    {
        void AddUser(UserEntity user);
        UserEntity? GetByEmail(string email);
    }
}