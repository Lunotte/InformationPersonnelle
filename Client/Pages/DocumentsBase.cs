using InformationPersonnelle.Shared.Dtos;
using Microsoft.AspNetCore.Components;
using InformationPersonnelle.Client.Services.Contracts;

namespace InformationPersonnelle.Client.Pages
{
    public class Documentsbase : ComponentBase
    {
        [Inject]
        public IDocumentService DocumentService { get; set; }
        public IEnumerable<DocumentDto> Documents { get; set; }
        protected override async Task OnInitializedAsync()
        {
            Documents = await DocumentService.GetDocuments();
        }

        protected IOrderedEnumerable<IGrouping<int, DocumentDto>> GetGroupedDocumentsByCategory()
        {
            //var totui = Documents.GroupBy(d => d.CategorieId)
            //  .Select(group => new
            //  {
            //      id = group.Key,u
            //      Documents = group.OrderByDescending(x => x.CategorieId)
            //  })
            //  .OrderBy(group => group.Documents.First().CategorieId);

            return Documents.GroupBy(d => d.CategorieId).OrderBy(group => group.Key);
        }

        protected string GetCategoryName(IGrouping<int, DocumentDto> groupedDocumentsDto)
        {
            return groupedDocumentsDto.FirstOrDefault(pg => pg.CategorieId == groupedDocumentsDto.Key).CategorieLibelle;
        }
    }
}
