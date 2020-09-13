using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Therapy.Core
{
    public class Ticket
    {
        public string overallMood;

        public Dictionary<string,bool> assets { get; set; }
        public Activity prescribedActivity { get; set; }
        public string userId { get; set; }
        public List<string> details { get; set; }
        public DateTime prescriptionDate { get; }
        public string diagnosis { get; set; }
        public string id { get; }
        public Ticket(string userId)
        {
            this.assets = new Dictionary<string, bool>();
            this.prescriptionDate = DateTime.Now;
            this.id = Guid.NewGuid().ToString().Split('-')[0];
            this.userId = userId;
            this.details = new List<string>();
        }
        public void generateActivity()
        {
            this.prescribedActivity = new Activity(this.assets, this.userId, this.id);
        }
    }
}
