namespace TrocCommunity.Core.Models
{
    public class Categorie: BaseEntity
    {
        public string NomCategorie { get; set; }
        public string Description { get; set; } //sous description

        public Categorie()
        {

        }

        public Categorie(string nomCategorie)
        {
            NomCategorie = nomCategorie;
        }

        public Categorie(string nomCategorie, string description)
        {
            NomCategorie = nomCategorie;
            Description = description;
        }
    }
}