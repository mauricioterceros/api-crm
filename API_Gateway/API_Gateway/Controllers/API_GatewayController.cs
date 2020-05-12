﻿using System;
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

    [Route("api-crm/Pricing-Books")]
    [ApiController]
    public class PricingBook : ControllerBase
    {
        private readonly IPricingBookBs _pricingDB;
        public PricingBook(IPricingBookBs pricingBook)
        {
            _pricingDB = pricingBook;
        }
        /*************IPriceBook****************/
        [HttpGet]
        [Route("")]
        public List<PricingBookBsDTO> GetAll()
        {
            return _pricingDB.GetAll().Result;
        }
        [HttpPost]
        [Route("")]
        public PricingBookBsDTO PostPricingBooks([FromBody]PricingBookBsDTO NewPB)
        {
            return _pricingDB.AddNew(NewPB).Result;
        }
        [HttpDelete]
        [Route("{id}")]
        public Task<bool> Delete(string id)
        {
            return _pricingDB.DeleteListProduct(id);
        }

        [HttpPut]
        [Route("{id}")]
        public PricingBookBsDTO PutPricingBooks([FromBody]PricingBookBsDTO pricingBookToUpdate, string id)
        {
            return _pricingDB.Update(pricingBookToUpdate, id).Result;
        }

        [HttpPost]
        [Route("pricing-books/{id}/activate")]
        public string ActivatePost(string id)
        {
            return _pricingDB.Activate(id).Result;
       
        }

        [HttpPost]
        [Route("pricing-books/{id}/deactivate")]
        public string DeActivatePost(string id)
        {
            return _pricingDB.DeActivate(id).Result;
           
        }
        /******************IPricingBo ***********************/
        [HttpPost]
        [Route("pricing-books/{id}/product-prices")]
        public PricingBookBsDTO Post([FromBody]List<ProductPriceBsDTO> newProductDTO, string id)
        {
            return _pricingDB.AddNewProduct(newProductDTO, id).Result;
        }
        
        [HttpGet]
        [Route("{id}")]
        public IEnumerable<ProductPriceBsDTO> GetProduct(string id)
        {
            return _pricingDB.GetProducts(id).Result;
        }

      /*  
        [HttpPut]
        [Route("{id}")]
        public PricingBookBsDTO PutProductPrice([FromBody]List<ProductPriceBsDTO> productPrice, string id)
        {
            return _pricingDB.UpdateProduct(productPrice, id).Result;
        }
        */
        
        //Delete pricings
        [HttpDelete]
        [Route("{id}")]
        public string DeletePricing(string code)
        {
           return _pricingDB.DeleteProduct(code).Result;
        }
        
        //Delete ID
        [HttpDelete]
        [Route("{id}/product-prices/{code}")]
        public void Delete(string id, string code)
        {
            _pricingDB.DeleteProductCode(id, code);
        }

    
    }
    

}
