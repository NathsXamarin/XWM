using System.Threading.Tasks;
using TUFCv3.Models.Users;

namespace TUFCv3.Additional.AuthenticateUser
{
    public interface IAuthenticateUser
    {
        string errorMessage { get; set; }
        Task<bool> Authenticate(IUser loginUser);
        bool ComparePasswords(IUser loginUser);
        bool GetDatabaseUser(IUser loginUser);
    }
}