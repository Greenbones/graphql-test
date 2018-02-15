using GraphQL.Types;
using System.Linq;

namespace MDT.GraphQLData.Models.GraphQL
{
    public class OwnerType : ObjectGraphType<Owner>
    {
        public OwnerType(PetContext dogContext)
        {
            Field(x => x.Id).Description("Owner Id");
            Field(x => x.Name).Description("Owner Name");
            Field(x => x.Age).Description("Owner Age");

            Field<ListGraphType<DogType>>("dogs",
                resolve: context => dogContext.Dogs.Where(dog => context.Source.Dogs.Contains(dog)));
        }
    }
}