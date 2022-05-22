using InformationPersonnelle.Shared.Dtos;
using InformationPersonnelle.Server.Entities;

namespace InformationPersonnelle.Server.Extensions
{
    public static class DtoConversions
    {
        public static IEnumerable<DocumentDto> convertDocumentsToDto(this IEnumerable<Document> documents)
        {
            return documents.Select(document => convertDocumentToDto(document));
        }

        public static DocumentDto convertDocumentToDto(this Document document)
        {
            return new DocumentDto()
            {
                Id = document.Id,
                Libelle = document.Libelle,
                Description = document.Description,
                Chemin = document.Chemin,
                CategorieId = document.Categorie.Id,
                CategorieLibelle = document.Categorie.Libelle,
                Tags = document.DocumentTags.Select(dt => new TagDto { Id = dt.Tag.Id, Libelle = dt.Tag.Libelle, Couleur = dt.Tag.Couleur }).ToList()
            };
        }
    }
}
