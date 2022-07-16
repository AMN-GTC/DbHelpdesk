using Ardalis.Specification;
using Helpdesk.Core.Entities;

namespace Helpdesk.Core.Specifications
{
    public class VwExcelReportDetailFilterSpecification : Specification<VwExcelReportDetail>
    {
        //public VwExcelReportDetailFilterSpecification()
        //{

        //}
        public int? Idequal { get; set; }
        public int? YearEqual { get; set; }
        public int? MonthEqual { get; set; }

        public Specification<VwExcelReportDetail> Build()
        {
            if (Idequal.HasValue)
            {
                Query.Where(f => f.Id == Idequal.Value);
            }
            if (YearEqual.HasValue)
            {
                Query.Where(f => f.Submission_date.Year == YearEqual.Value);
            }
            if (MonthEqual.HasValue)
            {
                Query.Where(f => f.Submission_date.Month == MonthEqual.Value);
            }

            return this;
        }
    }
}
