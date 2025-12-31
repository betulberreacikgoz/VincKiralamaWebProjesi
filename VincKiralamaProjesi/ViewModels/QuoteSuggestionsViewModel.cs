using System.Collections.Generic;
using VincKiralamaProjesi.Models;

namespace VincKiralamaProjesi.ViewModels
{
    public class QuoteSuggestionsViewModel
    {
        public QuoteRequestViewModel Request { get; set; } = null!;
        public List<Crane> SuggestedCranes { get; set; } = new List<Crane>();
    }
}
