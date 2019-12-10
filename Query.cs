using System.Collections.Generic;
using System.Linq;
using HotChocolate;
using HotChocolate.Types;
using Microsoft.EntityFrameworkCore;

public class Query
{
    [GraphQLNonNullType]
    public IQueryable<Book> GetBooks([Service] BookDbContext dbContext) =>  dbContext.Books.Include(x => x.Author); 
    public IQueryable<Author> GetAuthors([Service] BookDbContext dbContext) =>  dbContext.Authors; 

    //By convention GetBook() will be recorded as book in the query field.
    public Book GetBook([Service] BookDbContext dbContext, int id) => dbContext.Books.FirstOrDefault(x => x.Id == id);
}

public class QueryType
    : ObjectType<Query>
{
    protected override void Configure(IObjectTypeDescriptor<Query> descriptor)
    {
        descriptor.Field(t => t.GetBooks(default))
//            .Type<ListType<NonNullType<PersonType>>>();
//            .UseLimitOffsetPaging<Book>()
            .UseFiltering()
            .UseSorting();

        descriptor.Field(t => t.GetAuthors(default));
//            .UseLimitOffsetPaging<Author>();
    }
    
    
}