using System;
using System.Collections.Generic;

namespace Therapy.Core
{
    public class Activity
    {
        private List<List<String>> exampleDirections = new List<List<string>>()
        {
            new List<string>()
            {
                "Belly Breathing",
                "Sit Or Lie flat in a comfortable position",
                "Put one hand on your belly just below your ribs and the other hand on your chest.",
                "Take a deep breath in through your nose, and let your belly push your hand out. Your chest should not move.",
                "Breathe out through pursed lips as if you were whistling. Feel the hand on your belly go in, and use it to push all the air out.",
                "Do this breathing 3 to 10 times. Take your time with each breath."
            },
            new List<string>()
            {
                "Low Impactful Stretching",
                "Sit or stand and clasp your hands together behind your back, arms straight.",
                "Lift your hands towards the ceiling, going only as high as is comfortable. You should feel a stretch in your shoulders and chest.",
                "Hold for 15 to 30 seconds, repeating one to three times."
            },
            new List<string>()
            {
                "4-7-8 breathing",
                "To start, put one hand on your belly and the other on your chest.",
                "Take a deep, slow breath from your belly, and silently count to 4 as you breathe in.",
                "Hold your breath, and silently count from 1 to 7.",
                "Breathe out completely as you silently count from 1 to 8. Try to get all the air out of your lungs by the time you count to 8.",
                "Repeat 3 to 7 times or until you feel calm."
            }
        };
        private List<String> smallDialogue = new List<string>()
        {
            "Have you been feeling stressed lately?",
            "Have you been having trouble sleeping?",
            "Have you had trouble focusing on things?",
            "Have you felt very lazy lately?"
        };
        public Dictionary<string,bool> assets { get; }
        public string userId { get; }
        public List<List<String>> nestedDirections { get; }//Special Conditions: {wait:int} creates a timer for that long, {reflect:string} creates a textarea, {poll:[string array]} creates a poll
        public string ticketId { get; }
        public string id { get; }
        public Activity(Dictionary<string,bool> assets, string userId, string ticketId)
        {
            this.ticketId = ticketId;
            this.assets = assets;
            this.userId = userId;
            this.id = Guid.NewGuid().ToString().Split('-')[0];
            for(int i = 0; i < smallDialogue.Count; i++)
            {
                if (assets.ContainsKey(smallDialogue[i]))
                    {
                    if (assets[smallDialogue[i]])
                    {
                        nestedDirections.Add(exampleDirections[i]);
                    }
                }
            }
        }
    }
}
