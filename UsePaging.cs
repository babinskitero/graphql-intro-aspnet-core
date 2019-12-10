using System;
using System.Collections.Generic;
using System.Linq;
using HotChocolate;
using HotChocolate.Resolvers;
using HotChocolate.Types;
using HotChocolate.Types.Descriptors;


public static class Test
{
    public static IObjectFieldDescriptor UseLimitOffsetPaging<TSchemaType>(
        this IObjectFieldDescriptor descriptor)
        where TSchemaType : class
    {
        descriptor
            .Type<PaginationPayloadType<TSchemaType>>()
            .Argument("pageIndex", a => a.Type<IntType>())
            .Argument("pageSize", a => a.Type<IntType>())
            .Use(next => async context =>
            {
                await next(context);

                if (context.Result is IQueryable<TSchemaType> list)
                {
                    
                    var paginatedList = await PaginatedList<TSchemaType>.CreateAsync(list,
                        context.Argument<int>("pageIndex"), context.Argument<int>("pageSize"));
                    var result = new PaginationPayload<TSchemaType>(paginatedList.ToList(),
                        paginatedList.HasNextPage, paginatedList.HasPreviousPage);
                    context.Result = result;
                }

            });

        return descriptor;
    }
}

public class PageInfo
{
    public bool HasPreviousPage { get; set; }
    public bool HasNextPage { get; set; }
}

public class PaginationPayload <T> where T : class
{
    public PaginationPayload(IEnumerable<T> list, bool hasNext, bool hasPrevious)
    {
        FieldType = list;
        PageInfo = new PageInfo { HasNextPage = hasNext, HasPreviousPage = hasPrevious};
    }
    public IEnumerable<T> FieldType { get; }
    [GraphQLNonNullType]
    public PageInfo PageInfo { get; }//TODO: use non-null attribute
}

public class PaginationPayloadType<T> : ObjectType<PaginationPayload<T>> where T : class
{
    protected override void Configure(IObjectTypeDescriptor<PaginationPayload<T>> descriptor)
    {
        descriptor.Field(x => x.FieldType)
            .Name("list");
    }
}

