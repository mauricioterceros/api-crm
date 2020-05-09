using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace API_Gateway.Controllers
{
    public class API_GatewayController : ControllerBase
    {
        [Route("api-crm/quotes")]
        [ApiController]
        public class QuoteController : ControllerBase
        {
            private readonly IQuoteBackingService _quoteDB;
            public QuoteController(IQuoteBackingService productBS)
            {
                _quoteDB = productBS;
            }

            [HttpGet]
            [Route("")]
            public List<QuoteBsDTO> Get()
            {
                return _quoteDB.GetQuoteList().Result;
            }

            [HttpGet]
            [Route("{id}")]
            public IEnumerable<string> GetById()
            {
                throw new NotImplementedException();
            }
        }
    }
}