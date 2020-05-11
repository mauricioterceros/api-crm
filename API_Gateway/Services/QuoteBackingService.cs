using BackingServices.Exceptions;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class QuoteBackingService : IQuoteBackingService
    {
        private readonly IConfiguration _configuration;
        private string msPath;
        HttpClient quoteMS;
        public QuoteBackingService(IConfiguration configuration)
        {
            _configuration = configuration;
            msPath = _configuration.GetSection("Microservices").GetSection("Quoting").Value;
            quoteMS = new HttpClient();
        }
        public async Task<QuoteBsDTO> AddNewQuote(QuoteBsDTO newQuote)
        {
            try
            {
                // Creating HTTP Client
                // HttpClient quoteMS = new HttpClient();

                // string msPath = _configuration.GetSection("Microservices").GetSection("Quoting").Value;

                String newQuoteString = JsonConvert.SerializeObject(newQuote);
                HttpContent newQuoteHTTP = new StringContent(newQuoteString, Encoding.UTF8, "application/json");
                
                HttpResponseMessage response = await quoteMS.PostAsync($"{msPath}/api/quotes",newQuoteHTTP);

                int statusCode = (int)response.StatusCode;
                if (statusCode == 200) // OK
                {
                    // Read ASYNC response from HTTPResponse 
                    String jsonResponse = await response.Content.ReadAsStringAsync();
                    // Deserialize response
                    QuoteBsDTO quote = JsonConvert.DeserializeObject<QuoteBsDTO>(jsonResponse);

                    return quote;
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
        public async Task<List<QuoteBsDTO>> GetQuoteList()
        {
            try
            {
                // Creating HTTP Client
                // HttpClient quoteMS = new HttpClient();

                // string msPath = _configuration.GetSection("Microservices").GetSection("Quoting").Value;

                // Executing an ASYNC HTTP Method could be: Get, Post, Put, Delete
                // In this case is a GET
                // HttpContent content = new 
                // HttpResponseMessage response = await productMS.GetAsync($"{msPath}/pricing-books/PricingBook-001");
                // HttpResponseMessage response = await productMS.GetAsync($"{msPath}/campaigns/Campaigns-001");
                HttpResponseMessage response = await quoteMS.GetAsync($"{msPath}/api/quotes");

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
                    Console.WriteLine("BS throws the error: " + statusCode);
                    throw new BackingServiceException("BS throws the error: " + statusCode);
                }
            }
            catch (Exception ex)
            {
                //Console.WriteLine("Connection with Products is not working: " + msPath);
                throw new BackingServiceException("Connection with Products is not working: " + ex.Message);
            }          
        }
        public async Task<string> UpdateQuote(string id, QuoteBsDTO updatedQuote)
        {
            try
            {
                // Creating HTTP Client
                // HttpClient quoteMS = new HttpClient();

                // string msPath = _configuration.GetSection("Microservices").GetSection("Quoting").Value;

                String updateQuoteString = JsonConvert.SerializeObject(updatedQuote);
                HttpContent updateQuoteHTTP = new StringContent(updateQuoteString, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await quoteMS.PutAsync($"{msPath}/api/quotes/{id}", updateQuoteHTTP);

                int statusCode = (int)response.StatusCode;
                if (statusCode == 200) // OK
                {
                    // Read ASYNC response from HTTPResponse 
                    String jsonResponse = await response.Content.ReadAsStringAsync();
                    // Deserialize response
                    //QuoteBsDTO quote = JsonConvert.DeserializeObject<QuoteBsDTO>(jsonResponse);

                    //return quote;
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

        public async Task<string> DeleteByID(string id)
        {
            try
            {
                // Creating HTTP Client
                // HttpClient quoteMS = new HttpClient();

                // string msPath = _configuration.GetSection("Microservices").GetSection("Quoting").Value;

                //HttpContent updateQuoteHTTP = new StringContent(updateQuoteString, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await quoteMS.DeleteAsync($"{msPath}/api/quotes/{id}");

                int statusCode = (int)response.StatusCode;
                if (statusCode == 200) // OK
                {
                    // Read ASYNC response from HTTPResponse 
                    String jsonResponse = await response.Content.ReadAsStringAsync();
                    // Deserialize response
                    QuoteBsDTO quote = JsonConvert.DeserializeObject<QuoteBsDTO>(jsonResponse);

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
        public async Task<string> UpdateSale(string id, bool state)
        {
            try
            {
                // Creating HTTP Client
                // HttpClient quoteMS = new HttpClient();

                // string msPath = _configuration.GetSection("Microservices").GetSection("Quoting").Value;

                //HttpContent updateQuoteHTTP = new StringContent(updateQuoteString, Encoding.UTF8, "application/json");

                String value = state ? "sell" : "cancel-sell";
                HttpContent idHTTP = new StringContent(id);
                HttpResponseMessage response = await quoteMS.PutAsync($"{msPath}/api/quotes/{id}/{value}", idHTTP);

                int statusCode = (int)response.StatusCode;
                if (statusCode == 200) // OK
                {
                    // Read ASYNC response from HTTPResponse 
                    String jsonResponse = await response.Content.ReadAsStringAsync();
                    // Deserialize response
                    QuoteBsDTO quote = JsonConvert.DeserializeObject<QuoteBsDTO>(jsonResponse);

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
