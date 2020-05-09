using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface IClientsBackingService
    {
        public Task<ClientsBsDTO> AddNewClient(ClientsBsDTO newClient);
        public Task<List<ClientsBsDTO>> GetClients();
        //public ClientsBsDTO UpdateClient(string code, ClientsBsDTO clientToUpdate);
        //public ClientsBsDTO DeleteClient(string code);
    }
}
