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

        public string wonString {
            get
            {
                var text = "";
                if (won)
                {
                    text= "Won";
                }
                else
                {
                    text = "Didn't won";
                }
                return text;
            }
        }


        public bool correctAnswer { get; set; }

        public string correctAnswerString
        {
            get
            {
                var text = "";
                if (correctAnswer)
                {
                    text = "Correct";
                }
                else
                {
                    text = "Wrong";
                }
                return text;
            }
            set{}
        }

        public string name { get; set; }

        public int dice { get; set; }

        public int pursues { get; set; }
    }
}
