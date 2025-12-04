using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravailFinSession.Classes;
using TravailFinSession.Utils;

namespace TravailFinSession.Slingletons
{
    internal class SingletonEmploye
    {
        string connectionString;
        ObservableCollection<Employe> listeEmployes;
        static SingletonEmploye instance = null;

        private SingletonEmploye()
        {
            connectionString = Utilitaires.connectionString;
            listeEmployes = new ObservableCollection<Employe>();
        }

        public static SingletonEmploye getInstance()
        {
            if (instance == null)
                instance = new SingletonEmploye();

            return instance;


        }

        
        public ObservableCollection<Employe> AfficherEmployes(){ return listeEmployes; }



        public void getAllEmployes() 
        {
            listeEmployes.Clear(); 
            try
            {
                using MySqlConnection con = new MySqlConnection(connectionString);
                using MySqlCommand commande = con.CreateCommand();
                commande.CommandText = "Select * from employe";
                con.Open();
                using MySqlDataReader r = commande.ExecuteReader();
                while (r.Read())
                {
                    string matricule = r.GetString("matricule");
                    string nom = r.GetString("nom");
                    string prenom = r.GetString("prenom");
                    DateTime dateNaissance = r.GetDateTime("date_naissance");
                    string email = r.GetString("email");
                    string adresse = r.GetString("adresse");
                    DateTime dateEmbauche = r.GetDateTime("date_embauche");
                    double tauxHoraire = (double)r.GetDecimal("taux_horaire");
                    string photo = r.GetString("photo_identite");
                    string statutEmp = r.GetString("statut");
                    StatutEmploye statut;
                    if (!Enum.TryParse(statutEmp, true, out statut))
                    {
                        statut = StatutEmploye.Journalier;
                    }
                    Employe employe = new Employe(matricule,nom,prenom,dateNaissance,email,adresse,dateEmbauche,tauxHoraire,photo,statut);
                    listeEmployes.Add(employe);
                }
            }
            catch (MySqlException ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }


        // ajouter un employé
        public string ajouterEmploye(string nom,string prenom,DateTime dateNaissance, string email, string adresse,DateTime dateEmbauche,double tauxHoraire, string photo,StatutEmploye statut)
        {
            try
            {
                if (tauxHoraire < 15.00)
                    return "Letaux horaire doit être d'au moins 15$/h";

                if(tauxHoraire > 200.00)
                    return "Le taux horaire ne peut pas dépasser 200$/h";
             
            
                int age = DateTime.Today.Year - dateNaissance.Year;
                if (dateNaissance > DateTime.Today.AddYears(-age)) age--;

                if (age < 18)
                    return "L'employé doit avoir au moins 18 ans.";

                if(age > 65)
                    return "L'employé ne peut pas avoir plus de 65 ans.";
                
                if(dateEmbauche > DateTime.Today)
                    return "La date d'embauche ne peut pas être dans le futur.";



                using MySqlConnection con = new MySqlConnection(connectionString);
                using MySqlCommand commande = new MySqlCommand();
                commande.Connection = con;
                commande.CommandText = "insert into employe(nom,prenom,date_naissance,email,adresse,date_embauche,taux_horaire,photo_identite,statut) values(@nom,@prenom,@dateNaissance,@email, @adresse, @dateEmbauche,@tauxHoraire,@photo,@statut)";
                commande.Parameters.AddWithValue("@nom", nom);
                commande.Parameters.AddWithValue("@prenom", prenom);
                commande.Parameters.AddWithValue("@dateNaissance", dateNaissance);
                commande.Parameters.AddWithValue("@email", email);
                commande.Parameters.AddWithValue("@adresse", adresse);
                commande.Parameters.AddWithValue("@dateEmbauche", dateEmbauche);
                commande.Parameters.AddWithValue("@tauxHoraire", tauxHoraire);
                commande.Parameters.AddWithValue("@photo", photo);
                commande.Parameters.AddWithValue("@statut", statut.ToString());

                con.Open();
                int i = commande.ExecuteNonQuery();


                getAllEmployes(); 
                return "Employé ajouté avec succès.";
            }
            catch (MySqlException ex)
            {
                Debug.WriteLine(ex.Message);

                if(ex.Message.Contains("Duplicate entry") && ex.Message.Contains("email"))
                {
                    return "L'adresse email existe déjà pour un autre employé.";
                }

                return "Une erreur est survenue lors de l'ajout de l'employé." + ex.Message;
            }
        }

        public bool EstDisponible(string matricule)
        {
            try
            {
                using MySqlConnection con = new MySqlConnection(connectionString);
                using MySqlCommand commande = new MySqlCommand();
                commande.Connection = con;
                commande.CommandText = "Select COUNT(*) From projet p JOIN projetemploye pe ON p.numero = pe.numero_projet WHERE pe.matricule_employe = @matricule AND p.statut = 'En cours'";
                commande.Parameters.AddWithValue("@matricule", matricule);
                con.Open();
                int count = Convert.ToInt32(commande.ExecuteScalar());

                return count == 0;
            }
            catch (MySqlException ex)
            {
                Debug.WriteLine(ex.Message);
                return false;
            }
        }

        // modifier un employe
        public string modifierEmploye(string matricule, string nom, string prenom,string email,string adresse,double tauxHoraire,string photo,StatutEmploye statut)
        {
            try
            {
                if (tauxHoraire < 15.00)
                    return"Le taux horaire doit être de 15$ au moins.";
                using MySqlConnection con = new MySqlConnection(connectionString);
                using MySqlCommand commande = new MySqlCommand();
                commande.Connection = con;
                commande.CommandText = "update employe set nom = @nom,prenom=@prenom,email = @email,adresse = @adresse,taux_horaire = @tauxHoraire,photo_identite = @photo,statut = @statut WHERE matricule = @matricule";
                commande.Parameters.AddWithValue("@matricule", matricule);
                commande.Parameters.AddWithValue("@nom", nom);
                commande.Parameters.AddWithValue("@prenom", prenom);
                commande.Parameters.AddWithValue("@email", email);
                commande.Parameters.AddWithValue("@adresse", adresse);
                commande.Parameters.AddWithValue("@tauxHoraire", tauxHoraire);
                commande.Parameters.AddWithValue("@photo", photo);
                commande.Parameters.AddWithValue("@statut", statut.ToString());


                con.Open();
                int i = commande.ExecuteNonQuery();

                getAllEmployes();

                return "Employé modifié avec succès.";
            }
            catch (MySqlException ex)
            {
                Debug.WriteLine(ex.Message);
                if (ex.Message.Contains("Duplicate entry") && ex.Message.Contains("email"))
                {
                    return "L'adresse email existe déjà pour un autre employé.";
                }

                return "Une erreur est survenue lors de la modification de l'employé." + ex.Message;
            }
        }

       


    }

}

