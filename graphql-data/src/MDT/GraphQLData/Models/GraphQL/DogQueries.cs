using GraphQL.Types;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MDT.GraphQLData.Models.GraphQL
{
    public class DogQueries : ObjectGraphType<Dog>, IInitializingQuery
    {
        private readonly PetContext _dogContext;

        public DogQueries(PetContext dogContext)
        {
            _dogContext = dogContext;
        }

        public void initialize(ObjectGraphType queryType)
        {
            queryType.Field<ListGraphType<DogType>>("allDogs",
                resolve: context => _dogContext.Dogs.Include(dog => dog.Owner).ToList()); //Load Owners as well, so that we have the full data set for GraphQL. Let's DogType access the owner for each dog.

            queryType.Field<DogType>("dog",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "id", Description = "Dog Id" }),
                resolve: context => _dogContext.Dogs.FirstOrDefault(dog => dog.Id == context.GetArgument<int>("id", 0)));
        }
    }
}
