using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Microsoft.UI.Xaml.Shapes;
using Microsoft.WindowsAppSDK.Runtime.Packages;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using TravailFinSession.Classes;
using TravailFinSession.Singletons;
using TravailFinSession.Slingletons;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace TravailFinSession.Pages.Employe
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AjoutEmployePage : Page
    {
       
        public AjoutEmployePage()
        {
            InitializeComponent();
        }

        private async void Ajouter_Employe(object sender, RoutedEventArgs e)
        {
            bool valide = true;

            tblerreurNom.Text = "";
            tblerreurPrenom.Text = "";
            tblerreurDateNaissance.Text = "";
            tblerreurEmail.Text = "";
            tblerreurAdresse.Text = "";
            tblerreurDateEmbauche.Text = "";
            tblerreurTauxHoraire.Text = "";
            tblerreurUrl.Text = "";
            tblerreurStatut.Text = "";


            if (string.IsNullOrWhiteSpace(TbxNom.Text))
            {
                tblerreurNom.Text = "lenom est obligatoire.";
                valide = false;
            }

            if (string.IsNullOrWhiteSpace(TbxPrenom.Text))
            {
                tblerreurPrenom.Text = "Le prénom est obligatoire.";
                valide = false;
            }

            DateTime dateNaissance;
            if (dpDateNaissance.Date == null)
            {
                tblerreurDateNaissance.Text = "La date de naissance est obligatoire.";
            }
            else
            {
                dateNaissance = dpDateNaissance.Date.Value.DateTime;

                int age = DateTime.Today.Year - dateNaissance.Year;
                if (dateNaissance > DateTime.Today.AddYears(-age)) age--;

                if (age < 18)
                {
                    tblerreurDateNaissance.Text = "L'employé doit avoir au moins 18 ans.";
                    valide = false;
                }
                else if (age > 65)
                {
                    tblerreurDateNaissance.Text = "L'employé ne peut pas avoir plus de 65 ans.";
                    valide = false;
                }

                
            }

            if (string.IsNullOrWhiteSpace(TbxEmail.Text) || !TbxEmail.Text.Contains("@"))
            {
                tblerreurEmail.Text = "Entrez un courriel valide";
                valide = false;
            }

            if (string.IsNullOrWhiteSpace(TbxAdresse.Text))
            {
                tblerreurAdresse.Text = "L'adresse est obligatoire.";
                valide = false;
            }

            DateTime dateEmbauche;
            if (dpDateEmbauche.Date == null)
            {
                tblerreurDateEmbauche.Text = "La date d'embauche est obligatoire.";
                valide = false;
            }
            else 
            {
                dateEmbauche = dpDateEmbauche.Date.Value.DateTime;
                if (dateEmbauche > DateTime.Today)
                {
                    tblerreurDateEmbauche.Text = "La date d'embauche ne peut pas être dans le futur";
                    valide = false;
                }
                
            }

            double tauxHoraire = 0;
            

            if (string.IsNullOrWhiteSpace(TbxTauxHoraire.Text))
            {
                tblerreurTauxHoraire.Text = "Le taux horaire est obligatoire.";
                valide = false;
            }

            else if (!double.TryParse(TbxTauxHoraire.Text.Trim(), out tauxHoraire))
            {
                tblerreurTauxHoraire.Text = "Le taux horaire doit être un nombre valide.";
                valide = false;
            }
            else if (tauxHoraire < 15.00)
            {

                tblerreurTauxHoraire.Text = "Le taux horaire doit être au moins 15$/heure";
                valide = false;

            }
            else if (tauxHoraire > 200.00)
            {
                tblerreurTauxHoraire.Text = "Le taux horaire ne peut pas dépasser 200$/heure";
                valide = false;
            }



            if (string.IsNullOrWhiteSpace(TbxUrl.Text) || !Uri.IsWellFormedUriString(TbxUrl.Text.Trim(), UriKind.Absolute))
            {
                tblerreurUrl.Text = "Entrer un url valide pour la photo d'identité.";
                valide = false;
            }

            if (cmbxStatut.SelectedIndex == -1)
            {
                tblerreurStatut.Text = "Le statut est obligatoire.";
                valide = false;
            }


            if (valide)
            {
                string nom = TbxNom.Text.Trim();
                string prenom = TbxPrenom.Text.Trim();
                dateNaissance = dpDateNaissance.Date.Value.DateTime;
                string email = TbxEmail.Text.Trim();
                string adresse = TbxAdresse.Text.Trim();
                dateEmbauche = dpDateEmbauche.Date.Value.DateTime;
                string photo = TbxUrl.Text.Trim();

                StatutEmploye statut;

                if (cmbxStatut.SelectedItem.ToString() == "Journalier")
                {
                    statut = StatutEmploye.Journalier;
                }
                else
                {
                    statut = StatutEmploye.Permanent;
                }

                string resultat = SingletonEmploye.getInstance().ajouterEmploye(nom, prenom, dateNaissance, email, adresse, dateEmbauche, tauxHoraire, photo, statut);

                if(resultat == "")
                {

                    ContentDialog dialog = new ContentDialog()
                    {
                        Title = "Succès",
                        Content = "L'employé a été ajouté avec succès.",
                        CloseButtonText = "OK",
                        XamlRoot = this.XamlRoot
                    };
                }
                else
                {
                    ContentDialog dialog = new ContentDialog()
                    {
                        Title = "Erreur",
                        Content = resultat,
                        CloseButtonText = "OK",
                        XamlRoot = this.XamlRoot
                    };
                    await dialog.ShowAsync();
                    TbxNom.Text = "";
                    TbxPrenom.Text = "";
                    dpDateNaissance.Date = null;
                    TbxEmail.Text = "";
                    TbxAdresse.Text = "";
                    dpDateEmbauche.Date = null;
                    TbxTauxHoraire.Text = "";
                    TbxUrl.Text = "";
                    cmbxStatut.SelectedIndex = -1;
                }
            }

        }
    }
}
