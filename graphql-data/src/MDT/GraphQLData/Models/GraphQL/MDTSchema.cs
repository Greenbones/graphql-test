using GraphQL.Types;
using System;

namespace MDT.GraphQLData.Models.GraphQL
{
    public class MDTSchema : Schema 
    {
        public MDTSchema(Func<Type, GraphType> resolveType) : base(resolveType)
        {
            Query = (Queries)resolveType(typeof(Queries));
        }
    }
}
