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
    public class ClientsBackingService : IClientsBackingService
    {
        private readonly IConfiguration _configuration;
        public ClientsBackingService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<ClientsBsDTO> AddNewClient(ClientsBsDTO newClient)
        {
            try
            {
                HttpClient ClientMS = new HttpClient();

                string msPath = _configuration.GetSection("Microservices").GetSection("Clients").Value;
                String newClientString = JsonConvert.SerializeObject(newClient);
                StringContent data = new StringContent(newClientString, Encoding.UTF8, "application/json");
                //StringContent data2 = new StringContent("hola");
                HttpResponseMessage response = await ClientMS.PostAsync($"{msPath}/api/clients", data);
                int statusCode = (int)response.StatusCode;
                if (statusCode == 200) // OK
                {
                    // Read ASYNC response from HTTPResponse 
                    String jsonResponse = await response.Content.ReadAsStringAsync();
                    // Deserialize response
                    ClientsBsDTO AddedClient = JsonConvert.DeserializeObject<ClientsBsDTO>(jsonResponse);
                    return AddedClient;
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
            throw new NotImplementedException();
        }
        public async Task<List<RankingDTO>> GetRankings()
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
                HttpResponseMessage response = await ClientMS.GetAsync($"{msPath}/api/rankings");

                int statusCode = (int)response.StatusCode;
                if (statusCode == 200) // OK
                {
                    // Read ASYNC response from HTTPResponse 
                    String jsonResponse = await response.Content.ReadAsStringAsync();
                    // Deserialize response
                    List<RankingDTO> ranks = JsonConvert.DeserializeObject<List<RankingDTO>>(jsonResponse);

                    return ranks;
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
                HttpResponseMessage response = await ClientMS.GetAsync($"{msPath}/api/clients");

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
        public async Task<ClientsBsDTO> UpdateClient(string code, ClientsBsDTO clientToUpdate)
        {
            try
            {
                HttpClient ClientMS = new HttpClient();

                string msPath = _configuration.GetSection("Microservices").GetSection("Clients").Value;
                String newClientString = JsonConvert.SerializeObject(clientToUpdate);
                StringContent data = new StringContent(newClientString, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await ClientMS.PutAsync($"{msPath}/api/clients/{code}", data);

                int statusCode = (int)response.StatusCode;
                if (statusCode == 200) // OK
                {
                    String jsonResponse = await response.Content.ReadAsStringAsync();
                    ClientsBsDTO UpdatedClient = JsonConvert.DeserializeObject<ClientsBsDTO>(jsonResponse);
                    return UpdatedClient;
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
            throw new NotImplementedException();
        }

        public async Task<bool> DeleteClient(string code)
        {
            try
            {
                HttpClient ClientMS = new HttpClient();

                string msPath = _configuration.GetSection("Microservices").GetSection("Clients").Value;
                StringContent data = new StringContent(code, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await ClientMS.DeleteAsync($"{msPath}/api/clients/{code}");

                int statusCode = (int)response.StatusCode;
                if (statusCode == 200) // OK
                {
                    String jsonResponse = await response.Content.ReadAsStringAsync();
                    bool deletedClient = JsonConvert.DeserializeObject<bool>(jsonResponse);

                    if (deletedClient == true)
                    {
                        return deletedClient;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    // something wrong happens!
                    Console.WriteLine("BS throws the error: " + statusCode);
                    throw new BackingServiceException("BS throws the error: " + statusCode + " Entro al Else ");
                }
            }
            catch (Exception ex)
            {
                //Console.WriteLine("Connection with Products is not working: " + msPath);
                throw new BackingServiceException("Connection with Products is not working: " + ex.Message);
            }
            throw new NotImplementedException();
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
