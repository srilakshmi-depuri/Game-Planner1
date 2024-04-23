using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamePlanner1Logics
{
    public class Team
    {
        public string TeamName { set; get; }
        public string GameName { get; set; }
        public int NoOfParticipants { get; set; }
        public List<string> Participants { set; get; }
    }
}
