using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Helpdesk.Core.Entities
{
    public class Userlogin
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
    