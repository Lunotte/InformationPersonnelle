using InformationPersonnelle.Client.Services.Contracts;
using InformationPersonnelle.Shared.Dtos;
using Microsoft.AspNetCore.Components;

namespace InformationPersonnelle.Client.Pages
{
    public class Documentsbase : ComponentBase
    {
        [Inject]
        public IDocumentService DocumentService { get; set; }
        public ISet<CategorieVue> Categories { get; set; }
        public ISet<CategorieVue> CategoriesPourAfficher { get; set; }
        public CategorieVue CategoriesParente { get; set; }
        public ISet<DocumentDto> DocumentsPourAfficher { get; set; }

        public Documentsbase()
        {
            DocumentsPourAfficher = new HashSet<DocumentDto>();
            CategoriesParente = null;
        }
        protected override async Task OnInitializedAsync()
        {
            Categories = await DocumentService.GetDocuments();
            CategoriesPourAfficher = Categories.Select(x => (CategorieVue)x.Clone()).ToHashSet();
        }

        protected void ouvrirDossier(CategorieVue categorie)
        {
            CategoriesPourAfficher = categorie.SousCategories.ToHashSet();
            CategoriesParente = categorie;
            DocumentsPourAfficher = categorie.Documents.ToHashSet();
        }

        protected void retourRacine()
        {
            resetCategoriesEtDocuments();
        }

        private void resetCategoriesEtDocuments()
        {
            CategoriesPourAfficher = Categories.Select(x => (CategorieVue)x.Clone()).ToHashSet();
            DocumentsPourAfficher = new HashSet<DocumentDto>();
        }

        protected void retourParent()
        {
            bool possedeParent = false;

            //  Categories.Select(subList => subList?.).OfType<dynamic>();

            var toto = Categories.Select(subList => subList).OfType<List<dynamic>>().ToList();

            if (CategoriesParente != null && CategoriesParente.ParentCategorieId != null)
            {
                CategorieVue categorieTrouve = GetCategoriesById(Categories, CategoriesParente.Id);

                if (categorieTrouve.ParentCategorieId != null)
                {
                    CategorieVue categorieTrouveEncore = GetCategoriesById(Categories, (int)categorieTrouve.ParentCategorieId);
                    if (categorieTrouveEncore != null)
                    {
                        CategoriesPourAfficher = categorieTrouveEncore.SousCategories.ToHashSet();
                        DocumentsPourAfficher = categorieTrouveEncore.Documents.ToHashSet();
                        CategoriesParente = categorieTrouveEncore;
                        possedeParent = true;
                    }
                }
            }

            if (!possedeParent)
            {
                resetCategoriesEtDocuments();
            }

        }

        private CategorieVue GetCategoriesById(ISet<CategorieVue> categories, int idCategorie)
        {

            CategorieVue totot = null;

            foreach (var categorie in categories)
            {
                Console.WriteLine($"Afficher lid categorie {categorie.Id} et {idCategorie}");
                if (categorie.Id == idCategorie)
                {
                    totot = categorie;
                    break;
                }
                if (categorie.SousCategories.Count() > 0)
                {
                    return GetCategoriesById(categorie.SousCategories.ToHashSet(), idCategorie);
                }
            }

            return totot;
        }

        //protected IOrderedEnumerable<IGrouping<int, DocumentDto>> GetGroupedDocumentsByCategory()
        //{
        //    //var totui = Documents.GroupBy(d => d.CategorieId)
        //    //  .Select(group => new
        //    //  {
        //    //      id = group.Key,u
        //    //      Documents = group.OrderByDescending(x => x.CategorieId)
        //    //  })
        //    //  .OrderBy(group => group.Documents.First().CategorieId);

        //    return Documents.GroupBy(d => d.CategorieId).OrderBy(group => group.Key);
        //}

        //protected string GetCategoryName(IGrouping<int, DocumentDto> groupedDocumentsDto)
        //{
        //    return groupedDocumentsDto.FirstOrDefault(pg => pg.CategorieId == groupedDocumentsDto.Key).CategorieLibelle;
        //}
    }
}
