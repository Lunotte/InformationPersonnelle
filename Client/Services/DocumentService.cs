using InformationPersonnelle.Shared.Dtos;
using InformationPersonnelle.Client.Services.Contracts;
using System.Net.Http.Json;

namespace InformationPersonnelle.Client.Services
{
    public class DocumentService : IDocumentService
    {
        private readonly HttpClient httpClient;

        public DocumentService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<DocumentDto> GetDocument(int documentId)
        {
            var response = await this.httpClient.GetAsync($"api/Document/{documentId}");

            if (response.IsSuccessStatusCode)
            {
                if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                {
                    return default(DocumentDto);
                }
                return await response.Content.ReadFromJsonAsync<DocumentDto>();
            } else
            {
                var message = await response.Content.ReadAsStringAsync();
                throw new Exception(message);
            }
            
        }

        public async Task<IEnumerable<DocumentDto>> GetDocuments()
        {
            try
            {
                return await this.httpClient.GetFromJsonAsync<IEnumerable<DocumentDto>>("api/Document");
            }
            catch (Exception ess)
            {

                throw;
            }
        }
    }
}
