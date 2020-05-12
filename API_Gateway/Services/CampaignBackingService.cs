using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using BackingServices.Exceptions;

namespace BackingServices
{
    public class CampaignBackingService : ICampaignBackingService
    {
        private readonly IConfiguration _configuration;
        
        public CampaignBackingService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        //GET
                    //preguntar, esta parte tiene que exactamente al controller que estamos implementando
                    //el campaing utiliza un IEnumerable y no un list
        public async Task<IEnumerable<CampaignBsDTO>> GetAllCampaign()
        {
            try
            {
                HttpClient campaignMS = new HttpClient();

                string msPath = _configuration.GetSection("Microservices").GetSection("Campaigns").Value;
                HttpResponseMessage response = await campaignMS.GetAsync($"{msPath}/api/campaigns");

                int statusCode = (int)response.StatusCode;
                if (statusCode == 200)
                {
                    String jsonResponse = await response.Content.ReadAsStringAsync();
                    IEnumerable<CampaignBsDTO> campaigns = JsonConvert.DeserializeObject<IEnumerable<CampaignBsDTO>>(jsonResponse);

                    return campaigns;
                }
                else
                {
                    throw new BackingServiceException("BS throws the error: " + statusCode);
                }
            }
            catch (Exception ex)
            {
              throw new BackingServiceException("Connection with Products is not working: " + ex.Message);
            }
        }

        //POST
        public async Task<CampaignBsDTO> AddNewCampaign(CampaignBsDTO newCampaign)
        {
            try
            {
                HttpClient campaignMS = new HttpClient();
                String newCampignString = JsonConvert.SerializeObject(newCampaign);
                HttpContent newCampaignHTTP = new StringContent(newCampignString, Encoding.UTF8, "application/json");

                string msPath = _configuration.GetSection("Microservices").GetSection("Campaigns").Value;
                HttpResponseMessage response = await campaignMS.PostAsync($"{msPath}/api/campaigns", newCampaignHTTP);

                int statusCode = (int)response.StatusCode;
                if (statusCode == 200)
                {
                    String jsonResponse = await response.Content.ReadAsStringAsync();
                    
                    CampaignBsDTO campaigns = JsonConvert.DeserializeObject<CampaignBsDTO>(jsonResponse);

                    return campaigns;
                }
                else
                {
                    throw new BackingServiceException("BS throws the error: " + statusCode);
                }
            }
            catch (Exception ex)
            {
                throw new BackingServiceException("Connection with Products is not working: " + ex.Message);
            }
        }

        //PUT
        public async Task<CampaignBsDTO> UpdateCampaing(CampaignBsDTO campaign, string id)
        {
            try
            {
                HttpClient campaignMS = new HttpClient();

                string msPath = _configuration.GetSection("Microservices").GetSection("Campaigns").Value;
                HttpResponseMessage response = await campaignMS.GetAsync($"{msPath}/campaigns/{id}");

                int statusCode = (int)response.StatusCode;
                if (statusCode == 200)
                {
                    String jsonResponse = await response.Content.ReadAsStringAsync();
                    CampaignBsDTO campaigns = JsonConvert.DeserializeObject<CampaignBsDTO>(jsonResponse);

                    return campaigns;
                }
                else
                {
                    throw new BackingServiceException("BS throws the error: " + statusCode);
                }
            }
            catch (Exception ex)
            {
                throw new BackingServiceException("Connection with Products is not working: " + ex.Message);
            }

        }

        //POST-ACTIVATE
        public async Task<CampaignBsDTO> ActivateCampaign(string id)
        {
            try
            {
                HttpClient campaignMS = new HttpClient();

                string msPath = _configuration.GetSection("Microservices").GetSection("Campaigns").Value;
                HttpResponseMessage response = await campaignMS.GetAsync($"{msPath}/campaigns/{id}/activate");

                int statusCode = (int)response.StatusCode;
                if (statusCode == 200)
                {
                    String jsonResponse = await response.Content.ReadAsStringAsync();
                    CampaignBsDTO campaigns = JsonConvert.DeserializeObject<CampaignBsDTO>(jsonResponse);

                    return campaigns;
                }
                else
                {
                    throw new BackingServiceException("BS throws the error: " + statusCode);
                }
            }
            catch (Exception ex)
            {
                throw new BackingServiceException("Connection with Products is not working: " + ex.Message);
            }
        }

        //POST-DEACTIVATE
        public async Task<CampaignBsDTO> DeactivateCampaign(string id)
        {
            try
            {
                HttpClient campaignMS = new HttpClient();

                string msPath = _configuration.GetSection("Microservices").GetSection("Campaigns").Value;
                HttpResponseMessage response = await campaignMS.GetAsync($"{msPath}/campaigns/{id}/deactivate");

                int statusCode = (int)response.StatusCode;
                if (statusCode == 200)
                {
                    String jsonResponse = await response.Content.ReadAsStringAsync();
                    CampaignBsDTO campaigns = JsonConvert.DeserializeObject<CampaignBsDTO>(jsonResponse);

                    return campaigns;
                }
                else
                {
                    throw new BackingServiceException("BS throws the error: " + statusCode);
                }
            }
            catch (Exception ex)
            {
                throw new BackingServiceException("Connection with Products is not working: " + ex.Message);
            }
        }
        /*
        //DELETE
        public async Task DeleteCampaign(string id)
        {
            try
            {
                HttpClient campaignMS = new HttpClient();

                string msPath = _configuration.GetSection("Microservices").GetSection("Campaigns").Value;
                HttpResponseMessage response = await campaignMS.GetAsync($"{msPath}/campaigns/{id}");

                int statusCode = (int)response.StatusCode;
                if (statusCode == 200)
                {
                    String jsonResponse = await response.Content.ReadAsStringAsync();
                    CampaignBsDTO campaigns = JsonConvert.DeserializeObject<CampaignBsDTO>(jsonResponse);

                    
                }
                else
                {
                    throw new BackingServiceException("BS throws the error: " + statusCode);
                }
            }
            catch (Exception ex)
            {
                throw new BackingServiceException("Connection with Products is not working: " + ex.Message);
            }
        }*/
    }
}
