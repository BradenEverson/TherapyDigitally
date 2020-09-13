using System;
using System.Collections.Generic;
using System.Text;
using Therapy.Core;

namespace Therapy.Database
{
    public interface ITherapyData
    {
        public Ticket add(Ticket ticket);
        public Ticket delete(Ticket ticket);
        public Ticket update(Ticket updatedTicket);
        public List<Ticket> getAllTicketsForUser(string id);
        public List<Activity> getAllActivitiesForUser(string id);
        public Ticket getById(string ticketId);
        public void DeleteAllInBotMemory();
        public string getRecent();
        public string addToBotMemory(string addition);
        public string GetRandom();
    }
}
