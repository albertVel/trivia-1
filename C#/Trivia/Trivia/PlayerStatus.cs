using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Trivia
{
    public class PlayerStatus
    {
        public bool inPenaltyBox { get; set; }

        public bool won { get; set; }

        public bool correctAnswer { get; set; }

        public string name { get; set; }
    }
}
