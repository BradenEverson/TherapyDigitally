using System;
using System.Collections.Generic;

namespace Therapy.Core
{
    public class Activity
    {
        public string activityType { get; }
        public string userId { get; }
        public List<string> directions { get; }//Special Conditions: {wait:int} creates a timer for that long, {reflect:string} creates a textarea, {poll:[string array]} creates a poll
        public string ticketId { get; }
        public string id { get; }
        public Activity(string activityType, string userId, string ticketId)
        {
            this.ticketId = ticketId;
            this.activityType = activityType;
            this.userId = userId;
            this.id = Guid.NewGuid().ToString().Split('-')[0];
            //Code for scraping 
        }
    }
}
