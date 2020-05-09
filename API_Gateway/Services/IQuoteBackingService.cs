using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services
{
    public interface IQuoteBackingService
    {
        public Task<QuoteBsDTO> AddNewQuote(QuoteBsDTO newQuote);
        public Task<List<QuoteBsDTO>> GetQuoteList();
        public void UpdateQuote(string id, QuoteBsDTO updatedQuote);
        void DeleteByID(string id);
        void UpdateSale(string id, bool state);
    }
}
