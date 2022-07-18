using InformationPersonnelle.Shared.Dtos;
using Microsoft.AspNetCore.Mvc;
using InformationPersonnelle.Server.Extensions;
using InformationPersonnelle.Server.Repositories.Contracts;

namespace InformationPersonnelle.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentController : ControllerBase
    {
        private readonly IDocumentRepository documentRepository;
        private readonly ICategorieRepository categorieRepository;

        public DocumentController(IDocumentRepository documentRepository, ICategorieRepository categorieRepository)
        {
            this.documentRepository = documentRepository;
            this.categorieRepository = categorieRepository;
        }

        [HttpGet]
        public async Task<ActionResult<List<CategorieVue>>> GetDocuments()
        {
            try
            {
                var documents = await this.documentRepository.GetDocuments();
                var categories = await this.categorieRepository.GetCategories();

                var categoriesVue = categories.Select(cat => new CategorieVue
                {
                   Id = cat.Id,
                   Libelle = cat.Libelle,
                   Niveau = cat.Niveau,
                   ParentCategorieId = cat.ParentCategorieId
                }).ToList();

                var categorieById = categoriesVue.ToDictionary(c => c.Id);

                foreach (var categorie in categoriesVue.Where(cat => cat.ParentCategorieId != null))
                {
                    categorie.Documents = documents.Where(doc => doc.CategorieId == categorie.Id).Select(doc => 
                        new DocumentDto { 
                            Id = doc.Id,
                            Libelle = doc.Libelle,
                            Chemin = doc.Chemin,
                            Description = doc.Description,
                            Tags = doc.DocumentTags.Select(dt => new TagDto { Id = dt.Tag.Id, Libelle = dt.Tag.Libelle, Couleur = dt.Tag.Couleur }).ToList()
                        }).ToHashSet();
                    //var catParente = (CategorieVue)categorieById[categorie.ParentCategorieId.Value].Clone();


                    //if(catParente.ParentCategorie != null)
                    //{
                    //    catParente.ParentCategorie.SousCategories = new HashSet<CategorieVue>();
                    //}
                    //var categorie2 = (CategorieVue)categorie.Clone();
                    //categorie2.ParentCategorie = null;
                    //categorie2.SousCategories = new HashSet<CategorieVue>();
                    //categorie.ParentCategorie = catParente;
                    //categorie.ParentCategorie.SousCategories.Add(categorie2);


                    categorie.ParentCategorie = categorieById[categorie.ParentCategorieId.Value];
                    categorie.ParentCategorie.SousCategories.Add(categorie);
                }

                var categoriesSorted = categoriesVue.Where(c => c.ParentCategorieId == null);

               // IEnumerable<DocumentDto> documentsDto = DtoConversions.convertDocumentsToDto(documents);
                return Ok(categoriesSorted.ToList());




                //foreach (var categorie in categoriesVue.Where(cat => cat.ParentCategorieId != null))
                //{
                //    // categorie.Documents = documents.Where(doc => doc.CategorieId == categorie.Id).ToHashSet();
                //    categorie.ParentCategorie = categorieById[categorie.ParentCategorieId.Value];
                //    categorie.ParentCategorie.SousCategories.Add(categorie);

                //    var documentsFiltre = documents.Where(doc => doc.CategorieId == categorie.Id).ToHashSet();
                //    var parent = categorieById[categorie.ParentCategorieId.Value];


                //    CategorieDtos.Add(new CategorieDto
                //    {
                //        Id = categorie.Id,
                //        Libelle = categorie.Libelle,
                //        Niveau = categorie.Niveau,
                //        //ParentCategorie = parent,
                //        Documents = documentsFiltre.Select(document => new DocumentDto()
                //        {
                //            Id = document.Id,
                //            Libelle = document.Libelle,
                //            Description = document.Description,
                //            Chemin = document.Chemin,
                //            Tags = document.DocumentTags.Select(dt => new TagDto { Id = dt.Tag.Id, Libelle = dt.Tag.Libelle, Couleur = dt.Tag.Couleur }).ToHashSet()
                //        }).ToHashSet()
                //    });
                //}

                //var categoriesSorted = categoriesVue.Where(c => c.ParentCategorieId == null);

                //// IEnumerable<DocumentDto> documentsDto = DtoConversions.convertDocumentsToDto(documents);
                //return Ok(categoriesSorted.ToList());
            }
            catch(Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database");
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<DocumentDto>> GetDocument(int id)
        {
            try
            {
                var document = await documentRepository.GetDocument(id);
                if(document == null)
                {
                    return BadRequest();
                }
                return Ok(DtoConversions.convertDocumentToDto(document));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database");
            }
        }

    }
}
