using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackingServices;
using Microsoft.Extensions.Configuration;
using Serilog;

namespace API_Gateway.Controllers
{
    [Route("api-crm/campaigns")]
    [ApiController]
    public class CampaignController : ControllerBase
    {
        private readonly ICampaignBackingService _campaignBS;
        private IConfiguration _configuration;

        public CampaignController(ICampaignBackingService campaignBS, IConfiguration configuration)
        {
            _campaignBS = campaignBS;
            _configuration = configuration;
        }

        [HttpGet]
        [Route("")]
        public IEnumerable<CampaignBsDTO> GetAll()
        {
            Log.Logger.Information("Client trying to Get Campaigns list: ");
            return _campaignBS.GetAllCampaigns().Result;
        }

        [HttpGet]
        [Route("active")]
        public CampaignBsDTO GetActive()
        {
            Log.Logger.Information("Client trying to Get Active Campaign: ");
            return _campaignBS.GetCampaignActive().Result;
        }

        [HttpPost]
        [Route("")]
        public CampaignBsDTO Post([FromBody] CampaignBsDTO campaign)
        {

            var dbServer = _configuration.GetSection("Database").GetSection("ServerName");
            campaign.Name = $"{campaign.Name} data from {dbServer.Value}";
            Log.Logger.Information("Client trying to Create a New Campaign: "+campaign.Id);
            return _campaignBS.AddNewCampaign(campaign).Result;
        }
        
        [HttpPut]
        [Route("{id}")]
        public void Put([FromBody]CampaignBsDTO campaign, string id)
        {
            Log.Logger.Information("Client trying to Create a Update Campaign: " + campaign.Id);
            _campaignBS.UpdateCampaing(campaign, id);
        }
        
        [HttpPost]
        [Route("{id}/activate")]
        public void Activate(string id)
        {
            Log.Logger.Information("Client trying to Activate Campaign: " + id);
            _campaignBS.ActivateCampaign(id);
        }
        
        [HttpPost]
        [Route("{id}/deactivate")]
        public void Deactivate(string id)
        {
            Log.Logger.Information("Client trying to Deactivate Campaign: " + id);
            _campaignBS.DeactivateCampaign(id);
        }
        
        [HttpDelete]
        [Route("{id}")]
        public void Delete(string id)
        {
            Log.Logger.Information("Client trying to Delete Campaign: " + id);
            _campaignBS.DeleteCampaign(id);
        }
    }
}
