using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Therapy.Core;
using TherapyDigitally;

namespace Therapy.Database
{
    public class TicketDb : ITherapyData
    {
        private List<String> smallDialogue = new List<string>()
        {
            "Have you been feeling stressed lately?",
            "Have you been having trouble sleeping?",
            "Have you had trouble focusing on things?"
        };
        private readonly List<Ticket> tickets;

        private readonly List<String> botMemory;
        public TicketDb()
        {
            tickets = new List<Ticket>();
            botMemory = new List<string>();
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

        public Ticket getById(string ticketId)
        {
            return tickets.FirstOrDefault(r => r.id == ticketId);
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
                oldTicket.assets = updatedTicket.assets;
                oldTicket.overallMood = updatedTicket.overallMood;
            }
            return oldTicket;
        }
        public string addToBotMemory(string addition)
        {
            botMemory.Add(addition);
            return addition;
        }
        public void DeleteAllInBotMemory()
        {
            botMemory.Clear();
        }
        public string getRecent()
        {
            return botMemory[botMemory.Count() - 1];
        }
        public string GetRandom()
        {
            if(smallDialogue.Count == 0)
            {
                return "Okay, feel free to continue to the activity created by this information!";
            }
            string message = smallDialogue[StaticRandom.Instance.Next(0, smallDialogue.Count() - 1)];
            smallDialogue.Remove(message);
            return message;
        }
    }
}
