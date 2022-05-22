using InformationPersonnelle.Shared.Dtos;

namespace InformationPersonnelle.Client.Services.Contracts
{
    public interface IDocumentService
    {
        Task<IEnumerable<DocumentDto>> GetDocuments();
        Task<DocumentDto> GetDocument(int documentId);
    }
}
