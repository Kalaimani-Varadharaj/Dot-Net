using FraudDetectionRepositoryPatternProject.Models;

namespace FraudDetectionRepositoryPatternProject.Repository
{
    public interface IUserRegisterRepository
    {
        bool UsernameExists(string username);
        bool RegisterUser(UserRegister user);

        bool ValidateLogin(UserLogin login);

        //bool LoginUser(UserLogin login);
        public bool ValidatePassword(string userName, string password);
    }
        
}
