using InformationPersonnelle.Shared.Dtos;
using Microsoft.AspNetCore.Components;
using InformationPersonnelle.Client.Services.Contracts;

namespace InformationPersonnelle.Client.Pages
{
    public class DocumentDetailsBase : ComponentBase
    {
        [Parameter]
        public int Id { get; set; }

        [Inject]
        public IDocumentService DocumentService { get; set; }

        public DocumentDto Document { get; set; }
        public string ErrorMessage { get; set; }

        protected override async Task OnInitializedAsync()
        {
            try
            {
                Document = await DocumentService.GetDocument(Id);
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }
    }
}
