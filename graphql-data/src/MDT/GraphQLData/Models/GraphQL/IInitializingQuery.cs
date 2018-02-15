using GraphQL.Types;

namespace MDT.GraphQLData.Models.GraphQL
{
    public interface IInitializingQuery
    {
        void initialize(ObjectGraphType graphType);
    }
}