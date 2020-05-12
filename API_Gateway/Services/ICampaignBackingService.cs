using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BackingServices
{
    public interface ICampaignBackingService
    {
        public Task<IEnumerable<CampaignBsDTO>> GetAllCampaign();
        public Task<CampaignBsDTO> AddNewCampaign(CampaignBsDTO newCampaign);
        public Task<CampaignBsDTO> UpdateCampaing(CampaignBsDTO campaign, string id);
        public Task<CampaignBsDTO> ActivateCampaign(string id);
        public Task<CampaignBsDTO> DeactivateCampaign(string id);
        //public Task<CampaignBsDTO> DeleteCampaign(string id);
    }
}
