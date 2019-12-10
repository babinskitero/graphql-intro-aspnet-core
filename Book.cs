using HotChocolate;
using HotChocolate.Types;

public class Book
{
    public int Id { get; set; }
    [GraphQLType(typeof(NonNullType<StringType>))]
    //I changed from [GraphQLNonNullType] to what I have because UseFiltering middleware applied to it causes it to fail build
    public string Title { get; set; }
    public int Pages { get; set; }
    public int Chapters { get; set; }
    [GraphQLNonNullType]
    public Author Author { get; set; }
}