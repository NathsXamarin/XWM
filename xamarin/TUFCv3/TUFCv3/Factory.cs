using System;
using System.Collections.Generic;
using System.Text;
using TUFCv3.Additional.AuthenticateUser;
using TUFCv3.Additional.Encryption;
using TUFCv3.Additional.MySql;
using TUFCv3.Additional.Navigation;
using TUFCv3.Models.Users;

namespace TUFCv3
{
    public static class Factory
    {
        public static IAuthenticateUser CreateAuthenticateUser()
        {
            return new AuthenticateUser();
        }

        public static IConnection CreateConnection()
        {
            return new Connection();
        }

        public static IEncryptionCbcCombo CreateEncryptionCbcCombo()
        {
            return new EncryptionCbc();
        }

        public static IGetMySqlData CreateGetLoginDetails()
        {
            return new GetLoginDetails();
        }

        public static INavigate CreateNavigate()
        {
            return new Navigate();
        }

        public static IUser CreateUser()
        {
            return new User();
        }
    }
}
