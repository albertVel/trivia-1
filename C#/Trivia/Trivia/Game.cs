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

        List<string> popQuestions = new List<string>();
        List<string> scienceQuestions = new List<string>();
        List<string> sportsQuestions = new List<string>();
        List<string> rockQuestions = new List<string>();

        int currentPlayer = 0;

        /// <summary>
        /// Initializes a new instance of the <see cref="Game"/> class.
        /// </summary>
        public Game()
        {
            for (int i = 0; i < 50; i++)
            {
                popQuestions.Add("Pop Question " + i);
                scienceQuestions.Add("Science Question " + i);
                sportsQuestions.Add("Sports Question " + i);
                rockQuestions.Add("Rock Question " + i);
            }
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
            Console.WriteLine("The category is " + CurrentCategory());
            
            AskQuestion(luckyNumber);

            return PlayerWon();
        }

        /// <summary>
        /// Asks the question.
        /// </summary>
        /// <param name="luckyNumber">The lucky number.</param>
        private void AskQuestion(int luckyNumber)
        {
            if (CurrentCategory() == Category.Pop)
            {
                Console.WriteLine(popQuestions.First());
                popQuestions.RemoveAt(0);
            }
            if (CurrentCategory() == Category.Science)
            {
                Console.WriteLine(scienceQuestions.First());
                scienceQuestions.RemoveAt(0);
            }
            if (CurrentCategory() == Category.Sports)
            {
                Console.WriteLine(sportsQuestions.First());
                sportsQuestions.RemoveAt(0);
            }
            if (CurrentCategory() == Category.Rock)
            {
                Console.WriteLine(rockQuestions.First());
                rockQuestions.RemoveAt(0);
            }

            this.ProcessAnswer(luckyNumber);
        }

        /// <summary>
        /// Retrieves the current category.
        /// </summary>
        /// <returns>Returns the category</returns>
        private Category CurrentCategory()
        {
            Category category = Category.Unknown;

            if ((places[currentPlayer] == 0) || (places[currentPlayer] == 4) || (places[currentPlayer] == 8))
            {
                category = Category.Pop;
            }

            if ((places[currentPlayer] == 1) || (places[currentPlayer] == 5) || (places[currentPlayer] == 9))
            {
                category = Category.Science;
            }

            if ((places[currentPlayer] == 2) || (places[currentPlayer] == 6) || (places[currentPlayer] == 10))
            {
                category = Category.Sports;
            }

            if (category == Category.Unknown)
            {
                category = Category.Rock;
            }

            return category;
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
