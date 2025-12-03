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
using TravailFinSession.Singletons;
using TravailFinSession.Slingletons;
using Windows.Foundation;
using Windows.Foundation.Collections;
// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.
namespace TravailFinSession.Pages.Client
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ListeClientPage : Page
    {
        public ListeClientPage()
        {
            InitializeComponent();
            SingletonClient.getInstance().getAllClients();
            lvListeClients.ItemsSource = SingletonClient.getInstance().Liste;
        }
        private void Modifier_Client(object sender, RoutedEventArgs e)
        {
            Button b = sender as Button;
            Classes.Client client = b.DataContext as Classes.Client;
            if (client is not null)
                this.Frame.Navigate(typeof(ModifieClientPage), client);
        }
    }
}