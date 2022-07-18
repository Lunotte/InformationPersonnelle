using InformationPersonnelle.Shared.Dtos;
using InformationPersonnelle.Server.Entities;

namespace InformationPersonnelle.Server.Extensions
{
    public static class DtoConversions
    {
        public static IEnumerable<DocumentDto> convertDocumentsToDto(this IEnumerable<Document> documents)
        {
            var res = documents.GroupBy(doc => doc.CategorieId).Select(group => new
            {
                id = group.Key,
                Documents = group.OrderByDescending(x => x.CategorieId)
            })
            .OrderBy(group => group.Documents.First().CategorieId).ToList();


            var docs =  documents.Select(document => convertDocumentToDto(document));


            return docs;
        }

        public static DocumentDto convertDocumentToDto(this Document document)
        {
            var categories = new List<CategorieDto>();
            return new DocumentDto()
            {
                Id = document.Id,
                Libelle = document.Libelle,
                Description = document.Description,
                Chemin = document.Chemin,
                //Categories = getAllCategorieDto(document.Categorie, categories),
                Tags = document.DocumentTags.Select(dt => new TagDto { Id = dt.Tag.Id, Libelle = dt.Tag.Libelle, Couleur = dt.Tag.Couleur }).ToList()
            };
        }

        private static List<CategorieDto> getAllCategorieDto(this Categorie categorie, List<CategorieDto> categories)
        {
            if (categorie.ParentCategorie != null)
            {
                categories.Add(convertCategorieToDto(categorie));
                getAllCategorieDto(categorie.ParentCategorie, categories);
            }
            categories.Add(convertCategorieToDto(categorie));
            return categories;
        }

        public static CategorieDto convertCategorieToDto(this Categorie categorie)
        {
            return new CategorieDto()
            {
                Id = categorie.Id,
                Libelle = categorie.Libelle
            };
        }

    }
}
