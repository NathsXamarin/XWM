using System;
using System.Collections.Generic;
using System.Text;

namespace TUFCv3.Additional.Encryption
{
    public interface IEncryptionCbcCombo : IEncryption, IEncryptionCbc
    {
    }
}
