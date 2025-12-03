using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravailFinSession.Classes
{
    internal class ProjetDetails
    {
       public string Numero {  get; set; }
       public string Titre {  get; set; }
       public DateTime DateDebut {  get; set; }
       public string Description  { get; set; }
       public double Budget {  get; set; }
       public int NbreEmployes {  get; set; }
       public double TotalSalaire { get; set; }
       public int ClientId {  get; set; }
       public string Statut {  get; set; }


        public List<EmployeProjet> Employes { get; set; } = new List<EmployeProjet>();





    }
}
