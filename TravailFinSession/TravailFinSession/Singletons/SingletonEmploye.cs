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

        //Propriété qui retourne la liste des employés 
        public ObservableCollection<Employe> AfficherEmployes(){ return listeEmployes; }



        public void getAllEmployes() //charge la liste avec tous les employés
        {
            listeEmployes.Clear(); //permet de vider la liste avant de la recharger 
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
        public void ajouterEmploye(string nom,string prenom,DateTime dateNaissance, string email, string adresse,DateTime dateEmbauche,double tauxHoraire, string photo,StatutEmploye statut)
        {
            try
            {
                if (tauxHoraire < 15.00)
                    throw new ArgumentException("Le taux horaire doit être de 15$ au moins.");
                
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


                getAllEmployes();    //permet de recharger la liste des employés après un ajout 
            }
            catch (MySqlException ex)
            {
                Debug.WriteLine(ex.Message);
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
        public void modifierEmploye(string matricule, string nom, string prenom,string email,string adresse,double tauxHoraire,string photo,StatutEmploye statut)
        {
            try
            {
                if (tauxHoraire < 15.00)
                    throw new ArgumentException("Le taux horaire doit être de 15$ au moins.");
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
            }
            catch (MySqlException ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        //// supprimer un employé
        //public void supprimer(string matricule)
        //{
        //    try
        //    {
        //        using MySqlConnection con = new MySqlConnection(connectionString);
        //        using MySqlCommand commande = new MySqlCommand();
        //        commande.Connection = con;
        //        commande.CommandText = "delete from employe where matricule = @matricule";
        //        commande.Parameters.AddWithValue("@matricule", matricule);
        //        con.Open();
        //        int i = commande.ExecuteNonQuery();


        //        getAllEmployes();    //permet de recharger la liste des employés après un ajout 
        //    }
        //    catch (MySqlException ex)
        //    {
        //        Debug.WriteLine(ex.Message);
        //    }
        //}


        // Rechercher un employe par matricule


        public void rechercheParMatricule(string matricule)
        {
            listeEmployes.Clear(); //permet de vider la liste avant de la recharger 
            try
            {
                using MySqlConnection con = new MySqlConnection(connectionString);
                using MySqlCommand commande = new MySqlCommand();
                commande.Connection = con;
                commande.CommandText = "Select * from employe where matricule = @matricule";
                commande.Parameters.AddWithValue("@matricule", matricule);
                con.Open();
                using MySqlDataReader r = commande.ExecuteReader();
                while (r.Read())
                {
               
                    string nom = r.GetString("nom");
                    string prenom = r.GetString("prenom");
                    DateTime dateNaissance = r.GetDateTime("date_naissance");
                    string email = r.GetString("email");
                    string adresse = r.GetString("adresse");
                    DateTime dateEmbauche = r.GetDateTime("date_embauche");
                    double tauxHoraire = r.GetDouble("taux_horaire");
                    string photo = r.GetString("photo_identite");
                    string statutEmp = r.GetString("statut");
                    Enum.TryParse(statutEmp, true, out StatutEmploye statut);
                    

                    Employe employe = new Employe(matricule,nom,prenom,dateNaissance,email, adresse,dateEmbauche,tauxHoraire,photo,statut);

                    listeEmployes.Add(employe);
                }
            }
            catch (MySqlException ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }


    }

}

