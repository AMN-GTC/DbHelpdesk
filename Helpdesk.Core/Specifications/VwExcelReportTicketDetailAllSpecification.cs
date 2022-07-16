using Ardalis.Specification;
using Helpdesk.Core.Entities;

namespace Helpdesk.Core.Specifications
{
    public class VwExcelReportTicketDetailAllSpecification : Specification<VwExcelReportTicketDetailAll>
    {
        public string Idequal { get; set; }
        public int? YearEqual { get; set; }



        public Specification<VwExcelReportTicketDetailAll> Build()
        {
            if (Idequal != null)
            {
                Query.Where(f => f.Id == Idequal);
            }
            if (YearEqual.HasValue)
            {
                Query.Where(f => f.Start_time.Year == YearEqual.Value);
            }




            return this;
        }
    }
}
