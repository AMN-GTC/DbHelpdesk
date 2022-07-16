using System;

namespace Helpdesk.Core.Entities
{
    public class VwExcelReportDetail
    {
        public int Id { get; set; }
        public string Project_Name { get; set; }
        public string Problem_description { get; set; }
        public string Reported_by { get; set; }
        public DateTime Submission_date { get; set; }
        public DateTime Finish_date { get; set; }
        public string Status_Ticket { get; set; }
        public DateTime? Starts { get; set; }
        public DateTime? Ends { get; set; }

    }
}
