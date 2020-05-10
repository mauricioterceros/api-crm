using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BackingServices
{
    interface IPricingBookBs

    {
        public  Task<List<PricingBookBsDTO>> GetAll();
        public Task<PricingBookBsDTO> Update(PricingBookBsDTO pricingBookToUpdate, string id);
        public Task<PricingBookBsDTO> AddNew(PricingBookBsDTO newPricingBook);
        public void Delete(string code);
        public void Activate(string id);
        public void DeActivate(string id);

        public Task<PricingBookBsDTO> AddNewProduct(List<ProductPriceBsDTO> newProducts, string id);
        public Task<List<ProductPriceBsDTO>> GetProducts(string id);
        public Task<PricingBookBsDTO> UpdateProduct(List<ProductPriceBsDTO> productToUpdate, string id);
        void DeleteProduct(string code);
        void DeleteProductCode(string code, string productcode);
    }
}
