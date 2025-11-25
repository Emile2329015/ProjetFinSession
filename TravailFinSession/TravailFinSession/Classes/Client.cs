using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravailFinSession.Classes
{
    internal class Client
    {
        int identifiant;
        string nom;
        string adresse;
        string telephone;
        string email;

        public Client(int identifiant, string nom, string adresse, string telephone, string email)
        {
            this.identifiant = identifiant;
            this.nom = nom;
            this.adresse = adresse;
            this.telephone = telephone;
            this.email = email;
        }

        public int Identifiant { get => identifiant; set => identifiant = value; }
        public string Nom { get => nom; set => nom = value; }
        public string Adresse { get => adresse; set => adresse = value; }
        public string Telephone { get => telephone; set => telephone = value; }
        public string Email { get => email; set => email = value; }

        public override string ToString()
        {
            return $"Identifiant : {identifiant}\n" +
                   $"Nom : {nom}\n" +
                   $"Adresse : {adresse}\n" +
                   $"Téléphone : {telephone}\n" +
                   $"Email : {email}";
        }
    }
}
