using TUFCv3.Models.Users;

namespace TUFCv3.Additional.MySql
{
    public interface IGetMySqlData
    {
        string errorMessage { get; set; }
        bool ConvertToProperties();
        bool GetData(IUser loginUser);
        bool OpenConnection();
        IUser RunQuery(IUser loginUser);
    }
}
