using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Therapy.Core
{
    public class Ticket
    {
        public Activity prescribedActivity { get; set; }
        public string userId { get; set; }
        public List<string> details { get; set; }
        public DateTime prescriptionDate { get; }
        public string diagnosis { get; set; }
        public string id { get; }
        public Ticket(string userId)
        {
            this.prescriptionDate = DateTime.Now;
            this.id = Guid.NewGuid().ToString().Split('-')[0];
            this.userId = userId;
        }
        public void generateActivity()
        {
            Dictionary<string, int> everyDiagnosis = new Dictionary<string, int>();
            foreach (string detail in details)
            {
                //String diagnosis = {Scrape specific detail from a health database to see which illness it most likely is}
                //if(everyDiagnosis.containsKey(diagnosis){
                    //everyDiagnosis[diagnosis] = everyDiagnosis[diagnosis] + 1;
                //}else{
                    //everyDiagnosis.add(diagnosis,1);
                //}
            }
            //Cross reference every detail to see which illness is the most common in the list. Then set the diagnosis to that.
            int max = everyDiagnosis.Values.Max();
            this.diagnosis = everyDiagnosis.FirstOrDefault(r => r.Value == max).Key;
            this.prescribedActivity = new Activity(this.diagnosis, this.userId, this.id);
        }
    }
}
