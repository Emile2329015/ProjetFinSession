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
using TravailFinSession.Pages.Client;
using TravailFinSession.Pages.Employe;
using TravailFinSession.Pages.Projet;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace TravailFinSession
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.ExtendsContentIntoTitleBar = true; // Extend the content into the title bar and hide the default titlebar
            this.SetTitleBar(titleBar); // Set the custom title bar
            mainFrame.Navigate(typeof(ListeProjetPage));
        }

        private void navView_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
            if (args.InvokedItemContainer is NavigationViewItem item)
            {
                switch (item.Tag)
                {
                    case "listeProjet":
                        mainFrame.Navigate(typeof(ListeProjetPage));
                        break;
                    case "addProjet":
                        mainFrame.Navigate(typeof(AjoutProjetPage));
                        break;

                    case "listeEmploye":
                        mainFrame.Navigate(typeof(ListeEmployePage));
                        break;
                    case "addEmploye":
                        mainFrame.Navigate(typeof(AjoutEmployePage));
                        break;
                    case "listeClient":
                        mainFrame.Navigate(typeof(ListeClientPage));
                        break;
                    case "addClient":
                        mainFrame.Navigate(typeof(AjoutClientPage));
                        break;

                    default:
                        break;
                }
            }
        }

        private async void MenuFlyoutItem_Click(object sender, RoutedEventArgs e)
        {
           
            
        }
    }
}

