using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface IPricingBookBs

    {
        public  Task<List<PricingBookBsDTO>> GetAll();//
        public Task<PricingBookBsDTO> Update(PricingBookBsDTO pricingBookToUpdate, string id);//
        public Task<PricingBookBsDTO> AddNew(PricingBookBsDTO newPricingBook);//
        public Task<String> Delete(string code);//
        public void Activate(string id);
        public void DeActivate(string id);

        public Task<ProductPriceBsDTO> AddNewProduct(PricingBookBsDTO newProducts, string id);//
        public Task<List<ProductPriceBsDTO>> GetProducts(string id);//
        public Task<ProductPriceBsDTO> UpdateProduct(PricingBookBsDTO productToUpdate, string id);//
        public Task <String> DeleteProduct(string code);//
        public Task<String> DeleteProductCode(string id, string code);//
        
    }
}
