using System;
using System.Collections.Generic;
using System.Text;

namespace Services
{
    class ClientsRankingBsDTO
    {
        public class RankingDTO
        {
            public string RankingName { get; set; }

            public List<ClientsBsDTO> Clients { get; set; }

            public int MaxNumberOfClients { get; set; }
        }
    }
}
