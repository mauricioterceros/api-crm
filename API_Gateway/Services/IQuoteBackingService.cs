using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services
{
    public interface IQuoteBackingService
    {
        public Task<QuoteBsDTO> AddNewQuote(QuoteBsDTO newQuote);
        public Task<List<QuoteBsDTO>> GetQuoteList();
        public Task<string> UpdateQuote(string id, QuoteBsDTO updatedQuote);
        Task<string> DeleteByID(string id);
        Task<string> UpdateSale(string id, bool state);
    }
}
