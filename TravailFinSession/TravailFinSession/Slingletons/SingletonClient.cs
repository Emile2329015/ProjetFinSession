using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravailFinSession.Classes;
using TravailFinSession.Utils;


namespace TravailFinSession.Slingletons
{
    internal class SingletonClient
    {
        string connectionString;
        ObservableCollection<Client> listeClients;
        static SingletonClient instance = null;

        private SingletonClient() 
        {
            connectionString = Utilitaires.connectionString;
            listeClients= new ObservableCollection<Client>();
        }

        public static SingletonClient getInstance()
        {
            if (instance == null)
            {
                instance = new SingletonClient();
            }
               
            return instance;
        }

        //Propriété qui retourne la liste des clients 
        public ObservableCollection<Client> Liste { get => listeClients; }

        private int GenererIdentifiantunique()
        {
            int id;
            Random rnd = new Random();
            bool existe = true;

            try
            {
                using MySqlConnection con = new MySqlConnection(connectionString);
                using MySqlCommand commande = new MySqlCommand();
                commande.Connection = con;
                con.Open();

                do
                {
                    id = rnd.Next(100, 1000);
                    commande.CommandText = "select COUNT(*) from client WHERE id = @id";
                    commande.Parameters.Clear();
                    commande.Parameters.AddWithValue("@id", id);
                    int count = Convert.ToInt32(commande.ExecuteScalar());
                    existe = (count > 0);
                }
                while (existe);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return -1;
            }
            return id;
        }

        public void getAllClients() //charge la liste avec tous les clients 
        {
            listeClients.Clear(); //permet de vider la liste avant de la recharger 
            try
            {
                using MySqlConnection con = new MySqlConnection(connectionString);
                using MySqlCommand commande = con.CreateCommand();
                commande.CommandText = "Select * from client";
                con.Open();
                using MySqlDataReader r = commande.ExecuteReader();
                while (r.Read())
                {
                    int id = r.GetInt32("id");
                    string nom = r.GetString("nom");
                    string adresse = r.GetString("adresse");
                    string telephone = r.GetString("telephone");
                    string email = r.GetString("email");
                    Client client = new Client(id,nom,adresse,telephone,email);
                    listeClients.Add(client);
                }
            }
            catch (MySqlException ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }


        // ajouter un client 
        public void ajouterClient(string nom, string adresse, string telephone,string email)
        {
            try
            {
                int nouvelId = GenererIdentifiantunique();
                if(nouvelId == 1)
                {
                    Debug.WriteLine("Erreur de génération de l'id du client");
                    return;
                }
                using MySqlConnection con = new MySqlConnection(connectionString);
                using MySqlCommand commande = new MySqlCommand();
                commande.Connection = con;
                commande.CommandText = "insert into client(id,nom,adresse,numero_telephone,email) values(@id,@nom, @adresse, @telephone,@email) ";
                commande.Parameters.AddWithValue("@id", nouvelId);
                commande.Parameters.AddWithValue("@nom", nom);
                commande.Parameters.AddWithValue("@adresse", adresse);
                commande.Parameters.AddWithValue("@telephone", telephone);
                commande.Parameters.AddWithValue("@email", email);

                con.Open();
                int i = commande.ExecuteNonQuery();


                getAllClients();    //permet de recharger la liste des clients après un ajout 
            }
            catch (MySqlException ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }


        // modifier un client 
        public void modifierClient(int id, string nom,string adresse,string telephone,string email)
        {
            try
            {
                using MySqlConnection con = new MySqlConnection(connectionString);
                using MySqlCommand commande = new MySqlCommand();
                commande.Connection = con;
                commande.CommandText = "update client set nom = @nom,adresse = @adresse,telephone = @telephone,email = @email WHERE id = @id";
                commande.Parameters.AddWithValue("@id", id);
                commande.Parameters.AddWithValue("@nom", nom);
                commande.Parameters.AddWithValue("@adresse", adresse);
                commande.Parameters.AddWithValue("@telephone", telephone);
                commande.Parameters.AddWithValue("@email", email);
                con.Open();
                int i = commande.ExecuteNonQuery();

                getAllClients();     
            }
            catch (MySqlException ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }


        // Rechercher un client par id

        
        public void rechercheParId(int idClient )
        {
            listeClients.Clear(); //permet de vider la liste avant de la recharger 
            try
            {
                using MySqlConnection con = new MySqlConnection(connectionString);
                using MySqlCommand commande = new MySqlCommand();
                commande.Connection = con;
                commande.CommandText = "Select * from client where id = @idClient";
                commande.Parameters.AddWithValue("@idClient", idClient);
                con.Open();
                using MySqlDataReader r = commande.ExecuteReader();
                while (r.Read())
                {
                    int id = r.GetInt32("id");
                    string nom = r.GetString("nom");
                    string adresse = r.GetString("adresse");
                    string telephone = r.GetString("telephone");
                    string email = r.GetString("email");

                    Client client = new Client(id, nom, adresse, telephone, email);

                    listeClients.Add(client);
                }
            }
            catch (MySqlException ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }


    }
}
