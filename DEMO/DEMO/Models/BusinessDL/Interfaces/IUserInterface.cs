using static DEMO.Models.DTO.UserLogin.UserLoginInfo;

namespace DEMO.Models.BusinessDL.Interfaces
{
    public interface IUserInterface
    {
        Task<User?> ValidateUserAsync(string LOGIN_NAME, string PASSWORD);
    }
}
