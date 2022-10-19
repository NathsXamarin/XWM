using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace TUFCv3.Additional.Navigation
{
    public class Navigate : INavigate
    {
        /*  GoToPage()
            Naviagate to a new page  */
        public async Task GoToPage(Type selectedPage)
        {
            Page page = (Page)Activator.CreateInstance(selectedPage);       // Create the page
            await Application.Current.MainPage.Navigation.PushModalAsync(page);     //  and navigate to it.
        }
    }
}
