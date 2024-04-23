using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamePlanner1Logics
{
    public class Game
    {
        public string GameName { set; get; }
        public int NoOfTeams { get; set; }
        public List<Team> Teams { get; set; }
    }
}
