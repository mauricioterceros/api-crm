using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services;
using BackingServices;

namespace API_Gateway.Controllers
{
    [Route("api-crm/")]
    [ApiController]
    public class API_ProductGatewayController : ControllerBase
    {
        private readonly IProductBackingService _productDB;
        public API_ProductGatewayController(IProductBackingService productBS)
        {
            _productDB = productBS;
        }

        [HttpGet]
        [Route("product")]
        public List<ProductBsDTO> GetAll()
        {
            return _productDB.GetAllProduct().Result;
        }

        [HttpPost]
        [Route("product")]
        public ProductBsDTO PostProduct([FromBody] ProductBsDTO newproductDTO)
        {
            return _productDB.AddNew(newproductDTO).Result;
        }

        [HttpPut]
        [Route("product/{id}")]
        public ProductBsDTO PutProduct(string id, [FromBody] ProductBsDTO updateProduct)
        {


            return _productDB.Update(updateProduct, id).Result;

        }

        [HttpDelete]
        [Route("{id}")]
        public bool Delete(string id)
        {
            return _productDB.Delete(id).Result;
        }
    }


}
