
namespace InformationPersonnelle.Shared.Dtos
{
    public class CategorieVue : ICloneable
    {
        public int Id { get; set; }
        public string Libelle { get; set; }
        public int Niveau { get; set; }
        public int? ParentCategorieId { get; set; }
        public CategorieVue ParentCategorie { get; set; }
        public ISet<CategorieVue> SousCategories { get; set; }
        public ISet<DocumentDto> Documents { get; set; }
        public CategorieVue()
        {
            SousCategories = new HashSet<CategorieVue>();
            Documents = new HashSet<DocumentDto>();
        }

        public object Clone()
        {
            return new CategorieVue
            {
                Id = Id,
                Libelle = Libelle,
                Niveau = Niveau,
                ParentCategorieId = ParentCategorieId,
                ParentCategorie = ParentCategorie,
                SousCategories = SousCategories,
                Documents = Documents
            };
        }
    }
}
