using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
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
        private List<String> smallDialogue = new List<string>()
        {
            "Oh alright, noted, should I continue?",
            "Sounds good, I'll write that down. Anything else?",
            "Okay. Anything else?",
            "Thankks for sharing! Anything else?",
            "Thankks for sharing! Anything else you want to share with me?"
        };
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
            tickets.update(currentTicket);
            await Clients.Caller.SendAsync("BotMessage", botMessage);
        }
    }
    public class StaticRandom
    {
        private static int seed;

        private static readonly ThreadLocal<Random> threadLocal = new ThreadLocal<Random>
            (() => new Random(Interlocked.Increment(ref seed)));

        static StaticRandom()
        {
            seed = Environment.TickCount;
        }

        public static Random Instance { get { return threadLocal.Value; } }
    }
}

