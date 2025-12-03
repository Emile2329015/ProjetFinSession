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
using System.Text.RegularExpressions;
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
    public sealed partial class AjoutClientPage : Page
    {
        private readonly Random _rand = new Random();
        public AjoutClientPage()
        {
            InitializeComponent();
        }
        private async void Ajouter_Client(object sender, RoutedEventArgs e)
        {
            string patternEmail = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            string patternTelephone = @"^\+?\d{7,15}$";
            bool valide = true;
            tblerreurClientNom.Text = "";
            tblerreurClientAdresse.Text = "";
            tblerreurClientTelephone.Text = "";
            tblerreurClientEmail.Text = "";
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
            else if (!Regex.IsMatch(TbxClientTelephone.Text.Trim(), patternTelephone))
            {
                tblerreurClientTelephone.Text = "Numéro de téléphone invalide (format requis: 4393456789";
                valide = false;
            }
            if (!Regex.IsMatch(TbxClientEmail.Text.Trim(), patternEmail))
            {
                tblerreurClientEmail.Text = "Entrez un courriel valide(format requis: e@gmail.com)";
                valide = false;
            }
            if (valide)
            {
                SingletonClient.getInstance().ajouterClient(
                    TbxClientNom.Text.Trim(),
                    TbxClientAdresse.Text.Trim(),
                    TbxClientTelephone.Text.Trim(),
                    TbxClientEmail.Text.Trim()
                    );
                var dlg = new ContentDialog
                {
                    Title = "Succès",
                    Content = "le client a été ajouté avec succès.",
                    CloseButtonText = "OK",
                    XamlRoot = this.XamlRoot
                };
                _ = dlg.ShowAsync();
                TbxClientNom.Text = "";
                TbxClientAdresse.Text = "";
                TbxClientTelephone.Text = "";
                TbxClientEmail.Text = "";
            }

        }
    }
}