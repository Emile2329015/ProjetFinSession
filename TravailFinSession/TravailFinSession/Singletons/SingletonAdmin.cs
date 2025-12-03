using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace TravailFinSession.Slingletons
{
    internal class SingletonAdmin
    {
        private static SingletonAdmin instance = null; 
        private string nomUtilisateur;
        private string motDePasseHash;
        private bool estConnecte;

        private SingletonAdmin() { estConnecte = false; }

        public static SingletonAdmin getInstance()
        {
            if (instance == null)

                instance = new SingletonAdmin();
            return instance;
        }
        public void CreerCompte(string utilisateur,string motDePasse)
        {
            if (!string.IsNullOrEmpty(nomUtilisateur))
                throw new InvalidOperationException("Un compte existe déjà.");

            nomUtilisateur = utilisateur;
            motDePasseHash = HashPassword(motDePasse);
            estConnecte = false;
        }

        public bool Connexion(string utilisateur,string motDePasse)
        {
            if (nomUtilisateur == null)
                throw new InvalidOperationException("Aucun compte n'a été créé.");
            if (utilisateur == nomUtilisateur && VerifierMotDePasse(motDePasse, motDePasseHash)) ;

            {
                estConnecte=true;
                return true;
            }
            return false;
        }
        
        public void Deconnection()
        {
            estConnecte = false;
        }

        private string HashPassword(string motDePasse)
        {
            using SHA256 sha = SHA256.Create();
            byte[] bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(motDePasse));
            return Convert.ToBase64String(bytes); 
        }
        private bool VerifierMotDePasse(string motDePasse,string hash)
        {
            string hashInput = HashPassword(motDePasse);
            return hashInput == hash;
        }
    }
}
