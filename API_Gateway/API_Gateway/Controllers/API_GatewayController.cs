using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using API_Gateway;
using Services;

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

    [Route("api-crm/PricingBooks")]
    [ApiController]
    public class PricingBook : ControllerBase
    {
        private readonly IPricingBookBs _pricingDB;
        public PricingBook(IPricingBookBs pricingBook)
        {
            _pricingDB = pricingBook;
        }
        [HttpGet]
        [Route("")]
        public List<PricingBookBsDTO> GetAll()
        {
            return _pricingDB.GetAll().Result;
        }

        [HttpGet]
        [Route("{id}")]
        public IEnumerable<ProductPriceBsDTO> GetProduct(string id)
        {
            return _pricingDB.GetProducts(id).Result;
        }



    }


}
