using BackingServices.Exceptions;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Services
{
    public class QuoteBackingService : IQuoteBackingService
    {
        private readonly IConfiguration _configuration;
        public QuoteBackingService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        Task<QuoteBsDTO> IQuoteBackingService.AddNewQuote(QuoteBsDTO newQuote)
        {
            throw new NotImplementedException();
        }
        public async Task<List<QuoteBsDTO>> GetQuoteList()
        {
            try
            {
                // Creating HTTP Client
                HttpClient productMS = new HttpClient();

                string msPath = _configuration.GetSection("Microservices").GetSection("Quoting").Value;

                // Executing an ASYNC HTTP Method could be: Get, Post, Put, Delete
                // In this case is a GET
                // HttpContent content = new 
                // HttpResponseMessage response = await productMS.GetAsync($"{msPath}/pricing-books/PricingBook-001");
                // HttpResponseMessage response = await productMS.GetAsync($"{msPath}/campaigns/Campaigns-001");
                HttpResponseMessage response = await productMS.GetAsync($"{msPath}/quotes");

                int statusCode = (int)response.StatusCode;
                if (statusCode == 200) // OK
                {
                    // Read ASYNC response from HTTPResponse 
                    String jsonResponse = await response.Content.ReadAsStringAsync();
                    // Deserialize response
                    List<QuoteBsDTO> quotes = JsonConvert.DeserializeObject<List<QuoteBsDTO>>(jsonResponse);

                    return quotes;
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
        void IQuoteBackingService.UpdateQuote(string id, QuoteBsDTO updatedQuote)
        {
            throw new NotImplementedException();
        }

        void IQuoteBackingService.DeleteByID(string id)
        {
            throw new NotImplementedException();
        }
        void IQuoteBackingService.UpdateSale(string id, bool state)
        {
            throw new NotImplementedException();
        }
    }
}
