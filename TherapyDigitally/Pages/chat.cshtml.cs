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
    public class chatModel : PageModel
    {
        private readonly ITherapyData tickets;
        private readonly TherapyDigitallyContext db;
        public User currentUser;
        public Ticket newTicket;
        public chatModel(ITherapyData tickets, TherapyDigitallyContext db)
        {
            this.db = db;
            this.tickets = tickets;
        }
        public void OnGet()
        {
            currentUser = db.Users.FirstOrDefault(r => r.Email == User.Identity.Name);
            Console.WriteLine(currentUser.Email + " " + currentUser.Id);
            newTicket = new Ticket(currentUser.Id);
            tickets.add(newTicket);
        }
    }
}