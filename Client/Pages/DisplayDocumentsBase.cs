
using InformationPersonnelle.Shared.Dtos;
using Microsoft.AspNetCore.Components;

namespace InformationPersonnelle.Client.Pages
{
    public class DisplayDocumentsBase : ComponentBase
    {
        [Parameter]
        public IEnumerable<DocumentDto> Documents { get; set; }
    }
}
