using Helpdesk.Core.Entities;
using System;

namespace Helpdesk.DTO
{
    public class QuotaCalcullationDTO
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public DateTime Tanggal_pembelian { get; set; }
        public DateTime Tanggal_Expired { get; set; }
        public int Quota { get; set; }
        public virtual Project Project { get; set; }
    }
}
