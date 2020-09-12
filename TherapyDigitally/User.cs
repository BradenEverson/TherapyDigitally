using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TherapyDigitally
{
    public class User : IdentityUser
    {
        public int activitiesCompleted;

        public User()
        {
            activitiesCompleted = 0;
        }
    }
}
