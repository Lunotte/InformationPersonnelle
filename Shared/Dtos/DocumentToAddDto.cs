using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InformationPersonnelle.Shared.Dtos
{
    public class DocumentToAddDto
    {
        public string Libelle { get; set; }
        public string Description { get; set; }
        public int CategorieId { get; set; }
    }
}
