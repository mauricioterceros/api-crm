using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface IClientsBackingService
    {
        public Task<List<RankingDTO>> GetRankings();
        public Task<List<ClientsBsDTO>> GetClients();
        public Task<ClientsBsDTO> AddNewClient(ClientsBsDTO newClient);
        public Task<ClientsBsDTO> UpdateClient(string code, ClientsBsDTO clientToUpdate);
        public Task<bool> DeleteClient(string code);
    }
}
