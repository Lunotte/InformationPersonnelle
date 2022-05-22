using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InformationPersonnelle.Shared.Dtos
{
    public class AnniversaireToAddDto
    {
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public DateOnly DateNaissance { get; set; }
    }
}
