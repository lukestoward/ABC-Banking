using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using ABC_Banking.Services.AccountServices.Models;

namespace ABC_Banking.Services.AccountServices.Controllers
{
    public class StatementController : ApiController
    {
        /// <summary>
        /// Mini statment returns simplified version of the transaction list.
        /// Useful when printing slip via ATM.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IHttpActionResult> GetMiniStatement([FromUri]StatementRequest model)
        {
            StatementServices services = new StatementServices();
            List<MiniStatementTransaction> statement = await services.GetMiniStatement(model);

            if (statement == null)
                return BadRequest("Unable to generate mini statement");

            return Ok(statement);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IHttpActionResult> GetFullStatement([FromUri]StatementRequest model)
        {
            StatementServices services = new StatementServices();
            List<FullStatementTransaction> statement = await services.GetFullStatement(model);

            if (statement == null)
                return BadRequest("Unable to generate mini statement");

            return Ok(statement);
        }
    }
}
