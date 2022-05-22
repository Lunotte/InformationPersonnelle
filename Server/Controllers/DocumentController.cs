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

        public DocumentController(IDocumentRepository documentRepository)
        {
            this.documentRepository = documentRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DocumentDto>>> GetDocuments()
        {
            try
            {
                var documents = await this.documentRepository.GetDocuments();
                return Ok(DtoConversions.convertDocumentsToDto(documents));
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
