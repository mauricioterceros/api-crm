using System.Collections.Generic;

namespace Services
{
    public class QuoteBsDTO
    {
        public string QuoteID { get; set; }
        public string QuoteName { get; set; }
        public string ClientCode { get; set; }
        public List<QuoteProductsBsDTO> QuoteLineItems { get; set; }
        public bool IsSell { get; set; }
    }
}