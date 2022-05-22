using InformationPersonnelle.Server.Entities;

namespace InformationPersonnelle.Server.Repositories.Contracts
{
    public interface ICategorieRepository
    {
        Task<IEnumerable<Categorie>> GetCategories();
        Task<Categorie> GetCategorie(int id);
    }
}
