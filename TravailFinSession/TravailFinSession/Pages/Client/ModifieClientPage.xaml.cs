using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Imaging;
using Microsoft.UI.Xaml.Navigation;
using Microsoft.UI.Xaml.Shapes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using TravailFinSession.Singletons;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace TravailFinSession.Pages.Client
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ModifieClientPage : Page
    {
        Classes.Client clientAModifier;
        public ModifieClientPage()
        {
            InitializeComponent();
        }

        private async void Modifier_Client(object sender, RoutedEventArgs e)
        {

            tblerreurClientNom.Text = "";
            tblerreurClientAdresse.Text = "";
            tblerreurClientTelephone.Text = "";
            tblerreurClientEmail.Text = "";


            bool valide = true;

            if (TbxClientNom.Text.Trim() == "")
            {
                tblerreurClientNom.Text = "Entrez le nom du client";
                valide = false;
            }

            if (TbxClientAdresse.Text.Trim() == "")
            {
                tblerreurClientAdresse.Text = "Entrez l'adresse du client";
                valide = false;
            }

            if (TbxClientTelephone.Text.Trim() == "")
            {
                tblerreurClientTelephone.Text = "Entrez le téléphone du client";
                valide = false;
            }

            if (string.IsNullOrWhiteSpace(TbxClientEmail.Text) || !TbxClientEmail.Text.Contains("@"))
            {
                tblerreurClientEmail.Text = "Entrez un courriel valide";
                valide = false;
            }

            if (valide)
            {
                clientAModifier.Nom = TbxClientNom.Text.Trim();
                clientAModifier.Adresse = TbxClientAdresse.Text.Trim();
                clientAModifier.Telephone = TbxClientTelephone.Text.Trim();
                clientAModifier.Email = TbxClientEmail.Text.Trim();

                SingletonClient.getInstance().modifierClient(
                    clientAModifier.Identifiant,
                    clientAModifier.Nom,
                    clientAModifier.Adresse,
                    clientAModifier.Telephone,
                    clientAModifier.Email
                    );

            }
            var dlg = new ContentDialog
            {
                Title = "Succès",
                Content = "Le client a été modifié avec succès.",
                CloseButtonText = "OK",
                XamlRoot = this.XamlRoot
            };
            await dlg.ShowAsync();
            this.Frame.Navigate(typeof(ListeClientPage));
                
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            clientAModifier = e.Parameter as Classes.Client;
            if (clientAModifier != null)
            {
                TbxClientNom.Text = clientAModifier.Nom;
                TbxClientAdresse.Text = clientAModifier.Adresse;
                TbxClientTelephone.Text = clientAModifier.Telephone;
                TbxClientEmail.Text = clientAModifier.Email;


            }
        }


    }
}