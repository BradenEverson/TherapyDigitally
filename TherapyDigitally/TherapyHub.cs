using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Therapy.Core;
using Therapy.Database;
using TherapyDigitally.Data;

namespace TherapyDigitally
{
    public class TherapyHub : Hub
    {
        private readonly TherapyDigitallyContext db;
        private readonly ITherapyData tickets;
        public TherapyHub(ITherapyData tickets, TherapyDigitallyContext db)
        {
            this.tickets = tickets;
            this.db = db;
        }
        public async Task UserMessage(string ticketId, string message)
        {
            string botMessage = "";
            Ticket currentTicket = tickets.getById(ticketId);
            User currentUser = db.Users.FirstOrDefault(r => r.Id == currentTicket.userId);
            if(message.ToLowerInvariant() != "no")
            {
                currentTicket.details.Add(message);
            }
            
            if(currentTicket.details.Count <= 1)
            {
                botMessage = "What made you feel " + message + "?";
                currentTicket.overallMood = message;
            }
            else if(message.ToLowerInvariant() != "stop")
            {
                botMessage = tickets.GetRandom();
            }
            else
            {
                //If user wants to leave
                botMessage = "Okay, feel free to continue to the activity created by this information!";
                tickets.DeleteAllInBotMemory();
                currentTicket.generateActivity();
            }
            if (message.ToLowerInvariant().Contains("yes"))
            {
                currentTicket.assets.Add(tickets.getRecent(), true);
            }else if (message.ToLowerInvariant().Contains("no"))
            {
                currentTicket.assets.Add(tickets.getRecent(), false);
            }
            tickets.addToBotMemory(message);
            tickets.addToBotMemory(botMessage);
            tickets.update(currentTicket);
            await Clients.Caller.SendAsync("BotMessage", botMessage);
        }
    }
}

