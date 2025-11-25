using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravailFinSession.Classes
{
    internal class Projet
    {
        string numeroProjet;
        string titre;
        string dateDebut;
        string description;
        string nbEmployes;
        string totalSalaireAPayer;
        string idClient;
        string statut;

        public Projet(string numeroProjet, string titre, string dateDebut, string description, string nbEmployes, string totalSalaireAPayer, string idClient, string statut)
        {
            this.numeroProjet = numeroProjet;
            this.titre = titre;
            this.dateDebut = dateDebut;
            this.description = description;
            this.nbEmployes = nbEmployes;
            this.totalSalaireAPayer = totalSalaireAPayer;
            this.idClient = idClient;
            this.statut = statut;
        }

        public string NumeroProjet { get => numeroProjet; set => numeroProjet = value; }
        public string Titre { get => titre; set => titre = value; }
        public string DateDebut { get => dateDebut; set => dateDebut = value; }
        public string Description { get => description; set => description = value; }
        public string NbEmployes { get => nbEmployes; set => nbEmployes = value; }
        public string TotalSalaireAPayer { get => totalSalaireAPayer; set => totalSalaireAPayer = value; }
        public string IdClient { get => idClient; set => idClient = value; }
        public string Statut { get => statut; set => statut = value; }

        public override string ToString()
        {
            return $"Numéro du projet : {numeroProjet}\n" +
                   $"Titre : {titre}\n" +
                   $"Date de début : {dateDebut}\n" +
                   $"Description : {description}\n" +
                   $"Nombre d’employés : {nbEmployes}\n" +
                   $"Total du salaire à payer : {totalSalaireAPayer}$\n" +
                   $"ID du client : {idClient}\n" +
                   $"Statut : {statut}";
        }
    }
}
