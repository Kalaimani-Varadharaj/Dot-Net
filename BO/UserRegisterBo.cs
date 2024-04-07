using FraudDetectionRepositoryPatternProject.Models;
using FraudDetectionRepositoryPatternProject.Repository;
using Microsoft.EntityFrameworkCore;

namespace FraudDetectionRepositoryPatternProject.BO
{
    public class UserRegisterBo
    {
        private readonly IUserRegisterRepository _userRegisterRepository;

        public UserRegisterBo(IUserRegisterRepository userRegisterRepository)
        {
            _userRegisterRepository = userRegisterRepository;
        }

        public bool RegisterUser(UserRegister user)
        {
            try
            {
                // Check if the username already exists before attempting to register
                if (!_userRegisterRepository.UsernameExists(user.UserName))
                {
                    // Username is unique, proceed with registration
                    bool result = _userRegisterRepository.RegisterUser(user);
                    return result;
                }
                else
                {
                    // Username already exists, registration failed
                    return false;
                }
            }
            catch (Exception ex)
            {
                // Log or handle the exception as needed
                return false; // Registration failed
            }
        }

        public bool UsernameExists(string username)
        {
            // Utilize the repository to check if the username exists
            return _userRegisterRepository.UsernameExists(username);
        }


        public bool ValidateLogin(UserLogin login)
        {
            // Utilize the repository to validate login credentials
            return _userRegisterRepository.ValidateLogin(login);
        }

        public bool ValidatePassword(string username, string password)
        {
            // Utilize the repository to validate the provided password for the given username
            return _userRegisterRepository.ValidatePassword(username, password);
        }
    }
}
