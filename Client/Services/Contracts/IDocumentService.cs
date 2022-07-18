using InformationPersonnelle.Shared.Dtos;

namespace InformationPersonnelle.Client.Services.Contracts
{
    public interface IDocumentService
    {
        Task<HashSet<CategorieVue>> GetDocuments();
        Task<DocumentDto> GetDocument(int documentId);
    }
}
