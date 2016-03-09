using System;
using System.Collections.Generic;
using System.Linq;

namespace Trivia
{
    using System.Diagnostics;

    /// <summary>
    /// Definitions of Enum Category
    /// </summary>
    internal enum Category
    {
        Unknown,
        Pop,
        Science,
        Sports,
        Rock
    }

    public class Game
    {
        const int MaxPlayers = 6;
        const int MagicalNumber = 7;

        List<string> players = new List<string>();

        int[] places = new int[MaxPlayers];
        int[] purses = new int[MaxPlayers];

        bool[] inPenaltyBox = new bool[MaxPlayers];

        int currentPlayer = 0;

        private QuestionHandler questionHandler;

        /// <summary>
        /// Initializes a new instance of the <see cref="Game"/> class.
        /// </summary>
        public Game()
        {
            questionHandler = new QuestionHandler();
            questionHandler.GenerateQuestions();  
        }

        /// <summary>
        /// Adds the player.
        /// </summary>
        /// <param name="playerName">Name of the player.</param>
        public void AddPlayer(string playerName)
        {
            players.Add(playerName);
            places[players.Count] = 0;
            purses[players.Count] = 0;
            inPenaltyBox[players.Count] = false;

            Console.WriteLine(playerName + " was added");
            Console.WriteLine("They are player number " + players.Count);
        }

        /// <summary>
        /// Rolls the dice.
        /// </summary>
        /// <param name="dice">The dice.</param>
        /// <param name="luckyNumber">The lucky number.</param>
        public bool RollTheDice(int dice, int luckyNumber)
        {
            var winner = false;
            Console.WriteLine(players[currentPlayer] + " is the current player");
            Console.WriteLine("They have rolled a " + dice);

            if (inPenaltyBox[currentPlayer])
            {
                if (dice % 2 != 0)
                {
                    Console.WriteLine(players[currentPlayer] + " is getting out of the penalty box");
                    winner = this.ProcessDice(dice, luckyNumber);
                }
                else
                {
                    Console.WriteLine(players[currentPlayer] + " is not getting out of the penalty box");
                }
            }
            else
            {
                winner = this.ProcessDice(dice, luckyNumber);
            }
          
            return winner;
        }


        /// <summary>
        /// Processes the answer.
        /// </summary>
        /// <param name="luckyNumber">The lucky number.</param>
        private void ProcessAnswer(int luckyNumber)
        {
            if (luckyNumber == MagicalNumber)
            {
                this.ProcessWrongAnswers();
            }
            else
            {
                this.ProcessCorrectAnswers();
            }
        }

        /// <summary>
        /// Processes the dice.
        /// </summary>
        /// <param name="roll">The roll.</param>
        /// <param name="luckyNumber">The lucky number.</param>
        /// <returns>Returns true if the player won the game, false otherwise</returns>
        private bool ProcessDice(int roll, int luckyNumber)
        {
            places[currentPlayer] = places[currentPlayer] + roll;
            if (places[currentPlayer] > 11) places[currentPlayer] = places[currentPlayer] - 12;

            Console.WriteLine(players[currentPlayer] + "'s new location is " + places[currentPlayer]);

            questionHandler.AskQuestion(places[currentPlayer]);
           
            this.ProcessAnswer(luckyNumber);

            return PlayerWon();
        }

        /// <summary>
        /// Performs the change of player.
        /// </summary>
        public void NextPlayer()
        {
            currentPlayer++;
            if (currentPlayer == players.Count)
            {
                currentPlayer = 0;
            }
        }

        /// <summary>
        /// Processes the correct answers.
        /// </summary>
        /// <returns></returns>
        private void ProcessCorrectAnswers()
        {
            if (!inPenaltyBox[currentPlayer])
            {
                Console.WriteLine("Answer was correct!!!!");
                purses[currentPlayer]++;
                Console.WriteLine(players[currentPlayer]+" now has "+ purses[currentPlayer]+ " Gold Coins.");
            }
        }

        /// <summary>
        /// Processes the wrong answers.
        /// </summary>
        private void ProcessWrongAnswers()
        {
            Console.WriteLine("Question was incorrectly answered");
            Console.WriteLine(players[currentPlayer] + " was sent to the penalty box");
            inPenaltyBox[currentPlayer] = true;
        }

        /// <summary>
        /// Players the won.
        /// </summary>
        /// <returns>True if the player won, false otherwise.</returns>
        private bool PlayerWon()
        {
            return purses[currentPlayer] == 6;
        }
    }
}
