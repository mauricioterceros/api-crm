using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using BackingServices;

namespace BackingServices
{
    public class CampaignBackingService : ICampaignBackingService
    {
        private readonly IConfiguration _configuration;
        
        public CampaignBackingService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<List<CampaignBsDTO>> GetAllCampaign()
        {
            try
            {
                HttpClient campaignMS = new HttpClient();

                string msPath = _configuration.GetSection("Microservices").GetSection("Campaigns").Value;
                HttpResponseMessage response = await campaignMS.GetAsync($"{msPath}/campaigns");

                int statusCode = (int)response.StatusCode;
                if (statusCode == 200)
                {
                    String jsonResponse = await response.Content.ReadAsStringAsync();
                    List<CampaignBsDTO> campaigns = JsonConvert.DeserializeObject<List<CampaignBsDTO>>(jsonResponse);

                    return campaigns;
                }
                else
                {
                    throw new NotImplementedException();
                }
            }
            catch (Exception ex)
            {
                throw new NotImplementedException();
            }
        }
    }
}
