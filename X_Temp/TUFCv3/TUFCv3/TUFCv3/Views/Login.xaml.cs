using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TUFCv3.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Login : ContentPage
    {
        public Login()
        {
            InitializeComponent();
            AddImages();
        }

        void AddImages()
        {
            // Set the image's source to the path of the image 
            //  and add the View as the second parameter ie: 'typeof(Login)' 
            flex.Source = ImageSource.FromResource("TUFCv3.Additional.Images.Flex.jpg", typeof(Login));
        }
    }
}