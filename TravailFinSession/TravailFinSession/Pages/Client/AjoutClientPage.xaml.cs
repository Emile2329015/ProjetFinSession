using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace TravailFinSession.Pages.Client
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AjoutClientPage : Page
    {
        private readonly Random _rand = new Random();


        public AjoutClientPage()
        {
            InitializeComponent();
        }


        private void GenererId_Click(object sender, RoutedEventArgs e)
        {
            // Génère un identifiant aléatoire 100..999
            int id = _rand.Next(100, 1000);
            TbxClientId.Text = id.ToString();
            tblerreurClientId.Text = "";
        }
        private void Ajouter_Client(object sender, RoutedEventArgs e)
        {
            tblerreurClientId.Text = "";
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

            if (TbxClientId.Text.Trim() == "")
            {
                tblerreurClientId.Text = "Générez un identifiant pour le client";
                valide = false;
            }

            //if (valide)
            //{
            //    int idClient = int.Parse(TbxClientId.Text.Trim());
            //    string nom = TbxClientNom.Text.Trim();
            //    string adresse = TbxClientAdresse.Text.Trim();
            //    string telephone = TbxClientTelephone.Text.Trim();
            //    string email = TbxClientEmail.Text.Trim();
            //    Classes.Client nouveauClient = new Classes.Client(idClient, nom, adresse, telephone, email);
            //    Donnees.DonneesClients.AjouterClient(nouveauClient);
            //    TbxClientId.Text = "";
            //    TbxClientNom.Text = "";
            //    TbxClientAdresse.Text = "";
            //    TbxClientTelephone.Text = "";
            //    TbxClientEmail.Text = "";
            //    var dialog = new ContentDialog()
            //    {
            //        Title = "Succès",
            //        Content = "Le client a été ajouté avec succès.",
            //        CloseButtonText = "OK"
            //    };
            //    _ = dialog.ShowAsync();
            //}
        }
    }
}
