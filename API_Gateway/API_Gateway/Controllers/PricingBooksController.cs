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
    

    [Route("/api-crm/pricing-books")]
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
        [HttpGet]
        [Route("active")]
        public PricingBookBsDTO GetActivePricingBook()
        {
            return _pricingDB.GetActivePricingBook().Result;
        } 



        [HttpPut]
        [Route("{id}")]
        public PricingBookBsDTO PutPricingBooks([FromBody]PricingBookBsDTO pricingBookToUpdate, string id)
        {
            return _pricingDB.Update(pricingBookToUpdate, id).Result;
        }
        [HttpDelete]
        [Route("{id}")]
        public Task<bool> DeletePB(string id)
        {
            return _pricingDB.DeleteListProduct(id);
        }
        [HttpPost]
        [Route("{id}/activate")]
        public string ActivatePost(string id)
        {
            return _pricingDB.Activate(id).Result;
       
        }

        [HttpPost]
        [Route("{id}/deactivate")]
        public string DeActivatePost(string id)
        {
            return _pricingDB.DeActivate(id).Result;
           
        }

        /******************IPricingBo ***********************/
        [HttpPost]
        [Route("{id}/product-prices")]
        public PricingBookBsDTO Post([FromBody]List<ProductPriceBsDTO> newProductDTO, string id)
        {
            return _pricingDB.AddNewProduct(newProductDTO, id).Result;
        }
        
        [HttpGet]
        [Route("{id}/product-prices")]
        public IEnumerable<ProductPriceBsDTO> GetProduct(string id)
        {
            return _pricingDB.GetProducts(id).Result;
        }

        
        [HttpPut]
        [Route("{id}/product-prices")]
        public PricingBookBsDTO PutProductPrice([FromBody]List<ProductPriceBsDTO> productPrice, string id)
        {
            return _pricingDB.UpdateProduct(productPrice, id).Result;
        }
        
       
        [HttpDelete]
        [Route("{id}/product-prices")]
        public string DeletePricing(string id)
        {
            return _pricingDB.DeleteProduct(id).Result;
        }

        [HttpDelete]
        [Route("{id}/product-prices/{code}")]
        public void DeleteByCode(string id, string code)
        {
            _pricingDB.DeleteProductCode(id, code);
        }
    }
    

}
