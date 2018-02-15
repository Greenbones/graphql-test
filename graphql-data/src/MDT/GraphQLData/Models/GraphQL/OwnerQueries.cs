using GraphQL.Types;
using System.Linq;

namespace MDT.GraphQLData.Models.GraphQL
{
    public class OwnerQueries : ObjectGraphType<Owner>, IInitializingQuery
    {
        private readonly PetContext _ownerContext;

        public OwnerQueries(PetContext ownerContext)
        {
            _ownerContext = ownerContext;
        }

        public void initialize(ObjectGraphType queryType)
        {
            queryType.Field<ListGraphType<OwnerType>>("allOwners",
                resolve: context => _ownerContext.Owners);

            queryType.Field<OwnerType>("owner",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "id", Description = "Owner Id" }),
                resolve: context => _ownerContext.Owners.Where(owner => owner.Id == context.GetArgument<int>("id", 0)));
        }
    }
}