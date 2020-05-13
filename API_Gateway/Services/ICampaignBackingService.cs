using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BackingServices
{
    public interface ICampaignBackingService
    {
        public Task<IEnumerable<CampaignBsDTO>> GetAllCampaign();
        //public Task<IEnumerable<CampaignBsDTO>> GetActivateCampaign();
        public Task<CampaignBsDTO> AddNewCampaign(CampaignBsDTO newCampaign);
        public Task UpdateCampaing(CampaignBsDTO campaignUpdate, string id);
        public Task ActivateCampaign(string id);
        public Task DeactivateCampaign(string id);
        public Task DeleteCampaign(string id);
    }
}
