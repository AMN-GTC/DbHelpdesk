using Ardalis.Specification;
using Helpdesk.Core.Entities;

namespace Helpdesk.Core.Specifications
{
    public class VwQuotaSpec : Specification<VwQuota>
    {
        public VwQuotaSpec()
        {

        }
        public string Projects { get; set; }
        public double? QuotaStocks { get; set; }
        public double? QuotaBalances { get; set; }
        public double? QuotaUses { get; set; }
        public Specification<VwQuota> Build()
        {
            if (!string.IsNullOrEmpty(Projects))
            {
                Query.Where(f => f.Project != null);
            }
            if (QuotaStocks.HasValue)
            {
                Query.Where(f => f.QuotaStock == QuotaStocks.Value);
            }
            if (QuotaBalances.HasValue)
            {
                Query.Where(f => f.QuotaBalance == QuotaBalances.Value);
            }
            if (QuotaUses.HasValue)
            {
                Query.Where(f => f.QuotaUse == QuotaUses.Value);
            }
            return this;
        }
    }
}