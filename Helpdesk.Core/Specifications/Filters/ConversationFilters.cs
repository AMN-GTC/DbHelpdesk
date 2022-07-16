
using Ardalis.Specification;
using System.Collections.Generic;
using Helpdesk.Core.Entities;
using Helpdesk.Core.Specifications;

namespace API_DB_Ticket.Specification.Filters
{
    public class ConversationFilter
    {
        public ConversationFilter(Dictionary<string, string> param)
        {
            Dictionary<string, string> keyValuePairs = new Dictionary<string, string>();
            foreach (var pair in param)
            {
                if (!keyValuePairs.ContainsKey(pair.Key.ToLower()))
                {
                    keyValuePairs.Add(pair.Key.ToLower(), pair.Value);
                }
                keyValuePairs[pair.Key.ToLower()] = pair.Value;
            }

        }
        public Specification<Conversation> ToSpecification()
        {
           ConversationSpecification conversationSpecification = new ConversationSpecification();
          
            return conversationSpecification.Build();
        }
    }
}
