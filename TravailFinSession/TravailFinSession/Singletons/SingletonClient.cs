using MySqlConnector;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using TravailFinSession.Classes;
using TravailFinSession.Utils;

namespace TravailFinSession.Singletons
{
    internal class SingletonClient
    {
        string connectionString;
        ObservableCollection<Client> listeClients;
        static SingletonClient instance = null;

        private SingletonClient()
        {
            connectionString = Utilitaires.connectionString;
            listeClients = new ObservableCollection<Client>();
        }

        public static SingletonClient getInstance()
        {
            if (instance == null)
            {
                instance = new SingletonClient();
            }
            return instance;
        }

        public ObservableCollection<Client> Liste { get => listeClients; }

        public void getAllClients()
        {
            listeClients.Clear();
            try
            {
                using MySqlConnection con = new MySqlConnection(connectionString);
                using MySqlCommand commande = con.CreateCommand();
                commande.CommandText = "SELECT * FROM client";
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
            catch (Exception ex)
            {
                Debug.WriteLine($"ERREUR getAllClients: {ex.Message}");
            }
        }

        public void ajouterClient(string nom, string adresse, string telephone, string email)
        {
            try
            {
                int nouvelId = GenererIdentifiantunique();
                if (nouvelId == -1)
                {
                    Debug.WriteLine("Erreur de génération de l'id du client");
                    return;
                }

                using MySqlConnection con = new MySqlConnection(connectionString);
                using MySqlCommand commande = new MySqlCommand();
                commande.Connection = con;
                commande.CommandText = "INSERT INTO client(id, nom, adresse, telephone, email) VALUES(@id, @nom, @adresse, @telephone, @email)";
                commande.Parameters.AddWithValue("@id", nouvelId);
                commande.Parameters.AddWithValue("@nom", nom);
                commande.Parameters.AddWithValue("@adresse", adresse);
                commande.Parameters.AddWithValue("@telephone", telephone);
                commande.Parameters.AddWithValue("@email", email);

                con.Open();
                commande.ExecuteNonQuery();

                getAllClients();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"ERREUR ajouterClient: {ex.Message}");
            }
        }

        public void modifierClient(int id, string nom, string adresse, string telephone, string email)
        {
            try
            {
                using MySqlConnection con = new MySqlConnection(connectionString);
                using MySqlCommand commande = new MySqlCommand();
                commande.Connection = con;
                commande.CommandText = "UPDATE client SET nom = @nom, adresse = @adresse, telephone = @telephone, email = @email WHERE id = @id";
                commande.Parameters.AddWithValue("@id", id);
                commande.Parameters.AddWithValue("@nom", nom);
                commande.Parameters.AddWithValue("@adresse", adresse);
                commande.Parameters.AddWithValue("@telephone", telephone);
                commande.Parameters.AddWithValue("@email", email);

                con.Open();
                commande.ExecuteNonQuery();

                getAllClients();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"ERREUR modifierClient: {ex.Message}");
            }
        }

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
                    commande.CommandText = "SELECT COUNT(*) FROM client WHERE id = @id";
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

       
    }
}
