using InformationPersonnelle.Server.Entities;

namespace InformationPersonnelle.Server.Repositories.Contracts
{
    public interface IDocumentRepository
    {
        Task<IEnumerable<Document>> GetDocuments();
        Task<Document> GetDocument(int id);
    }
}
