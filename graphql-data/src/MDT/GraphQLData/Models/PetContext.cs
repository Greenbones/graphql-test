using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MDT.GraphQLData
{
    public class PetContext : DbContext
    {

        public PetContext(DbContextOptions<PetContext> options) : base(options) { }

        public DbSet<Dog> Dogs { get; set; }
        public DbSet<Owner> Owners { get; set; }
    }
}
