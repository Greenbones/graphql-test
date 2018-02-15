using System;
using System.Linq;
using System.Threading.Tasks;
using GraphQL;
using GraphQL.Types;
using Microsoft.AspNetCore.Mvc;

namespace MDT.GraphQLData.Controllers
{

    [Route("api/graphql")]
    public class GraphQLController : Controller
    {
        private readonly IDocumentExecuter _documentExecuter;
        private readonly ISchema _schema;

        public GraphQLController(IDocumentExecuter documentExecuter, ISchema schema) {
            _documentExecuter = documentExecuter;
            _schema = schema;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] String query)
        {
            ExecutionOptions options = new ExecutionOptions { Schema = _schema, Query = query };
            ExecutionResult result = await _documentExecuter.ExecuteAsync(options).ConfigureAwait(false);

            return Ok(result);
        }
    }
}