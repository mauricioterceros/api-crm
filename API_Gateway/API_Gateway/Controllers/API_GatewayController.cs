using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services;
using BackingServices;
using Serilog;

namespace API_Gateway.Controllers
{
    [Route("api-crm/quotes")]
    [ApiController]
    public class API_GatewayController : ControllerBase
    {
        private readonly IQuoteBackingService _quoteDB;
        public API_GatewayController(IQuoteBackingService productBS)
        {
            _quoteDB = productBS;
        }

        [HttpPost]
        [Route("")]
        public QuoteBsDTO Post([FromBody] QuoteBsDTO newQuoteDTO)
        {
            return _quoteDB.AddNewQuote(newQuoteDTO).Result;
        }

        [HttpGet]
        [Route("")]
        public List<QuoteBsDTO> Get()
        {
            return _quoteDB.GetQuoteList().Result;
        }

        [HttpPut]
        [Route("{id}")]
        public string Put(string id, [FromBody] QuoteBsDTO updateQuoteDTO)
        {
            return _quoteDB.UpdateQuote(id, updateQuoteDTO).Result;
        }

        [HttpDelete]
        [Route("{id}")]
        public string Delete(string id)
        {
            return _quoteDB.DeleteByID(id).Result;
        }

        [HttpPut]
        [Route("{id}/sell")]
        public string PutSale(string id)
        {
            return _quoteDB.UpdateSale(id, true).Result;
        }

        [HttpPut]
        [Route("{id}/cancel-sell")]
        public string PutCancelSale(string id)
        {
            return _quoteDB.UpdateSale(id, false).Result;
        }
    }
}
