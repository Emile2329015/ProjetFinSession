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

namespace TravailFinSession.Pages.Projet
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AjoutProjetPage : Page
    {
        public AjoutProjetPage()
        {
            InitializeComponent();
        }

        private void Ajouter_Projet(object sender, RoutedEventArgs e)
        {
            bool valide = true;

            tblerreurProjetNumero.Text = "";
            tblerreurProjetTitre.Text = "";
            tblerreurProjetDateDebut.Text = "";
            tblerreurProjetDescription.Text = "";
            tblerreurProjetBudget.Text = "";
            tblerreurProjetNbEmployes.Text = "";
            tblerreurProjetTotalSalaires.Text = "";
            tblerreurProjetClientId.Text = "";
            tblerreurProjetStatut.Text = "";

            if (TbxProjetNumero.Text.Trim() == "")
            {
                tblerreurProjetNumero.Text = "Vous devez entrer le numéro du projet";
                valide = false;
            }

            if (TbxProjetTitre.Text.Trim() == "")
            {
                tblerreurProjetTitre.Text = "Vous devez entrer le titre du projet";
                valide = false;
            }

            if(DpProjetDebut.Date == null)
            {
                tblerreurProjetDateDebut.Text = "Vous devez entrer la date de début du projet";
                valide = false;
            }

            if (TbxProjetDescription.Text.Trim() == "")
            {
                tblerreurProjetDescription.Text = "Vous devez entrer la description du projet";
                valide = false;
            }
            double budget;
            if (!double.TryParse(TbxProjetBudget.Text.Trim(), out budget) || budget <= 0)
            {
                tblerreurProjetBudget.Text = "Entrez un budget valide (>0)";
                valide = false;
            }

            int nbEmployes;
            if (!int.TryParse(TbxProjetNbEmployes.Text.Trim(), out nbEmployes) || nbEmployes < 1 || nbEmployes > 5)
            {
                tblerreurProjetNbEmployes.Text = "Nombre d'employés requis entre 1 et 5";
                valide = false;
            }

            double totalSalaires;
            if (!double.TryParse(TbxProjetTotalSalaires.Text.Trim(), out totalSalaires) || totalSalaires < 0)
            {
                tblerreurProjetTotalSalaires.Text = "Entrez un total de salaires valide";
                valide = false;
            }


            if (TbxProjetClientId.Text.Trim() == "")
            {
                tblerreurProjetClientId.Text = "Vous devez entrer l'ID du client pour le projet";
                valide = false;
            }
          //  else if()

            if (CbxProjetStatut.SelectedItem == null)
            {
                tblerreurProjetStatut.Text = "Le statut doit être sélectionné";
                valide = false;
            }




            if (valide)
            {
                //string numero = TbxProjetNumero.Text.Trim();
                //string titre = TbxProjetTitre.Text.Trim();
                //string description = TbxProjetDescription.Text.Trim();
                //string clientId = TbxProjetClientId.Text.Trim();
                //string statut = (CbxProjetStatut.SelectedItem as ComboBoxItem)?.Content.ToString() ?? "En cours";

                //Projet nouveauProjet = new Projet(numero, titre, dateDebut, description, budget, nbEmployes, totalSalaires, clientId, statut);

                //Donnees.DonneesProjets.AjouterProjet(nouveauProjet);

                //TbxProjetNumero.Text = "";
                //TbxProjetTitre.Text = "";
                //DpProjetDebut.SelectedDate = null;
                //TbxProjetDescription.Text = "";
                //TbxProjetBudget.Text = "";
                //TbxProjetNbEmployes.Text = "";
                //TbxProjetTotalSalaires.Text = "";
                //TbxProjetClientId.Text = "";
                //CbxProjetStatut.SelectedIndex = 0;

                //var dialog = new ContentDialog()
                //{
                //    Title = "Succès",
                //    Content = "Le projet a été ajouté avec succès.",
                //    CloseButtonText = "OK"
                //};
                //_ = dialog.ShowAsync();
            }

        }



        private void BtnGenererNumero_Click(object sender, RoutedEventArgs e)
        {
            string clientId = TbxProjetClientId.Text.Trim();
            if (!string.IsNullOrEmpty(clientId))
            {
                Random rand = new Random();
                int valeur = rand.Next(1, 100);
                string annee = DateTime.Today.Year.ToString();
                TbxProjetNumero.Text = $"{clientId}-{valeur:D2}-{annee}";
            }
            else
            {
                tblerreurProjetClientId.Text = "Entrez d'abord l'identifiant du client";
            }

        }
    }
}
