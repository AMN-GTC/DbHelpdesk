using System.Collections.Generic;

namespace API_DB_Ticket.Specification.Filters
{
    public class VwActiveTicketSummaryFilter
    {
        public VwActiveTicketSummaryFilter(Dictionary<string, string> filter)
        {
            Dictionary<string , string> keyValuePairs = new Dictionary<string , string>();
            foreach(KeyValuePair<string, string> pair in filter)
            {
                if(pair.Value != null)
                {
                    keyValuePairs.Add(pair.Key, pair.Value);
                }
            }
        }
    }
}
