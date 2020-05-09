using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace API_Gateway.Controllers
{
    [Route("api-crm/clients")]
    [ApiController]
    public class Clients : ControllerBase
    {
        private readonly IClientsBackingService _clientsDB;

        public Clients(IClientsBackingService productBS1)
        {
            _clientsDB = productBS1;
        }

        [HttpGet]
        [Route("")]
        public List<ClientsBsDTO> Get()
        {
            return _clientsDB.GetClients().Result;
        }

        ///
    }

    [Route("api-crm/quotes")]
    [ApiController]
    public class API_GatewayController : ControllerBase
    {
        private readonly IQuoteBackingService _quoteDB;

        public API_GatewayController(IQuoteBackingService productBS)
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

        ///
        
    }
}
