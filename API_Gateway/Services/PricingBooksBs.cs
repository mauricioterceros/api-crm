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
        private readonly IConfiguration _configuration;

        HttpClient productMS;
        string msPath;
        

        public PricingBooksBs(IConfiguration configuration)
        {
            _configuration = configuration;
            msPath = _configuration.GetSection("Microservices").GetSection("PricingBooks").Value;
            productMS = new HttpClient();
        }

        //Get
        public async Task<List<PricingBookBsDTO>> GetAll()
        {
            try
            {
                //HttpContent content = new HttpContent();
                HttpResponseMessage response = await productMS.GetAsync($"{msPath}/pricing-books");
                int statusCode = (int)response.StatusCode;
                if (statusCode == 200) // OK
                {
                    // Read ASYNC response from HTTPResponse 
                    String jsonResponse = await response.Content.ReadAsStringAsync();
                    Console.WriteLine(jsonResponse);
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
       
        //Post
        public async Task<PricingBookBsDTO> AddNew(PricingBookBsDTO newPricingBook)
        {

            try
            {
                String newPBString = JsonConvert.SerializeObject(newPricingBook);
                HttpContent newPBHTTP = new StringContent(newPBString, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await productMS.PostAsync($"{msPath}/pricing-books", newPBHTTP);

                int statusCode = (int)response.StatusCode;
                if (statusCode == 200) // OK
                {
                    // Read ASYNC response from HTTPResponse 
                    String jsonResponse = await response.Content.ReadAsStringAsync();
                    // Deserialize response
                    PricingBookBsDTO pricing = JsonConvert.DeserializeObject<PricingBookBsDTO>(jsonResponse);

                    return pricing;
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
    
        //Put
        public  async Task<PricingBookBsDTO> Update(PricingBookBsDTO pricingBookToUpdate, string id)
        {
            try
            {
                String newPBString = JsonConvert.SerializeObject(pricingBookToUpdate);
                HttpContent newPBHTTP = new StringContent(newPBString, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await productMS.PutAsync($"{msPath}/pricing-books/{id}", newPBHTTP);

                int statusCode = (int)response.StatusCode;
                if (statusCode == 200) // OK
                {
                    // Read ASYNC response from HTTPResponse 
                    String jsonResponse = await response.Content.ReadAsStringAsync();
                    // Deserialize response
                    PricingBookBsDTO pricing = JsonConvert.DeserializeObject<PricingBookBsDTO>(jsonResponse);

                    return pricing;
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

        
        //delete 1
        public async Task<String> Delete(string code)
        {
            try
            {
                // Creating HTTP Client
                // HttpClient quoteMS = new HttpClient();

                HttpResponseMessage response = await productMS.DeleteAsync($"{msPath}/api/pricing-book/{code}");

                int statusCode = (int)response.StatusCode;
                if (statusCode == 200) // OK
                {
                    // Read ASYNC response from HTTPResponse 
                    String jsonResponse = await response.Content.ReadAsStringAsync();
                    // Deserialize response
                    PricingBookBsDTO quote = JsonConvert.DeserializeObject<PricingBookBsDTO>(jsonResponse);

                    return jsonResponse;
                }
                else
                {
                    // something wrong happens!
                    Console.WriteLine("BS throws the error: " + statusCode);
                    throw new BackingServiceException("BS throws the error: " + statusCode);

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Connection with Products is not working: " + ex.Message);
                throw new BackingServiceException("Connection with Products is not working: " + ex.Message);

            }
        }
        public void Activate(string id)
        {
            throw new NotImplementedException();
        }
        public void DeActivate(string id)
        {
            throw new NotImplementedException();
        }


        public async Task<ProductPriceBsDTO> AddNewProduct(PricingBookBsDTO newProducts, string id)
        {
            try
            {
                String newPBString = JsonConvert.SerializeObject(newProducts);
                HttpContent newPBHTTP = new StringContent(newPBString, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await productMS.PostAsync($"{msPath}/pricing-books", newPBHTTP);

                int statusCode = (int)response.StatusCode;
                if (statusCode == 200) // OK
                {
                    // Read ASYNC response from HTTPResponse 
                    String jsonResponse = await response.Content.ReadAsStringAsync();
                    // Deserialize response
                    ProductPriceBsDTO pricing = JsonConvert.DeserializeObject<ProductPriceBsDTO>(jsonResponse);

                    return pricing;
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

                HttpResponseMessage response = await productMS.GetAsync($"{msPath}/pricing-books/{id}/product-prices");
                //HttpResponseMessage response = await productMS.GetAsync($"{msPath}/pricing-books/PricingBook-1/product-prices");

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
        public async Task<ProductPriceBsDTO> UpdateProduct(PricingBookBsDTO productToUpdate, string id)
        {
            try
            {
                String newPBString = JsonConvert.SerializeObject(productToUpdate);
                HttpContent newPBHTTP = new StringContent(newPBString, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await productMS.PutAsync($"{msPath}/pricing-books/{id}", newPBHTTP);

                int statusCode = (int)response.StatusCode;
                if (statusCode == 200) // OK
                {
                    // Read ASYNC response from HTTPResponse 
                    String jsonResponse = await response.Content.ReadAsStringAsync();
                    // Deserialize response
                    ProductPriceBsDTO pricing = JsonConvert.DeserializeObject<ProductPriceBsDTO>(jsonResponse);

                    return pricing;
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

        //delete 2
        public async Task<string> DeleteProduct(string code)
        {
            try
            {
                // Creating HTTP Client
                // HttpClient quoteMS = new HttpClient();



                HttpResponseMessage response = await productMS.DeleteAsync($"{msPath}/api/product-prices/{code}");

                int statusCode = (int)response.StatusCode;
                if (statusCode == 200) // OK
                {
                    // Read ASYNC response from HTTPResponse 
                    String jsonResponse = await response.Content.ReadAsStringAsync();
                    // Deserialize response
                    PricingBookBsDTO quote = JsonConvert.DeserializeObject<PricingBookBsDTO>(jsonResponse);

                    return jsonResponse;
                }
                else
                {
                    // something wrong happens!
                    Console.WriteLine("BS throws the error: " + statusCode);
                    throw new BackingServiceException("BS throws the error: " + statusCode);

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Connection with Products is not working: " + ex.Message);
                throw new BackingServiceException("Connection with Products is not working: " + ex.Message);

            }
        }

        
        //delete 3
        public async Task<String> DeleteProductCode(string id, string code)
        {
            try
            {
                // Creating HTTP Client
                // HttpClient quoteMS = new HttpClient();

                

                HttpResponseMessage response = await productMS.DeleteAsync($"{msPath}/api/product-prices/{id}/{code}");

                int statusCode = (int)response.StatusCode;
                if (statusCode == 200) // OK
                {
                    // Read ASYNC response from HTTPResponse 
                    String jsonResponse = await response.Content.ReadAsStringAsync();
                    // Deserialize response
                    ProductPriceBsDTO quote = JsonConvert.DeserializeObject<ProductPriceBsDTO>(jsonResponse);

                    return jsonResponse;
                }
                else
                {
                    // something wrong happens!
                    Console.WriteLine("BS throws the error: " + statusCode);
                    throw new BackingServiceException("BS throws the error: " + statusCode);

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Connection with Products is not working: " + ex.Message);
                throw new BackingServiceException("Connection with Products is not working: " + ex.Message);

            }
        }
    }
}
