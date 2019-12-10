using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace graphql_intro_web_core.Data
{
    public class SeedDataGenerator
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using var context = new BookDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<BookDbContext>>());
            // Look for any books.
            if (context.Books.Any())
            {
                return;   // Data was already seeded
            }

            var james = new Author {Name = "James Collins"};
            var newton = new Author() {Name = "Isaac Newton"};
            var moses = new Author() {Name = "Moses Wester"};
            var santos = new Author() {Name = " Santos Jose"};
            var chuks = new Author() {Name = "Chuks Peter"};

            context.Books.AddRange(
                new Book()
                {
                    Title = "How to Hack your life",
                    Chapters = 10,
                    Pages = 1040,
                    Author = james
                },
                new Book()
                {
                    Title = "Bio Life Trust",
                    Pages = 300,
                    Chapters = 4,
                    Author = james
                },
                new Book()
                {
                    Title = "How to write JavaScript",
                    Pages = 300,
                    Chapters = 4,
                    Author = santos
                },
                new Book()
                {
                    Title = "How to Dance Salsa",
                    Pages = 500,
                    Chapters = 6,
                    Author = moses
                },
                new Book()
                {
                    Title = "Biology for Programmers",
                    Pages = 506,
                    Chapters = 10,
                    Author = newton
                },
                new Book()
                {
                    Title = "Advanced GraphQL Development in C#",
                    Pages = 300,
                    Chapters = 8,
                    Author = chuks
                },
                new Book()
                {
                    Title = "Mastering Apollo GraphQL",
                    Pages = 400,
                    Chapters = 10,
                    Author = chuks
                }
            );

            context.SaveChanges();
        }
    }
}