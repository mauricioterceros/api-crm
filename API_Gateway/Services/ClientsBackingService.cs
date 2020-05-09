using BackingServices.Exceptions;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Services
{
    public class ClientsBackingService : IClientsBackingService
    {
        private readonly IConfiguration _configuration;
        public ClientsBackingService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public Task<ClientsBsDTO> AddNewClient(ClientsBsDTO newClient)
        {
            throw new NotImplementedException();
        }
        public async Task<List<ClientsBsDTO>> GetClients()
        {
            try
            {
                // Creating HTTP Client
                HttpClient ClientMS = new HttpClient();

                string msPath = _configuration.GetSection("Microservices").GetSection("Clients").Value;

                // Executing an ASYNC HTTP Method could be: Get, Post, Put, Delete
                // In this case is a GET
                // HttpContent content = new 
                // HttpResponseMessage response = await productMS.GetAsync($"{msPath}/pricing-books/PricingBook-001");
                // HttpResponseMessage response = await productMS.GetAsync($"{msPath}/campaigns/Campaigns-001");
                HttpResponseMessage response = await ClientMS.GetAsync($"{msPath}/api/Clients");

                int statusCode = (int)response.StatusCode;
                if (statusCode == 200) // OK
                {
                    // Read ASYNC response from HTTPResponse 
                    String jsonResponse = await response.Content.ReadAsStringAsync();
                    // Deserialize response
                    List<ClientsBsDTO> clients = JsonConvert.DeserializeObject<List<ClientsBsDTO>>(jsonResponse);

                    return clients;
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
        /*public ClientsBsDTO UpdateClient(string code, ClientsBsDTO clientToUpdate);
        {
            throw new NotImplementedException();
        }

        public ClientsBsDTO DeleteClient(string code);
        {
            throw new NotImplementedException();
        }
        public void UpdateSale(string id, bool state)
        {
            throw new NotImplementedException();
        }*/
    }
}
