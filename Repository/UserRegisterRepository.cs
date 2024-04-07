using FraudDetectionRepositoryPatternProject.Models;

namespace FraudDetectionRepositoryPatternProject.Repository
{
    public class UserRegisterRepository : IUserRegisterRepository
    {
        private readonly FraudRiskManagementContext _context;

        //Initializing the FraudRiskManagementContext instance which is received as a arugument
        public UserRegisterRepository(FraudRiskManagementContext context)
        {
            _context = context;
        }

        //intializing the FraudRiskManagementContext instance
        public UserRegisterRepository()
        {
            _context = new FraudRiskManagementContext();
        }

        public bool UsernameExists(string userName)
        {
            return _context.UserRegisters.Any(u => u.UserName == userName);
        }
        public bool RegisterUser(UserRegister user)
        {
            try
            {
                _context.UserRegisters.Add(user);
                _context.SaveChanges();
                return true; // Registration successful
            }
            catch (Exception ex)
            {
                // Log or handle the exception as needed
                return false; // Registration failed
            }
        }

        public bool ValidateLogin(UserLogin login)
        {
            // Validate login credentials against your data store (e.g., database)
            return _context.UserRegisters.Any(u => u.UserName == login.UserName && u.Password == login.Password);
        }

        public bool ValidatePassword(string userName, string password)
        {
            // Validate the provided password for the given username
            return _context.UserRegisters.Any(u => u.UserName == userName && u.Password == password);
        }

    }
}
