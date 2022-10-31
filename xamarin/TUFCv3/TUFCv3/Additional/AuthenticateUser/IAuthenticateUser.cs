using System.Threading.Tasks;
using TUFCv3.Models.Users;

namespace TUFCv3.Additional.AuthenticateUser
{
    public interface IAuthenticateUser
    {
        IUser databaseUser { get; set; }
        string errorMessage { get; set; }
        Task<bool> Authenticate(IUser loginUser);
        bool ComparePasswords(IUser loginUser);
        bool GetDatabaseUser(IUser loginUser);
    }
}