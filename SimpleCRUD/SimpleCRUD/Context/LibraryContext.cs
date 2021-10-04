using Microsoft.EntityFrameworkCore;
using SimpleCRUD.Entities;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleCRUD.Context
{
    public class LibraryContext: DbContext
    {
        public LibraryContext(DbContextOptions<LibraryContext> options) : base(options) { }

        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Image> Images { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>().HasKey(b => b.Id);
            modelBuilder.Entity<Author>().HasKey(a => a.Id);
            modelBuilder.Entity<Image>().HasKey(i => i.Id);

            //relationships

            //books and authors
            modelBuilder.Entity<Book>().HasMany(books => books.Author)
                                       .WithMany(authors => authors.Books);

            //books and imagens
            modelBuilder.Entity<Book>().HasMany(books => books.Image)
                                        .WithOne(image => image.Books);

        }
        protected override void OnConfiguring(DbContextOptionsBuilder Builder)
        {

           
        }


    }
}
