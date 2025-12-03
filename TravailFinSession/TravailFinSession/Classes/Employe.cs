using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravailFinSession.Classes
{
    public enum StatutEmploye
    {
        Journalier,
        Permanent
    };
    internal class Employe
    {
        string matricule;
        string nom;
        string prenom;
        DateTime dateNaissance;
        string email;
        string adresse;
        DateTime dateEmbauche;
        double tauxHoraire;
        string photo;
        StatutEmploye statutEmploye;

       
        

        public Employe(string matricule, string nom, string prenom, DateTime dateNaissance, string email, string adresse, DateTime dateEmbauche, double tauxHoraire, string photo,StatutEmploye statutEmploye)
        {
            this.matricule = matricule;
            this.nom = nom;
            this.prenom = prenom;
            this.dateNaissance = dateNaissance;
            this.email = email;
            this.adresse = adresse;
            this.dateEmbauche = dateEmbauche;
            this.tauxHoraire = tauxHoraire;
            this.photo = photo;
            this.statutEmploye = statutEmploye;
            
        }

        public string Matricule { get => matricule; set => matricule = value; }
        public string Nom { get => nom; set => nom = value; }
        public string NomComplet { get => $"{prenom} {nom}"; }
        public string Prenom { get => prenom; set => prenom = value; }
        public DateTime DateNaissance { get => dateNaissance; set => dateNaissance = value; }
        public string Email { get => email; set => email = value; }
        public string Adresse { get => adresse; set => adresse = value; }
        public DateTime DateEmbauche { get => dateEmbauche; set => dateEmbauche = value; }
        public double TauxHoraire { get => tauxHoraire; set => tauxHoraire = value; }
        public string Photo { get => photo; set => photo = value; }
        public  StatutEmploye  StatutEmploye{ get => statutEmploye; set => statutEmploye = value; }

        public override string ToString()
        {
            return $"Matricule : {matricule}\n" +
                   $"Nom : {nom}\n" +
                   $"Prénom : {prenom}\n" +
                   $"Date de naissance : {dateNaissance}\n" +
                   $"Email : {email}\n" +
                   $"Adresse : {adresse}\n" +
                   $"Date d’embauche : {dateEmbauche}\n" +
                   $"Taux horaire : {tauxHoraire}$\n" +
                   $"Photo : {photo}\n" +
                   $"Statut : {statutEmploye}";
                 
        }


    }

}