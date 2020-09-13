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
        private List<String> botMemory;
        private readonly TherapyDigitallyContext db;
        private readonly ITherapyData tickets;
        private List<String> smallDialogue = new List<string>()
        {
            "Have you been feeling stressed lately?",
            "Have you been having trouble sleeping?",
            "Have you had trouble focusing on things?",
            "Do you feel overworked a lot lately?",
            "Have your symptoms gotten worse lately?",
            "Have you felt very lazy lately?"
        };
        public TherapyHub(ITherapyData tickets, TherapyDigitallyContext db)
        {
            botMemory = new List<string>();
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
            else if(message.ToLowerInvariant() != "no")
            {
                botMessage = smallDialogue[StaticRandom.Instance.Next(0, smallDialogue.Count() - 1)];
            }
            else
            {
                //If user wants to leave
                botMessage = "Okay, feel free to continue to the activity created by this information!";
                currentTicket.generateActivity();
            }
            if (message.ToLowerInvariant().Contains("yes"))
            {
                currentTicket.assets.Add(botMemory[botMemory.Count() - 1], true);
            }else if (message.ToLowerInvariant().Contains("no"))
            {
                currentTicket.assets.Add(botMemory[botMemory.Count() - 1], false);
            }
            botMemory.Add(message);
            botMemory.Add(botMessage);
            tickets.update(currentTicket);
            await Clients.Caller.SendAsync("BotMessage", botMessage);
        }
    }
}

