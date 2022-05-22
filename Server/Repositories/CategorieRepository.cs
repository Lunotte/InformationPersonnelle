using Microsoft.EntityFrameworkCore;
using InformationPersonnelle.Server.Data;
using InformationPersonnelle.Server.Entities;
using InformationPersonnelle.Server.Repositories.Contracts;

namespace InformationPersonnelle.Server.Repositories
{
    public class CategorieRepository : ICategorieRepository
    {
        private readonly InformationPersonnelleDbContext informationPersonnelleDbContext;

        public CategorieRepository(InformationPersonnelleDbContext informationPersonnelleDbContext)
        {
            this.informationPersonnelleDbContext = informationPersonnelleDbContext;
        }

        public Task<Categorie> GetCategorie(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Categorie>> GetCategories()
        {
            return await this.informationPersonnelleDbContext.Categories.ToListAsync();
        }

    }
}
