using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackingServices;

namespace API_Gateway.Controllers
{
    [Route("api-crm/campaing")]
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
    }
}
