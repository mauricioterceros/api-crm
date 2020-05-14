using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackingServices;
using Microsoft.Extensions.Configuration;

namespace API_Gateway.Controllers
{
    [Route("api-crm/campaigns")]
    [ApiController]
    public class CampaignController : ControllerBase
    {
        private readonly ICampaignBackingService _campaignBS;

        public CampaignController(ICampaignBackingService campaignBS)
        {
            _campaignBS = campaignBS;
        }

        [HttpGet]
        [Route("")]
        public IEnumerable<CampaignBsDTO> GetAll()
        {
            return _campaignBS.GetAllCampaigns().Result;
        }

        [HttpGet]
        [Route("active")]
        public CampaignBsDTO GetActive()
        {
            return _campaignBS.GetCampaignActive().Result;
        }

        [HttpPost]
        [Route("")]
        public CampaignBsDTO Post([FromBody] CampaignBsDTO campaign)
        {
            return _campaignBS.AddNewCampaign(campaign).Result;
        }
        
        [HttpPut]
        [Route("{id}")]
        public void Put([FromBody]CampaignBsDTO campaign, string id)
        {
            _campaignBS.UpdateCampaing(campaign, id);
        }
        
        [HttpPost]
        [Route("{id}/activate")]
        public void Activate(string id)
        {
           _campaignBS.ActivateCampaign(id);
        }
        
        [HttpPost]
        [Route("{id}/deactivate")]
        public void Deactivate(string id)
        {
            _campaignBS.DeactivateCampaign(id);
        }
        
        [HttpDelete]
        [Route("{id}")]
        public void Delete(string id)
        {
            _campaignBS.DeleteCampaign(id);
        }
    }
}
