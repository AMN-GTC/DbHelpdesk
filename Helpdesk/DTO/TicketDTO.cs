using System;
using System.Collections.Generic;

namespace Helpdesk.DTO
{
    public class TicketDTO
    {
        public int Id { get; set; }
        public string Application { get; set; }
        public string Assign_to_username { get; set; }
        public int ProjectId { get; set; }
        public string Ticket_category { get; set; }
        public string Title { get; set; }
        public string Problem_description { get; set; }
        public Ticket_source Ticket_source { get; set; }
        public string Ticket_status { get; set; }
        public int StatusId { get; set; }
        public DateTime Submission_date { get; set; }
        public DateTime Due_date { get; set; }
        public DateTime Finish_date { get; set; }
        public string Attachment { get; set; }
        public string Reported_by { get; set; }
        public string Remaks { get; set; }
        public int UserId { get; set; }
        public List<TimerDTO> Timers { get; set; } = new List<TimerDTO>();

    }
    public enum Ticket_source
    {
        Chat,
        Phone,
        Helpdesk_Portal,

    }
}

