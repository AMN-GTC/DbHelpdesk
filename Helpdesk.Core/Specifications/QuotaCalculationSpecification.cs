using Ardalis.Specification;
using Helpdesk.Core.Entities;

namespace Helpdesk.Core.Specifications
{
    public class QuotaCalculationSpecification : Specification<QuotaCalculation>
    {
        public QuotaCalculationSpecification()
        {

        }
        public int? IdEqual { get; set; }

        public Specification<QuotaCalculation> Build()
        {
            if (IdEqual.HasValue)
            {
                Query.Where(x => x.Id == IdEqual.Value);
            }
            return this;
        }

    }
}
