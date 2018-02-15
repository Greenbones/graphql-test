using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MDT.GraphQLData.Models.GraphQL
{
    public class Queries : ObjectGraphType
    {
        public Queries(DogQueries dogQueries, OwnerQueries ownerQueries)
        {
            dogQueries.initialize(this);
            ownerQueries.initialize(this);
        }

    }
}
