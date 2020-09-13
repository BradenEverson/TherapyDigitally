using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Therapy.Core;
using Therapy.Database;
using TherapyDigitally.Data;

namespace TherapyDigitally
{
    public class activityModel : PageModel
    {
        private ITherapyData tickets;
        private TherapyDigitallyContext db;
        public User currentUser;
        public Ticket currentTicket;
        public activityModel(ITherapyData tickets,TherapyDigitallyContext db)
        {
            this.tickets = tickets;
            this.db = db;
        }
        public void OnGet(string ticketId)
        {
            currentTicket = tickets.getById(ticketId);
            currentUser = db.Users.FirstOrDefault(r => r.Email == User.Identity.Name);
        }
    }
}