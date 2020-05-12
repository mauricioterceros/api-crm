﻿using System;
using System.Collections.Generic;
using System.Text;
using BackingServices.Exceptions;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace BackingServices
{
    public class ProductBackingService : IProductBackingService
    {
        private readonly IConfiguration _configuration;
        private HttpClient productMS;
        private string msPath ;
        public ProductBackingService(IConfiguration configuration)
        {
            _configuration = configuration;
            productMS = new HttpClient();
            msPath = _configuration.GetSection("Microservices").GetSection("Products").Value;
        }

        public List<ProductBsDTO> GetAllProducts()
        {
            throw new NotImplementedException();
        }

        public string GetAllProductsjson()
        {
            throw new NotImplementedException();
        }

        public async Task<List<ProductBsDTO>> GetAllProduct()
        {
          
            try
            {
                HttpResponseMessage response = await productMS.GetAsync($"{msPath}/product");
                int statusCode = (int)response.StatusCode;
                if (statusCode == 200) // OK
                {
                    // Read ASYNC response from HTTPResponse 
                    String jsonResponse = await response.Content.ReadAsStringAsync();
                    // Deserialize response
                    List<ProductBsDTO> products = JsonConvert.DeserializeObject<List<ProductBsDTO>>(jsonResponse);
                    return products;
                }
                else
                {
                    // something wrong happens!
                    throw new NotImplementedException();
                }
                //List<ProductBsDTO> productss = JsonConvert.DeserializeObject<List<ProductBsDTO>>(File.ReadAllText(_dbPath));
                //return productss;
            }
            catch (Exception ex)
            {
                throw new BackingServiceException("Connection with Products is not working! " + msPath);

            }


        }
        public async Task<ProductBsDTO> AddNew(ProductBsDTO newProductDTO)
        {
         
            try
            {
                String newProduct = JsonConvert.SerializeObject(newProductDTO);
                HttpContent content = new StringContent(newProduct, Encoding.UTF8, "application/json"); 
                HttpResponseMessage response = await productMS.PostAsync($"{msPath}/product",content);
                int statusCode = (int)response.StatusCode;
                if (statusCode == 200) // OK
                {
                    // Read ASYNC response from HTTPResponse 
                    String jsonResponse = await response.Content.ReadAsStringAsync();
                    // Deserialize response
                    ProductBsDTO products = JsonConvert.DeserializeObject<ProductBsDTO>(jsonResponse);
                    return products;
                }
                else
                {
                    // something wrong happens!
                    throw new NotImplementedException();
                }
                //List<ProductBsDTO> productss = JsonConvert.DeserializeObject<List<ProductBsDTO>>(File.ReadAllText(_dbPath));
                //return productss;
            }
            catch (Exception ex)
            {
                throw new BackingServiceException("Connection with Products is not working! " + msPath);

            }


        }

        public async Task<ProductBsDTO> Update(ProductBsDTO upProductDTO, string id)
        {
            try
            {
                String upProduct = JsonConvert.SerializeObject(upProductDTO);
                HttpContent content = new StringContent(upProduct, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await productMS.PutAsync($"{msPath}/product/{id}", content);
                int statusCode = (int)response.StatusCode;
                if (statusCode == 200) // OK
                {
                    // Read ASYNC response from HTTPResponse 
                    String jsonResponse = await response.Content.ReadAsStringAsync();
                    // Deserialize response
                    ProductBsDTO products = JsonConvert.DeserializeObject<ProductBsDTO>(jsonResponse);
                    return products;
                }
                else
                {
                    // something wrong happens!
                    throw new NotImplementedException();
                }
                //List<ProductBsDTO> productss = JsonConvert.DeserializeObject<List<ProductBsDTO>>(File.ReadAllText(_dbPath));
                //return productss;
            }
            catch (Exception ex)
            {
                throw new BackingServiceException("Connection with Products is not working! " + msPath);

            }
        }

        public async void Delete(string id)
        {
            throw new NotImplementedException();
        }

    }
}