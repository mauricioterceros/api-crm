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
    [Route("/api-crm/product")]
    [ApiController]
    public class Products : ControllerBase
    {
        private readonly IProductBackingService _productDB;
        public Products(IProductBackingService productBS)
        {
            _productDB = productBS;
        }

        [HttpGet]
        [Route("")]
        public List<ProductBsDTO> GetAll()
        {
            Log.Logger.Information("Client trying to Products list");
            return _productDB.GetAllProduct().Result;
        }

        [HttpPost]
        [Route("")]
        public ProductBsDTO PostProduct([FromBody] ProductBsDTO newproductDTO)
        {
            Log.Logger.Information("Client trying to Create a new Product: "+newproductDTO.Code);
            return _productDB.AddNew(newproductDTO).Result;
        }

        [HttpPut]
        [Route("{id}")]
        public ProductBsDTO PutProduct(string id, [FromBody] ProductBsDTO updateProduct)
        {

            Log.Logger.Information("Client trying to Update Product: " + updateProduct.Code);
            return _productDB.Update(updateProduct, id).Result;

        }

        [HttpDelete]
        [Route("{id}")]
        public bool Delete(string id)
        {
            Log.Logger.Information("Client trying to Delete Product: " + id);
            return _productDB.Delete(id).Result;
        }
    }


}
