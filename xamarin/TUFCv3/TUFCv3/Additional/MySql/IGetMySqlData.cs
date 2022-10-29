using MySqlConnector;
using TUFCv3.Models.Users;

namespace TUFCv3.Additional.MySql
{
    public interface IGetMySqlData
    {
        MySqlDataReader reader { get; set; }
        string errorMessage { get; set; }
        bool ConvertToProperties();
        bool GetData(IUser loginUser);
        bool OpenConnection();

        void CloseConnection();
        IUser RunQuery(IUser loginUser);
    }
}
