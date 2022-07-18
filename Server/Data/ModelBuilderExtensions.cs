using InformationPersonnelle.Server.Entities;
using Microsoft.EntityFrameworkCore;
using System;

public static class ModelBuilderExtensions
{
    public static void Seed(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Anniversaire>().ToTable("Anniversaires");
        modelBuilder.Entity<Anniversaire>().Property(a => a.Nom).HasMaxLength(255);
        modelBuilder.Entity<Anniversaire>().Property(a => a.Prenom).HasMaxLength(255);

        modelBuilder.Entity<Categorie>().ToTable("Categories");
        modelBuilder.Entity<Categorie>().Property(c => c.Libelle).HasMaxLength(255);
        modelBuilder.Entity<Categorie>()
            .Property(c => c.ParentCategorieId)
            .IsRequired(false);
            
        modelBuilder.Entity<Categorie>().HasIndex(categorie => categorie.Libelle).IsUnique();

        modelBuilder.Entity<Document>().ToTable("Documents").HasOne(d => d.Categorie);
        modelBuilder.Entity<Document>().Property(d => d.Libelle).HasMaxLength(255);
        modelBuilder.Entity<Document>().Property(d => d.Chemin).HasMaxLength(255);
        modelBuilder.Entity<Document>().HasIndex(document => new { document.Libelle, document.Chemin, document.CategorieId }).IsUnique();
        modelBuilder.Entity<Document>()
           .Property(d => d.Creation)
           .HasColumnType("timestamp without time zone")
           .HasDefaultValueSql("NOW()")
           .ValueGeneratedOnAdd();

        modelBuilder.Entity<Document>()
         .Property(d => d.Modification)
         .HasColumnType("timestamp without time zone")
         .HasDefaultValueSql("NOW()")
         .ValueGeneratedOnAdd();

        modelBuilder.Entity<Tag>().ToTable("Tags");
        modelBuilder.Entity<Tag>().Property(t => t.Libelle).HasMaxLength(255);
        modelBuilder.Entity<Tag>().Property(t => t.Couleur).HasMaxLength(25).HasDefaultValue("#008000");
        modelBuilder.Entity<Tag>().HasIndex(tag => tag.Libelle).IsUnique();

        modelBuilder.Entity<DocumentTag>().HasKey(dt => new { dt.DocumentId, dt.TagId });
        modelBuilder.Entity<DocumentTag>()
            .HasOne(dt => dt.Document)
            .WithMany(d => d.DocumentTags)
            .HasForeignKey(dt => dt.DocumentId);
        modelBuilder.Entity<DocumentTag>()
            .HasOne(dt => dt.Tag)
            .WithMany(t => t.DocumentTags)
            .HasForeignKey(dt => dt.TagId);
    }
}
