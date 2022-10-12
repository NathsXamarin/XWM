using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace TUFCv3.Additional
{
    public class Navigation
    {
        /*  GoToPage()
            Naviagate to a new page  */
        public async Task GoToPage(Type selectedPage)
        {
            Page page = (Page)Activator.CreateInstance(selectedPage);       // Create the page
            await App.Current.MainPage.Navigation.PushModalAsync(page);                 //  and navigate to it.
        }
    }
}
