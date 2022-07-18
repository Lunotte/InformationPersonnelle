using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InformationPersonnelle.Shared.Dtos
{
    public class CategorieDto
    {
        public int Id { get; set; }
        public string Libelle { get; set; }
        public int Niveau { get; set; }
        public CategorieDto ParentCategorie { get; set; }
        public ISet<CategorieDto> SousCategories { get; set; }
        public ISet<DocumentDto> Documents { get; set; }
        public CategorieDto()
        {
            SousCategories = new HashSet<CategorieDto>();
            Documents = new HashSet<DocumentDto>();
        }
    }
}
