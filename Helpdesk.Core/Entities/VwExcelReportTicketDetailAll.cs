using OfficeOpenXml.FormulaParsing.Excel.Functions.DateTime;
using System;

namespace Helpdesk.Core.Entities
{
    public class VwExcelReportTicketDetailAll
    {
        public string Id { get; set; }
        public int TicketId { get; set; }
        public string Project_Name { get; set; }
        public string Problem_description { get; set; }
        public string Reported_by { get; set; }
        public DateTime Submission_date { get; set; }
        public DateTime Start_date { get; set; }
        public DateTime Start_time { get; set; }
        public DateTime End_date { get; set; }
        public DateTime End_time { get; set; }
        public string Status_Ticket { get; set; }
        public int Jam { get; set; }
        public int Menit_total { get; set; }
        public int Quota { get; set; }
    }
}
