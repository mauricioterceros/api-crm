using System.Collections.Generic;
using System.Threading.Tasks;

namespace BackingServices
{
    public interface IProductBackingService
    {
        public Task<List<ProductBsDTO>> GetAllProduct();
        public Task<ProductBsDTO> AddNew(ProductBsDTO newproductDTO);
        public Task<ProductBsDTO> Update(ProductBsDTO newproductDTO, string id);
        public Task<bool> Delete(string id);
    }
}
