using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Helpdesk.Core.Entities
{
    [Table("tbl_quota")]
    public class QuotaCalculation
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public DateTime Tanggal_pembelian { get; set; }
        public DateTime Tanggal_Expired { get; set; }
        public int Quota { get; set; }
        public virtual Project Project { get; set; }
    }
}
