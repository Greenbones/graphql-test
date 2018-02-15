using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using GraphQL;
using GraphQL.Types;
using MDT.GraphQLData;
using MDT.GraphQLData.Models.GraphQL;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
namespace graphql_data
{
    public class Startup
    {
        public IConfiguration Configuration { get; set; } 

        public Startup()
        {
            IConfigurationBuilder builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");
            Configuration = builder.Build();

        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            // When used with ASP.net core, add these lines to Startup.cs
            var connectionString = Configuration.GetConnectionString("DogContext");

            services.AddDbContext<PetContext>(options => options.UseNpgsql(connectionString));

            services.AddMvc();

            services.AddScoped<DogQueries>();
            services.AddScoped<OwnerQueries>();
            services.AddScoped<Queries>();
            services.AddTransient<DogType>();
            services.AddTransient<OwnerType>();
            services.AddScoped<IDocumentExecuter, DocumentExecuter>();
            var sp = services.BuildServiceProvider();
            services.AddScoped<ISchema>(_ => new MDTSchema(type => (GraphType)sp.GetService(type)) { Query = sp.GetService<Queries>() });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, PetContext petContext)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();

            petContext.Database.EnsureCreated();

            petContext.Dogs.RemoveRange(petContext.Dogs);
            petContext.SaveChanges();

            petContext.Owners.RemoveRange(petContext.Owners);
            petContext.SaveChanges();
            
            for (int i = 0; i < 10; i++)
            {        
                petContext.Owners.Add(new Owner { Name = "Name" + i, Age = i });
                petContext.SaveChanges();
            }

            for (int i = 0; i < 10; i++)
            {
                petContext.Dogs.Add(new Dog { Name = "Name" + i, Breed = "Breed" + i, Owner = petContext.Owners.FirstOrDefault(owner => owner.Name == "Name" + i) });
                petContext.SaveChanges();
            }

        }
    }
}
