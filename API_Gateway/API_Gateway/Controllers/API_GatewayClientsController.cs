using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace API_Gateway.Controllers
{

    [Route("api-crm")]
    [ApiController]
    public class Clients : ControllerBase
    {
        private readonly IClientsBackingService _clientsDB;

        public Clients(IClientsBackingService productBS1)
        {
            _clientsDB = productBS1;
        }

        [HttpGet]
        [Route("clients")]
        public List<ClientsBsDTO> GetClient()
        {
            return _clientsDB.GetClients().Result;
        }

        [HttpGet]
        [Route("rankings")]
        public List<RankingDTO> GetRanks()
        {
            return _clientsDB.GetRankings().Result;

        }

        [HttpPost]
        [Route("clients")]
        public ClientsBsDTO AddNewClient([FromBody]ClientsBsDTO newClientdto)
        {
            return _clientsDB.AddNewClient(newClientdto).Result;
        }
        [HttpPut]
        [Route("clients/{code}")]
        public Task<ClientsBsDTO> UpdateProduct(string code, [FromBody]ClientsBsDTO clientToUpdate)
        {
            return _clientsDB.UpdateClient(code, clientToUpdate);
        }

        [HttpDelete]
        [Route("clients/{code}")]
        public Task<bool> DeleteProduct(string code)
        {
            return _clientsDB.DeleteClient(code);
        }

        /*[HttpGet]
        [Route("{id}")]
        public IEnumerable<string> GetById()
        {
            throw new NotImplementedException();
        } */


    }

}