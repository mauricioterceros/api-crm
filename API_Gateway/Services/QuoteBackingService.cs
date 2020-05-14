using BackingServices.Exceptions;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Serilog;
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
                    Log.Logger.Information("Succesfull");
                    return quote;
                }
                else
                {
                    // something wrong happens!
                    Log.Logger.Information("BS throws the error: " + statusCode);
                    throw new BackingServiceException("BS throws the error: " + statusCode);
                }
            }
            catch (Exception ex)
            {
                Log.Logger.Information("Connection with Quote Backing Service  is not working: " + ex.Message);
                throw new BackingServiceException("Connection with Quote Backing Service  is not working: " + ex.Message);
            }
        }
        public async Task<List<QuoteBsDTO>> GetQuoteList()
        {
            try
            {
                // Creating HTTP Client
                // HttpClient quoteMS = new HttpClient();

                // string msPath = _configuration.GetSection("Microservices").GetSection("Quoting").Value;

                HttpResponseMessage response = await quoteMS.GetAsync($"{msPath}/api/quotes");

                int statusCode = (int)response.StatusCode;
                if (statusCode == 200) // OK
                {
                    // Read ASYNC response from HTTPResponse 
                    String jsonResponse = await response.Content.ReadAsStringAsync();
                    // Deserialize response
                    List<QuoteBsDTO> quotes = JsonConvert.DeserializeObject<List<QuoteBsDTO>>(jsonResponse);

                    Log.Logger.Information("Succesfull");
                    return quotes;
                }
                else
                {
                    // something wrong happens!
                    Log.Logger.Information("BS throws the error: " + statusCode);
                    throw new BackingServiceException("BS throws the error: " + statusCode);
                }
            }
            catch (Exception ex)
            {
                Log.Logger.Information("Connection with Quote Backing Service  is not working: " + ex.Message);
                throw new BackingServiceException("Connection with Quote Backing Service  is not working: " + ex.Message);
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
                    Log.Logger.Information("Succesfull");
                    return jsonResponse;
                }
                else
                {
                    // something wrong happens!
                    Log.Logger.Information("BS throws the error: " + statusCode);
                    throw new BackingServiceException("BS throws the error: " + statusCode);
                }
            }
            catch (Exception ex)
            {
                Log.Logger.Information("Connection with Quote Backing Service  is not working: " + ex.Message);
                throw new BackingServiceException("Connection with Quote Backing Service  is not working: " + ex.Message);
            }
        }

        public async Task<string> DeleteByID(string id)
        {
            try
            {
                // Creating HTTP Client
                // HttpClient quoteMS = new HttpClient();

                // string msPath = _configuration.GetSection("Microservices").GetSection("Quoting").Value;

                HttpResponseMessage response = await quoteMS.DeleteAsync($"{msPath}/api/quotes/{id}");

                int statusCode = (int)response.StatusCode;
                if (statusCode == 200) // OK
                {
                    // Read ASYNC response from HTTPResponse 
                    String jsonResponse = await response.Content.ReadAsStringAsync();
                    // Deserialize response
                    //  QuoteBsDTO quote = JsonConvert.DeserializeObject<QuoteBsDTO>(jsonResponse);

                    Log.Logger.Information("Succesfull");
                    return jsonResponse;
                }
                else
                {
                    // something wrong happens!
                    Log.Logger.Information("BS throws the error: " + statusCode);
                    throw new BackingServiceException("BS throws the error: " + statusCode);
                }
            }
            catch (Exception ex)
            {
                Log.Logger.Information("Connection with Quote Backing Service  is not working: " + ex.Message);
                throw new BackingServiceException("Connection with Quote Backing Service  is not working: " + ex.Message);
            }
        }
        public async Task<string> UpdateSale(string id, bool state)
        {
            try
            {
                // Creating HTTP Client
                // HttpClient quoteMS = new HttpClient();

                // string msPath = _configuration.GetSection("Microservices").GetSection("Quoting").Value;

                String value = state ? "sell" : "cancel-sell";
                HttpContent idHTTP = new StringContent(id);
                HttpResponseMessage response = await quoteMS.PutAsync($"{msPath}/api/quotes/{id}/{value}", idHTTP);

                int statusCode = (int)response.StatusCode;
                if (statusCode == 200) // OK
                {
                    // Read ASYNC response from HTTPResponse 
                    String jsonResponse = await response.Content.ReadAsStringAsync();
                    // Deserialize response
                    //QuoteBsDTO quote = JsonConvert.DeserializeObject<QuoteBsDTO>(jsonResponse);

                    
                    Log.Logger.Information("Succesfull");
                    return jsonResponse;
                }
                else
                {
                    // something wrong happens!
                    Log.Logger.Information("BS throws the error: " + statusCode);
                    throw new BackingServiceException("BS throws the error: " + statusCode);
                }
            }
            catch (Exception ex)
            {
                Log.Logger.Information("Connection with Quote Backing Service  is not working: " + ex.Message);
                throw new BackingServiceException("Connection with Quote Backing Service  is not working: " + ex.Message);
            }
        }
    }
}
