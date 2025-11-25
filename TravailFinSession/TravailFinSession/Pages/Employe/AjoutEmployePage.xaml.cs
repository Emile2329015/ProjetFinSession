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
    public sealed partial class AjoutEmployePage : Page
    {
       
        public AjoutEmployePage()
        {
            InitializeComponent();
        }

        private void Ajouter_Employe(object sender, RoutedEventArgs e)
        {
            bool valide = true;

            tblerreurMatricule.Text = "";
            tblerreurNom.Text = "";
            tblerreurPrenom.Text = "";
            tblerreurDateNaissance.Text = "";
            tblerreurEmail.Text = "";
            tblerreurAdresse.Text = "";
            tblerreurDateEmbauche.Text = "";
            tblerreurTauxHoraire.Text = "";
            tblerreurUrl.Text = "";
            tblerreurStatut.Text = "";


            if (TbxMatricule.Text.Trim() == "")
            {
                tblerreurMatricule.Text = "Vous devez entrer votre matricule";
                valide = false;
            }

            if (TbxNom.Text.Trim() == "")
            {
                tblerreurNom.Text = "Vous devez entrer votre nom";
                valide = false;
            }

            if (TbxPrenom.Text.Trim() == "")
            {
                tblerreurPrenom.Text = "Vous devez entrer votre prénom";
                valide = false;
            }

            DateTime dateNaissance;
            if (!DateTime.TryParse(TbxDateNaissance.Text.Trim(), out dateNaissance))
            {
                tblerreurDateNaissance.Text = "Entrez une date de naissance valide (jj/mm/aaaa)";
                valide = false;
            }
            else
            {
                int age = DateTime.Today.Year - dateNaissance.Year;
                if (dateNaissance > DateTime.Today.AddYears(-age)) age--;
                if (age < 18 || age > 65)
                {
                    tblerreurDateNaissance.Text = "L'employé doit avoir entre 18 et 65 ans";
                    valide = false;
                }
            }

            if (string.IsNullOrWhiteSpace(TbxEmail.Text) || !TbxEmail.Text.Contains("@"))
            {
                tblerreurEmail.Text = "Entrez un courriel valide";
                valide = false;
            }

            if (TbxAdresse.Text.Trim() == "")
            {
                tblerreurAdresse.Text = "Vous devez entrer votre adresse";
                valide = false;
            }

            DateTime dateEmbauche;
            if (!DateTime.TryParse(TbxDateEmbauche.Text.Trim(), out dateEmbauche))
            {
                tblerreurDateEmbauche.Text = "Entrez une date d'embauche valide";
                valide = false;
            }
            else if (dateEmbauche > DateTime.Today)
            {
                tblerreurDateEmbauche.Text = "La date d'embauche ne peut pas être dans le futur";
                valide = false;
            }

            if (TbxTauxHoraire.Text.Trim() == "")
            {
                tblerreurTauxHoraire.Text = "Vous devez entrer votre taux horaire";
                valide = false;
            }
            else
            {
                try
                {
                    double taux = Convert.ToDouble(TbxTauxHoraire.Text.Trim());
                    if (taux <= 15)
                    {
                        tblerreurTauxHoraire.Text = "Le taux horaire doit être au moins 15$/heure";
                        valide = false;
                    }
                }
                catch
                {
                    tblerreurTauxHoraire.Text = "Le taux horaire doit être un nombre valide";
                    valide = false;
                }
            }



            if (Uri.IsWellFormedUriString(TbxUrl.Text.Trim(), UriKind.Absolute) == false)
            {
                tblerreurUrl.Text = "Entrez l'url de votre photo d'identité";
                valide = false;
            }

            if (cmbxStatut.Text.Trim() == "")
            {
                tblerreurStatut.Text = "Vous devez entrer votre statut";
                valide = false;
            }

            //if (valide)
            //{
            //    string matricule = TbxMatricule.Text.Trim();
            //    string nom = TbxNom.Text.Trim();
            //    string prenom = TbxPrenom.Text.Trim();
            //    string email = TbxEmail.Text.Trim();
            //    string adresse = TbxAdresse.Text.Trim();
            //    double tauxHoraire = Convert.ToDouble(TbxTauxHoraire.Text.Trim());
            //    string photo = TbxUrl.Text.Trim();
            //    string statut = cmbxStatut.Text.Trim();
            //    Employe nouvelEmploye = new Employe(matricule, nom, prenom, dateNaissance, email, adresse, dateEmbauche, tauxHoraire, photo, statut);
            //    Donnees.DonneesEmployes.AjouterEmploye(nouvelEmploye);
            //    TbxMatricule.Text = "";
            //    TbxNom.Text = "";
            //    TbxPrenom.Text = "";
            //    TbxDateNaissance.Text = "";
            //    TbxEmail.Text = "";
            //    TbxAdresse.Text = "";
            //    TbxDateEmbauche.Text = "";
            //    TbxTauxHoraire.Text = "";
            //    TbxUrl.Text = "";
            //    cmbxStatut.SelectedIndex = -1;
            //    var dialog = new ContentDialog()
            //    {
            //        Title = "Succès",
            //        Content = "L'employé a été ajouté avec succès.",
            //        CloseButtonText = "OK"
            //    };
            //    _ = dialog.ShowAsync();
            //}



        }
    }
}
