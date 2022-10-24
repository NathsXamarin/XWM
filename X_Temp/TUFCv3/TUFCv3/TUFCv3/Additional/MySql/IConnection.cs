using MySqlConnector;

namespace TUFCv3.Additional.MySql
{
    public interface IConnection
    {
        MySqlConnection connection { get; set; }
        string errorMessage { get; set; }
        bool OpenConnection();
        bool TestConnection();
    }
}