using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface IPricingBookBs

    {
        //IPriceLogic 
        public  Task<List<PricingBookBsDTO>> GetAll();//
        public Task<PricingBookBsDTO> GetActivePricingBook();
        public Task<PricingBookBsDTO> Update(PricingBookBsDTO pricingBookToUpdate, string id);//
        public Task<PricingBookBsDTO> AddNew(PricingBookBsDTO newPricingBook);//
        public Task<bool> DeleteListProduct(string code);
        public Task<string> Activate(string id);
        public Task<string> DeActivate(string id);

        //IPricingBook
        public Task<PricingBookBsDTO> AddNewProduct(List<ProductPriceBsDTO> newProducts, string id);//
        public Task<List<ProductPriceBsDTO>> GetProducts(string id);//
        public Task<PricingBookBsDTO> UpdateProduct(List<ProductPriceBsDTO> productToUpdate, string id);
        public Task<string> DeleteProduct(string code);
        public Task<string> DeleteProductCode(string code, string productcode);
    }
}
