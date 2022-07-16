using Ardalis.Specification;
using Helpdesk.Core.Entities;
using System.Collections.Generic;

namespace Helpdesk.Core.Specifications.Filters
{
    public class VwExcelReportDetailFilter
    {

        public int? Year { get; set; }
        public int? Month { get; set; }
        public VwExcelReportDetailFilter(Dictionary<string, string> param)
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
                if (keyValuePairs.ContainsKey("month"))
                {
                    Month = int.Parse(keyValuePairs["month"]);
                }
            }

        }

        public Specification<VwExcelReportDetail> ToSpecification()
        {
            VwExcelReportDetailFilterSpecification spec = new VwExcelReportDetailFilterSpecification();
            if (Year.HasValue)
            {
                spec.YearEqual = Year;
            }
            if (Month.HasValue)
            {
                spec.MonthEqual = Month;
            }
            return spec.Build();

        }
    }
}
