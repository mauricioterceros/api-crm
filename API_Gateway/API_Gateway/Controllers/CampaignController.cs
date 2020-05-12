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
            return _campaignBS.GetAllCampaign().Result;
        }
        
        [HttpPost]
        [Route("")]
        public CampaignBsDTO Post([FromBody] CampaignBsDTO campaign)
        {
            Console.WriteLine("from post => " + campaign.Id + " - " + campaign.Name + " - " + campaign.Type + " - " + campaign.Description);
            
            campaign = _campaignBS.AddNewCampaign(campaign).Result;
            return campaign;
        }
        
        // PUT: api/Campaign/5
        [HttpPut]
        [Route("{id}")]
        public CampaignBsDTO Put([FromBody]CampaignBsDTO campaign, string id)
        {

            return _campaignBS.UpdateCampaing(campaign, id).Result;

        }
        
        [HttpPost]
        [Route("{id}/activate")]
        public void Activate(string id)
        {
            //preguntar inge
            //como no dara ni un resultado, pues solo estoy haciendo que se conecten y que el usuario pueda hacer sus funciones no?
            _campaignBS.ActivateCampaign(id);
        }
        
        [HttpPost]
        [Route("{id}/deactivate")]
        public void Deactivate(string id)
        {
            _campaignBS.DeactivateCampaign(id);
            
        }
        /*
        [HttpDelete]
        [Route("{id}")]
        public void Delete(string id)
        {
            _campaignBS.DeleteCampaign(id);
        }*/
    }
}
