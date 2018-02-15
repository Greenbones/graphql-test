using System.Collections.Generic;

namespace MDT.GraphQLData
{
    public class Owner
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public IList<Dog> Dogs { get; set; }
    }
}