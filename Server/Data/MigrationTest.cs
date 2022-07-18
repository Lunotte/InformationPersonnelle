using InformationPersonnelle.Server.Data;
using InformationPersonnelle.Server.Entities;
using System.Collections.Generic;

namespace InformationPersonnelle.Server.Data
{
    public static class MigrationTest
    {
        public static void Initialize(InformationPersonnelleDbContext context)
        {

            Anniversaire anniversaire1 = new Anniversaire
            {
                Nom = "Beaugrand",
                Prenom = "Charly",
                DateNaissance = new DateOnly(1991, 6, 7)
            };
            context.Anniversaires.Add(anniversaire1);

            Anniversaire anniversaire2 = new Anniversaire
            {
                Nom = "Tournaille",
                Prenom = "Baptiste",
                DateNaissance = new DateOnly(1992, 5, 12)
            };
            context.Anniversaires.Add(anniversaire2);

            context.SaveChanges();


            var tag1 = new Tag { Libelle = "Salaire", Couleur = "#F00020" };
            var tag2 = new Tag { Libelle = "Capgemini", Couleur = "#0080FF" };
            var tag3 = new Tag { Libelle = "Apef", Couleur = "#008000" };

            context.Tags.AddRange(new List<Tag>() { tag1, tag2, tag3 });
            context.SaveChanges();

            var categorie1 = new Categorie { Libelle = "Bulletin de salaire", ParentCategorie = null };
            context.Categories.Add(categorie1);

            var categorie11 = new Categorie { Libelle = "Capgemini", ParentCategorie = categorie1 };
            context.Categories.Add(categorie11);
            var categorie111 = new Categorie { Libelle = "Charly", ParentCategorie = categorie11 };
            context.Categories.Add(categorie111);
            var categorie112 = new Categorie { Libelle = "Valeria", ParentCategorie = categorie11 };
            context.Categories.Add(categorie112);
            var categorie12 = new Categorie { Libelle = "Axians", ParentCategorie = categorie1 };
            context.Categories.Add(categorie12);

            var categorie2 = new Categorie { Libelle = "Apef", ParentCategorie = null };
            context.Categories.Add(categorie2);

            context.SaveChanges();

            var document1 = new Document
            {
                Libelle = "Salaire 01/2022",
                Description = null,
                Chemin = "/test/salaires/salaire_01-2022.png",
                Categorie = categorie111,
            };
            context.Documents.Add(document1);

            var document2 = new Document
            {
                Libelle = "Salaire 02/2022",
                Description = null,
                Chemin = "/test/salaires/salaire_02-2022.png",
                Categorie = categorie112,
            };
            context.Documents.Add(document2);

            var document3 = new Document
            {
                Libelle = "Apef Facture 01/2022",
                Description = "Seulement 4 heures ont été travaillées",
                Chemin = "/test/apef/apef_facture_01-2022.png",
                Categorie = categorie2
            };
            context.Documents.Add(document3);

            var document4 = new Document
            {
                Libelle = "Apef Facture 02/2022",
                Description = "Seulement 1 heures ont été travaillées",
                Chemin = "/test/apef/apef_facture_02-2022.png",
                Categorie = categorie2
            };
            context.Documents.Add(document4);

            var document5 = new Document
            {
                Libelle = "Apef Facture 03/2022",
                Description = "Toutes les heures ont été effectuées",
                Chemin = "/test/apef/apef_facture_03-2022.png",
                Categorie = categorie2
            };
            context.Documents.Add(document5);

            var document6 = new Document
            {
                Libelle = "Apef Facture 04/2022",
                Description = null,
                Chemin = "/test/apef/apef_facture_04-2022.png",
                Categorie = categorie2
            };
            context.Documents.Add(document6);

            var document7 = new Document
            {
                Libelle = "Apef Facture 05/2022",
                Description = null,
                Chemin = "/test/apef/apef_facture_05-2022.png",
                Categorie = categorie2
            };
            context.Documents.Add(document7);
            context.SaveChanges();

            var documentTag1 = new DocumentTag { Document = document1, Tag = tag1 };
            var documentTag12 = new DocumentTag { Document = document1, Tag = tag2 };
            var documentTag2 = new DocumentTag { Document = document2, Tag = tag2 };
            var documentTag21 = new DocumentTag { Document = document2, Tag = tag1 };
            var documentTag3 = new DocumentTag { Document = document3, Tag = tag3 };
            var documentTag4 = new DocumentTag { Document = document4, Tag = tag3 };
            var documentTag5 = new DocumentTag { Document = document5, Tag = tag3 };
            var documentTag6 = new DocumentTag { Document = document6, Tag = tag3 };
            var documentTag7 = new DocumentTag { Document = document7, Tag = tag3 };
            context.DocumentTags.AddRange(new List<DocumentTag>() { documentTag1, documentTag2, documentTag3,
                                                                    documentTag4, documentTag5, documentTag6, documentTag7,
                                                                    documentTag12, documentTag21});
            context.SaveChanges();
        }
    }
}
