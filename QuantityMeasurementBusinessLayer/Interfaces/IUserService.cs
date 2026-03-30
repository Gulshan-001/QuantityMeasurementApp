using QuantityMeasurementModel.DTOs;

namespace QuantityMeasurementBusinessLayer.Interfaces
{
    public interface IUserService
    {
        string Register(RegisterDTO dto);
        string Login(LoginDTO dto);
        string GoogleLogin(string idToken);
    }
}