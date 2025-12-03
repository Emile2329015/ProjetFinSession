using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravailFinSession.Classes
{
    public enum StatutProjet
    {
        En_cours,
        Terminé
    };
    internal class Projet
    {
        string numeroProjet;
        string titre;
        DateTime dateDebut;
        string description;
        double budget;
        int nbEmployes;
        double totalSalaireAPayer;
        int idClient;
        StatutProjet statutProjet;

        public Projet(string numeroProjet, string titre, DateTime dateDebut, string description,double budget, int nbEmployes, double totalSalaireAPayer, int idClient, StatutProjet statutProjet)
        {
            this.numeroProjet = numeroProjet;
            this.titre = titre;
            this.dateDebut = dateDebut;
            this.description = description;
            this.budget = budget;
            this.nbEmployes = nbEmployes;
            this.totalSalaireAPayer = totalSalaireAPayer;
            this.idClient = idClient;
            this.statutProjet = statutProjet;
        }

        public string NumeroProjet { get => numeroProjet; set => numeroProjet = value; }
        public string Titre { get => titre; set => titre = value; }
        public DateTime DateDebut { get => dateDebut; set => dateDebut = value; }
        public string Description { get => description; set => description = value; }
        public double Budget { get => budget; set => budget = value; }
        public int NbEmployes { get => nbEmployes; set => nbEmployes = value; }
        public double TotalSalaireAPayer { get => totalSalaireAPayer; set => totalSalaireAPayer = value; }
        public int IdClient { get => idClient; set => idClient = value; }
        public StatutProjet StatutProjet { get => statutProjet; set => statutProjet = value; }

        public override string ToString()
        {
            return $"Numéro du projet : {numeroProjet}\n" +
                   $"Titre : {titre}\n" +
                   $"Date de début : {dateDebut}\n" +
                   $"Description : {description}\n" +
                   $"Budget : {budget}\n" +
                   $"Nombre d’employés : {nbEmployes}\n" +
                   $"Total du salaire à payer : {totalSalaireAPayer}$\n" +
                   $"ID du client : {idClient}\n" +
                   $"Statut : {statutProjet}";
        }
    }
}
