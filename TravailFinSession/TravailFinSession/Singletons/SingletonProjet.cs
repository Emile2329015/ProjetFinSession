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
    internal class SingletonProjet
    {
        string connectionString;
        ObservableCollection<Projet> listeProjets;
        static SingletonProjet instance = null;

        private SingletonProjet()
        {
            connectionString = Utilitaires.connectionString;
            listeProjets = new ObservableCollection<Projet>();
        }

        public static SingletonProjet getInstance()
        {
            if (instance == null)

                instance = new SingletonProjet();
            return instance;
        }

        //Propriété qui retourne la liste des employés 
        public ObservableCollection<Projet> AfficherProjets() { return listeProjets; }

        public void getAllProjets() //charge la liste avec tous les employés
        {
            listeProjets.Clear(); //permet de vider la liste avant de la recharger 
            try
            {
                using MySqlConnection con = new MySqlConnection(connectionString);
                using MySqlCommand commande = con.CreateCommand();
                commande.CommandText = "Select * from projet";
                con.Open();
                using MySqlDataReader r = commande.ExecuteReader();
                while (r.Read())
                {
                    string numero = r.GetString("numero");
                    string titre = r.GetString("titre");
                    DateTime dateDebut = r.GetDateTime("date_debut");
                    string description = r.GetString("description");
                    double budget = (double)r.GetDecimal("budget");
                    int nbreEmployes = r.GetInt32("nbre_employes");
                    double totalSalaire = (double)r.GetDecimal("total_salaire_a_payer");
                    int clientId = r.GetInt32("client");
                    string statutProjet = r.GetString("statut");
                    statutProjet = statutProjet.Replace("_", "");
                    StatutProjet statut;
                    if (!Enum.TryParse(statutProjet, true, out statut))
                    {
                        statut = StatutProjet.Terminé;
                    }
                    Projet projet = new Projet(numero,titre,dateDebut,description,budget,nbreEmployes,totalSalaire,clientId,statut);
                    listeProjets.Add(projet);
                }
            }
            catch (MySqlException ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }


        public void AjouterProjet(string titre, DateTime dateDebut, string description, double budget, int nbreEmployes, int clientId, StatutProjet statut)
        {
            try
            {
                

                using MySqlConnection con = new MySqlConnection(connectionString);
                using MySqlCommand commande = new MySqlCommand();
                commande.Connection = con;
                commande.CommandText = "insert into projet(titre,date_debut,description,budget,nbre_employes,total_salaire_a_payer,client,statut) values(@titre,@dateDebut,@description,@budget,@nbreEmployes,0,@client,@statut)";
                commande.Parameters.AddWithValue("@titre", titre);
                commande.Parameters.AddWithValue("@dateDebut", dateDebut);
                commande.Parameters.AddWithValue("@description", description);
                commande.Parameters.AddWithValue("@budget", budget);
                commande.Parameters.AddWithValue("@nbreEmployes", nbreEmployes);
                commande.Parameters.AddWithValue("@client", clientId );
                
                commande.Parameters.AddWithValue("@statut", statut.ToString());

                con.Open();
                int i = commande.ExecuteNonQuery();


                getAllProjets();    //permet de recharger la liste des employés après un ajout 
            }
            catch (MySqlException ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        public void modifierProjet(string numero, string titre, string description, double budget, int nbreEmployes, int clientId, StatutProjet statut)
        {
            try
            {
               
                using MySqlConnection con = new MySqlConnection(connectionString);
                using MySqlCommand commande = new MySqlCommand();
                commande.Connection = con;
                commande.CommandText = "update projet set titre = @titre,description = @description, budget = @budget,nbre_employes = @nbreEmployes,client = @client,statut = @statut WHERE numero = @numero";
                commande.Parameters.AddWithValue("@titre", titre);
                commande.Parameters.AddWithValue("@description", description);
                commande.Parameters.AddWithValue("@budget", budget);
                commande.Parameters.AddWithValue("@nbreEmployes", nbreEmployes);
                commande.Parameters.AddWithValue("@client", clientId);
                commande.Parameters.AddWithValue("@statut", statut.ToString());


                con.Open();
                int i = commande.ExecuteNonQuery();

                getAllProjets();
            }
            catch (MySqlException ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        public void AssignerEmploye(string numeroProjet,string matricule,double heures)
        {
            try
            {
                if (!SingletonEmploye.getInstance().EstDisponible(matricule))
                    throw new Exception("Cet employé est déjà assigné à un projet en cours.");
                Employe employe = SingletonEmploye.getInstance().AfficherEmployes().FirstOrDefault(emp => emp.Matricule == matricule);

                if (employe == null)
                    throw new Exception("Cet employé n'existe pas");

                double salaire = employe.TauxHoraire * heures;
                using MySqlConnection con = new MySqlConnection(connectionString);
                con.Open();

                using MySqlCommand commande = new MySqlCommand();
                commande.CommandText = "insert into projetemploye(numero_projet,matricule_employe,heures,salaire) values(@numero,@matricule,@heures,@salaire)";
                commande.Parameters.AddWithValue("@numero", numeroProjet);
                commande.Parameters.AddWithValue("@matricule", matricule);
                commande.Parameters.AddWithValue("@heures", heures);
                commande.Parameters.AddWithValue("@salaire", salaire);
                commande.ExecuteNonQuery();

                using MySqlCommand update = new MySqlCommand();
                update.CommandText = "update projet set total_salaire_a_payer = total_salaire_a_payer + @salaire WHERE numero = @numero";
                update.Parameters.AddWithValue("@salaire", salaire);
                update.Parameters.AddWithValue("@numero", numeroProjet);
                update.ExecuteNonQuery();
                getAllProjets();
            }
            catch (MySqlException ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        public ProjetDetails afficherDetailsprojets(string numeroProjet)
        {
            ProjetDetails details = new ProjetDetails();
            try
            {
                using MySqlConnection con = new MySqlConnection(connectionString);
                con.Open();

                using MySqlCommand commande = new MySqlCommand();
                commande.CommandText = "select * from projet WHERE numero=@numero";
                commande.Parameters.AddWithValue("@numero", numeroProjet);
                using MySqlDataReader r = commande.ExecuteReader();
                if (r.Read())
                {
                    details.Numero = r.GetString("numero");
                    details.Titre = r.GetString("titre");
                    details.DateDebut = r.GetDateTime("date_debut");
                    details.Description = r.GetString("description");
                    details.Budget = (double)r.GetDecimal("budget");
                    details.NbreEmployes = r.GetInt32("nbre_employes");
                    details.TotalSalaire = (double)r.GetDecimal("total_salaire_a_payer");
                    details.ClientId = r.GetInt32("client");
                    details.Statut = r.GetString("statut");
                }
                r.Close();
                details.Employes = new List<EmployeProjet>();
                using MySqlCommand cmd = new MySqlCommand();
                cmd.CommandText = "select e.matricule,e.nom,e.prenom,pe.heures,pe.salaire from projetemploye pe JOIN employe ON pe.matricule_employe = e.matricule WHERE pe.numero_projet=@numero";
                cmd.Parameters.AddWithValue("@numero",numeroProjet);

                using MySqlDataReader r2 = cmd.ExecuteReader();
                while (r2.Read())
                {
                    details.Employes.Add(new EmployeProjet
                    {
                        Matricule = r2.GetString("matricule"),
                        Nom = r2.GetString("nom"),
                        Prenom = r2.GetString("prenom"),
                        Heures = r2.GetDouble("heures"),
                        Salaire = r2.GetDouble("salaire")
                    });
                }
            }
            catch (MySqlException ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return details;
        }


        public void terminerProjet(string numeroProjet)
        {
            try
            {
                using MySqlConnection con = new MySqlConnection(connectionString);
                using MySqlCommand commande = new MySqlCommand();
                commande.Connection = con;
                commande.CommandText = "update projet set statut='Terminé' WHERE numero = @numero";
                commande.Parameters.AddWithValue("@numero", numeroProjet);
                con.Open();
                int i = commande.ExecuteNonQuery();

                getAllProjets();
            }
            catch (MySqlException ex)
            { 
                Debug.WriteLine(ex.Message); 
            }
             
        }
    }
}
