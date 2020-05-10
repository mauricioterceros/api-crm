using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using BackingServices.Exceptions;

namespace Services
{
    public class PricingBooksBs : IPricingBookBs

    {
        HttpClient productMS = new HttpClient();
        private readonly IConfiguration _configuration;
        public PricingBooksBs(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        

        public void Activate(string id)
        {
            throw new NotImplementedException();
        }

        public Task<PricingBookBsDTO> AddNew(PricingBookBsDTO newPricingBook)
        {
            throw new NotImplementedException();
        }

        public Task<PricingBookBsDTO> AddNewProduct(List<ProductPriceBsDTO> newProducts, string id)
        {
            throw new NotImplementedException();
        }

        public void DeActivate(string id)
        {
            throw new NotImplementedException();
        }

        public void Delete(string code)
        {
            throw new NotImplementedException();
        }

        public void DeleteProduct(string code)
        {
            throw new NotImplementedException();
        }

        public void DeleteProductCode(string code, string productcode)
        {
            throw new NotImplementedException();
        }

        public async Task<List<PricingBookBsDTO>> GetAll()
        {
            try
            {

                string msPath = _configuration.GetSection("Microservices").GetSection("PricingBooks").Value;

                HttpResponseMessage response = await productMS.GetAsync($"{msPath}/pricing-books");
                int statusCode = (int)response.StatusCode;
                if (statusCode == 200) // OK
                {
                    // Read ASYNC response from HTTPResponse 
                    String jsonResponse = await response.Content.ReadAsStringAsync();
                    // Deserialize response
                    List<PricingBookBsDTO> pricings = JsonConvert.DeserializeObject<List<PricingBookBsDTO>>(jsonResponse);
                    return pricings;
                }
                else
                {
                    // something wrong happens!
                    throw new BackingServiceException("BS throws the error: " + statusCode);
                }
            }
            catch (Exception ex)
            {
                throw new BackingServiceException("Connection with Products is not working: " + ex.Message);
            }
        }

        public async Task<List<ProductPriceBsDTO>> GetProducts(string id)
        {
            try
            {

                string msPath = _configuration.GetSection("Microservices").GetSection("PricingBooks").Value;

                HttpResponseMessage response = await productMS.GetAsync($"{msPath}/getProducts");
                int statusCode = (int)response.StatusCode;
                if (statusCode == 200) // OK
                {
                    // Read ASYNC response from HTTPResponse 
                    String jsonResponse = await response.Content.ReadAsStringAsync();
                    // Deserialize response
                    List<ProductPriceBsDTO> productPrice = JsonConvert.DeserializeObject<List<ProductPriceBsDTO>>(jsonResponse);
                    return productPrice;
                }
                else
                {
                    // something wrong happens!
                    throw new BackingServiceException("BS throws the error: " + statusCode);
                }
            }
            catch (Exception ex)
            {
                throw new BackingServiceException("Connection with Products is not working: " + ex.Message);
            }
        }

        public Task<PricingBookBsDTO> Update(PricingBookBsDTO pricingBookToUpdate, string id)
        {
            throw new NotImplementedException();
        }

        public Task<PricingBookBsDTO> UpdateProduct(List<ProductPriceBsDTO> productToUpdate, string id)
        {
            throw new NotImplementedException();
        }
    }
}
