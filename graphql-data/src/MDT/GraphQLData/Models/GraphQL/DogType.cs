using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MDT.GraphQLData.Models.GraphQL
{
    public class DogType : ObjectGraphType<Dog>
    {
        public DogType(PetContext ownerContext)
        {
            Field(x => x.Id).Description("Dog Id");
            Field(x => x.Name).Description("Dog Name");
            Field(x => x.Breed).Description("Dog Breed");

            Field<OwnerType>("owner",
                resolve: context => context.Source.Owner);
        }
    }
}
