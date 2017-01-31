using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
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
        public IHttpActionResult GetMiniStatement(StatementRequest model)
        {
            return Ok();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetFullStatement(StatementRequest model)
        {
            return Ok();
        }
    }
}
