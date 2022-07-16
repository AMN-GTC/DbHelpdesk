using Ardalis.Specification;
using Helpdesk.Core.Entities;

namespace Helpdesk.Core.Specifications
{
    public class VwExcelMonthlySpecification : Specification<VwExcelMonthly>
    {
        public VwExcelMonthlySpecification()
        {

        }
        public string ProjectEqual { get; set; }

        public Specification<VwExcelMonthly> Build()
        {
            if (ProjectEqual != null)
            {
                Query.Where(f => f.Project_Name == ProjectEqual);
            }
            return this;
        }
    }
}
