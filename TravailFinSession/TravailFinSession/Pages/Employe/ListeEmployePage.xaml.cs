using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Microsoft.WindowsAppSDK.Runtime.Packages;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using TravailFinSession.Slingletons;
using TravailFinSession.Classes;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace TravailFinSession.Pages.Employe
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ListeEmployePage : Page
    {
        public ListeEmployePage()
        {
            InitializeComponent();
            //      gvListeEmploye.ItemsSource = SingletonEmploye.getInstance().Liste;
            //        SingletonEmploye.getInstance().getAllEmploye();
        }

        private void Modifier_Employe(object sender, RoutedEventArgs e)
        {

            Button b = sender as Button;
            Classes.Employe employe = b.DataContext as Classes.Employe;

            //if (Classes.Employe is not null)
            //    SingletonEmploye.getInstance().modifierEmploye(Employe.Code);


        }

        private void gvListeEmploye_ItemClick(object sender, ItemClickEventArgs e)
        {

        }
        //}
    }
}