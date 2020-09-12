using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Therapy.Core;

namespace Therapy.Database
{
    public class TicketDb : ITherapyData
    {
        private List<Ticket> tickets;
        public TicketDb()
        {
            tickets = new List<Ticket>();
        }
        public Ticket add(Ticket ticket)
        {
            tickets.Add(ticket);
            return ticket;
        }

        public Ticket delete(Ticket ticket)
        {
            tickets.Remove(ticket);
            return ticket;
        }

        public List<Activity> getAllActivitiesForUser(string id)
        {
            //Might Be Extremely wrong
            List<Ticket> activityTickets = tickets.Where(r => r.userId == id).ToList();
            List<Activity> activities = activityTickets.Select(r => r.prescribedActivity).ToList();
            return activities;
        }

        public List<Ticket> getAllTicketsForUser(string id)
        {
            List<Ticket> userTickets = tickets.Where(r => r.userId == id).ToList();
            return userTickets;
        }

        public Ticket update(Ticket updatedTicket)
        {
            Ticket oldTicket = tickets.FirstOrDefault(r => r.id == updatedTicket.id);
            if(oldTicket != null)
            {
                oldTicket.prescribedActivity = updatedTicket.prescribedActivity;
                oldTicket.userId = updatedTicket.userId;
                oldTicket.details = updatedTicket.details;
                oldTicket.diagnosis = updatedTicket.diagnosis;
            }
            return oldTicket;
        }
    }
}
