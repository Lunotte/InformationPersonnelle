using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InformationPersonnelle.Shared.Dtos
{
    public class DocumentDto
    {
        public int Id { get; set; }
        public string Libelle { get; set; }
        public string? Description { get; set; }
        public string Chemin { get; set; }
        public IEnumerable<TagDto> Tags { get; set; }

    }

}
