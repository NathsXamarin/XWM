using System;
using System.Threading.Tasks;

namespace TUFCv3.Additional.Navigation
{
    public interface INavigate
    {
        Task GoToPage(Type selectedPage);
    }
}