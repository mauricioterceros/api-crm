﻿using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using BackingServices.Exceptions;
using Serilog;

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
        public async Task<IEnumerable<CampaignBsDTO>> GetAllCampaigns()
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
                    Log.Logger.Information("Succesfull");
                    return campaigns;
                }
                else
                {
                    Log.Logger.Information("BS throws the error: " + statusCode);
                    throw new BackingServiceException("BS throws the error: " + statusCode);
                }
            }
            catch (Exception ex)
            {
                Log.Logger.Information("Connection with Campaigns is not working: " + ex.Message);

                throw new BackingServiceException("Connection with Campaigns is not working: " + ex.Message);
            }
        }

        
        //GET-ACTIVE
        public async Task<CampaignBsDTO> GetCampaignActive()
        {
            try
            {
                HttpClient campaignMS = new HttpClient();

                string msPath = _configuration.GetSection("Microservices").GetSection("Campaigns").Value;
                HttpResponseMessage response = await campaignMS.GetAsync($"{msPath}/api/campaigns/active");

                int statusCode = (int)response.StatusCode;
                if (statusCode == 200)
                {
                    String jsonResponse = await response.Content.ReadAsStringAsync();
                    CampaignBsDTO campaigns = JsonConvert.DeserializeObject<CampaignBsDTO>(jsonResponse);
                    Log.Logger.Information("Succesfull");
                    return campaigns;
                }
                else
                {
                    Log.Logger.Information("BS throws the error: " + statusCode);
                    throw new BackingServiceException("BS throws the error: " + statusCode);
                }
            }
            catch (Exception ex)
            {
                Log.Logger.Information("Connection with Campaigns is not working: " + ex.Message);
                throw new BackingServiceException("Connection with Campaigns is not working: " + ex.Message);
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
                    Log.Logger.Information("Succesfull");
                    return campaigns;
                }
                else
                {
                    Log.Logger.Information("BS throws the error: " + statusCode);
                    throw new BackingServiceException("BS throws the error: " + statusCode);
                }
            }
            catch (Exception ex)
            {
                Log.Logger.Information("Connection with Campaigns is not working: " + ex.Message);
                throw new BackingServiceException("Connection with Campaigns is not working: " + ex.Message);
            }
        }

        //PUT
        public async Task UpdateCampaing(CampaignBsDTO campaignUpdate, string id)
        {

            try
            {
                HttpClient campaignMS = new HttpClient();
                String CampignUpdateString = JsonConvert.SerializeObject(campaignUpdate);
                HttpContent CampaignUpdateHTTP = new StringContent(CampignUpdateString, Encoding.UTF8, "application/json");

                string msPath = _configuration.GetSection("Microservices").GetSection("Campaigns").Value;
                HttpResponseMessage response = await campaignMS.PutAsync($"{msPath}/api/campaigns/{id}", CampaignUpdateHTTP);

                int statusCode = (int)response.StatusCode;
                if (statusCode == 200)
                {
                    String jsonResponse = await response.Content.ReadAsStringAsync();
                    CampaignBsDTO campaigns = JsonConvert.DeserializeObject<CampaignBsDTO>(jsonResponse);
                    Log.Logger.Information("Succesfull");
                }
                else
                {
                    Log.Logger.Information("BS throws the error: " + statusCode);
                    throw new BackingServiceException("BS throws the error: " + statusCode);
                }
            }
            catch (Exception ex)
            {
                Log.Logger.Information("Connection with Campaigns is not working: " + ex.Message);
                throw new BackingServiceException("Connection with Campaigns is not working: " + ex.Message);
            }

        }

        //POST-ACTIVATE
        public async Task ActivateCampaign(string id)
        {
            try
            {
                HttpClient campaignMS = new HttpClient();

                string msPath = _configuration.GetSection("Microservices").GetSection("Campaigns").Value;
                HttpResponseMessage response = await campaignMS.PostAsync($"{msPath}/api/campaigns/{id}/activate", new StringContent(String.Empty));

                int statusCode = (int)response.StatusCode;
                if (statusCode == 200)
                {
                    String jsonResponse = await response.Content.ReadAsStringAsync();
                    CampaignBsDTO campaigns = JsonConvert.DeserializeObject<CampaignBsDTO>(jsonResponse);
                    Log.Logger.Information("Succesfull");
                }
                else
                {
                    Log.Logger.Information("BS throws the error: " + statusCode);
                    throw new BackingServiceException("BS throws the error: " + statusCode);
                }
            }
            catch (Exception ex)
            {

                Log.Logger.Information("Connection with Campaigns is not working: " + ex.Message);
                throw new BackingServiceException("Connection with Campaigns is not working: " + ex.Message);
            }
        }

        //POST-DEACTIVATE
        public async Task DeactivateCampaign(string id)
        {
            try
            {
                HttpClient campaignMS = new HttpClient();

                string msPath = _configuration.GetSection("Microservices").GetSection("Campaigns").Value;
                HttpResponseMessage response = await campaignMS.PostAsync($"{msPath}/api/campaigns/{id}/deactivate", new StringContent(String.Empty));

                int statusCode = (int)response.StatusCode;
                if (statusCode == 200)
                {
                    String jsonResponse = await response.Content.ReadAsStringAsync();
                    CampaignBsDTO campaigns = JsonConvert.DeserializeObject<CampaignBsDTO>(jsonResponse);
                    Log.Logger.Information("Succesfull");
                }
                else
                {
                    Log.Logger.Information("BS throws the error: " + statusCode);
                    throw new BackingServiceException("BS throws the error: " + statusCode);
                }
            }
            catch (Exception ex)
            {
                Log.Logger.Information("Connection with Campigns is not working: " + ex.Message);
                throw new BackingServiceException("Connection with Campaigns is not working: " + ex.Message);
            }
        }
        
        //DELETE
        public async Task DeleteCampaign(string id)
        {
            try
            {
                HttpClient campaignMS = new HttpClient();

                string msPath = _configuration.GetSection("Microservices").GetSection("Campaigns").Value;
                HttpResponseMessage response = await campaignMS.DeleteAsync($"{msPath}/api/campaigns/{id}");

                int statusCode = (int)response.StatusCode;
                if (statusCode == 200)
                {
                    String jsonResponse = await response.Content.ReadAsStringAsync();
                    CampaignBsDTO campaigns = JsonConvert.DeserializeObject<CampaignBsDTO>(jsonResponse);
                    Log.Logger.Information("Succesfull");
                }
                else
                {
                    Log.Logger.Information("BS throws the error: " + statusCode);
                    throw new BackingServiceException("BS throws the error: " + statusCode);
                }
            }
            catch (Exception ex)
            {
                Log.Logger.Information("Connection with Campaigns is not working: " + ex.Message);
                throw new BackingServiceException("Connection with Campaigns is not working: " + ex.Message);
            }
        }
    }
}
