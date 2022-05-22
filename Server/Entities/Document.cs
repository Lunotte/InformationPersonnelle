using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InformationPersonnelle.Server.Entities
{
    
    public class Document : DateTimeOperation
    {
        public Document()
        {
            this.DocumentTags = new HashSet<DocumentTag>();
        }
        public int Id { get; set; }
        public string Libelle { get; set; }
        public string? Description { get; set; }
        public string Chemin { get; set; }
        public int CategorieId { get; set; }
        public virtual Categorie Categorie { get; set; }
        public virtual ICollection<DocumentTag> DocumentTags { get; set; }
    }
}
