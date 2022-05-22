namespace InformationPersonnelle.Server.Entities
{
    public class Tag
    {
        public Tag()
        {
            this.DocumentTags = new HashSet<DocumentTag>();
        }
        public int Id { get; set; }
        public string Libelle { get; set; }
        public string Couleur { get; set; }
        public virtual ICollection<DocumentTag> DocumentTags { get; set; }
    }
}
