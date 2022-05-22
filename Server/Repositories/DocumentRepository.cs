using Microsoft.EntityFrameworkCore;
using InformationPersonnelle.Server.Data;
using InformationPersonnelle.Server.Entities;
using InformationPersonnelle.Server.Repositories.Contracts;

namespace InformationPersonnelle.Server.Repositories
{
    public class DocumentRepository : IDocumentRepository
    {
        private readonly InformationPersonnelleDbContext informationPersonnelleDbContext;

        public DocumentRepository(InformationPersonnelleDbContext informationPersonnelleDbContext)
        {
            this.informationPersonnelleDbContext = informationPersonnelleDbContext;
        }

        public async Task<Document> GetDocument(int id)
        {
            return await informationPersonnelleDbContext.Documents.SingleOrDefaultAsync(d => d.Id == id);
        }

        //public async Task<DocumentTag> GetDocumentTag(int id)
        //{
        //    return await informationPersonnelleDbContext.DocumentTags.SingleOrDefaultAsync(d => d.DocumentId == id);
        //}

        public async Task<IEnumerable<Document>> GetDocuments()
        {
            return await informationPersonnelleDbContext.Documents.ToListAsync();
        }
    }
}
