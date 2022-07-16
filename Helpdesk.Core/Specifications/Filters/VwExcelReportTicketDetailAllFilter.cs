using Ardalis.Specification;
using Helpdesk.Core.Entities;
using System.Collections.Generic;

namespace Helpdesk.Core.Specifications.Filters
{
    public class VwExcelReportTicketDetailAllFilter
    {
        public int? Year { get; set; }

        public VwExcelReportTicketDetailAllFilter(Dictionary<string, string> param)
        {
            Dictionary<string, string> keyValuePairs = new Dictionary<string, string>();
            foreach (var pair in param)
            {
                if (!keyValuePairs.ContainsKey(pair.Key.ToLower()))
                {
                    keyValuePairs.Add(pair.Key.ToLower(), pair.Value);
                }
                keyValuePairs[pair.Key.ToLower()] = pair.Value;

                if (keyValuePairs.ContainsKey("year"))
                {
                    Year = int.Parse(keyValuePairs["year"]);
                }

            }

        }

        public Specification<VwExcelReportTicketDetailAll> ToSpecification()
        {
            VwExcelReportTicketDetailAllSpecification spec = new VwExcelReportTicketDetailAllSpecification();
            if (Year.HasValue)
            {
                spec.YearEqual = Year;
            }

            return spec.Build();

        }
    }

}
