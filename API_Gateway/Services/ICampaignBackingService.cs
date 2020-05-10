using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BackingServices
{
    public interface ICampaignBackingService
    {
        public Task<List<CampaignBsDTO>> GetAllCampaign();
    }
}
