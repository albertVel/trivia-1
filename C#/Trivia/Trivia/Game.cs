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
        /// Adds the specified player name.
        /// </summary>
        /// <param name="playerName">Name of the player.</param>
        /// <returns></returns>
        public bool add(string playerName)
        {
            players.Add(playerName);
            places[players.Count] = 0;
            purses[players.Count] = 0;
            inPenaltyBox[players.Count] = false;

            Console.WriteLine(playerName + " was added");
            Console.WriteLine("They are player number " + players.Count);
            return true;
        }

        /// <summary>
        /// Rolls the specified dice.
        /// </summary>
        /// <param name="dice">The dice.</param>
        /// <param name="luckyNumber">The lucky number.</param>
        public bool rollTheDice(int dice, int luckyNumber)
        {
            var winner = false;
            Console.WriteLine(players[currentPlayer] + " is the current player");
            Console.WriteLine("They have rolled a " + dice);

            if (inPenaltyBox[currentPlayer])
            {
                if (dice % 2 != 0)
                {
                    Console.WriteLine(players[currentPlayer] + " is getting out of the penalty box");
                    this.processDice(dice);
                }
                else
                {
                    Console.WriteLine(players[currentPlayer] + " is not getting out of the penalty box");
                }
            }
            else
            {
                this.processDice(dice);
            }

            winner = processLuckyNumber(luckyNumber);

            return winner;
        }

        /// <summary>
        /// Processes the lucky number.
        /// </summary>
        /// <param name="luckyNumber">The lucky number.</param>
        /// <returns></returns>
        private bool processLuckyNumber(int luckyNumber)
        {
            var winner = false;

            if (luckyNumber == MagicalNumber)
            {
                winner = this.wrongAnswer();
            }
            else
            {
                winner = this.wasCorrectlyAnswered();
            }
            return winner;
        }


        /// <summary>
        /// Processes the dice.
        /// </summary>
        /// <param name="roll">The roll.</param>
        /// <param name="luckyNumber">The lucky number.</param>
        /// <returns></returns>
        private void processDice(int roll)
        {

            places[currentPlayer] = places[currentPlayer] + roll;
            if (places[currentPlayer] > 11) places[currentPlayer] = places[currentPlayer] - 12;

            Console.WriteLine(players[currentPlayer] + "'s new location is " + places[currentPlayer]);
            Console.WriteLine("The category is " + currentCategory());
            askQuestion();
        }

        /// <summary>
        /// Asks the question.
        /// </summary>
        private void askQuestion()
        {
            if (currentCategory() == Category.Pop)
            {
                Console.WriteLine(popQuestions.First());
                popQuestions.RemoveAt(0);
            }
            if (currentCategory() == Category.Science)
            {
                Console.WriteLine(scienceQuestions.First());
                scienceQuestions.RemoveAt(0);
            }
            if (currentCategory() == Category.Sports)
            {
                Console.WriteLine(sportsQuestions.First());
                sportsQuestions.RemoveAt(0);
            }
            if (currentCategory() == Category.Rock)
            {
                Console.WriteLine(rockQuestions.First());
                rockQuestions.RemoveAt(0);
            }
        }

        /// <summary>
        /// Currents the category.
        /// </summary>
        /// <returns></returns>
        private Category currentCategory()
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
        /// Wases the correctly answered.
        /// </summary>
        /// <returns></returns>
        public bool wasCorrectlyAnswered()
        {
            var winner = false;
            if (inPenaltyBox[currentPlayer])
            {
                currentPlayer++;
                if (currentPlayer == players.Count) currentPlayer = 0;
                winner = true;
            }
            else
            {
                Console.WriteLine("Answer was corrent!!!!");
                purses[currentPlayer]++;
                Console.WriteLine(players[currentPlayer]
                        + " now has "
                        + purses[currentPlayer]
                        + " Gold Coins.");

                winner = didPlayerWin();
                currentPlayer++;
                if (currentPlayer == players.Count) currentPlayer = 0;


            }
            return winner;
        }

        /// <summary>
        /// Wrongs the answer.
        /// </summary>
        /// <returns></returns>
        public bool wrongAnswer()
        {
            Console.WriteLine("Question was incorrectly answered");
            Console.WriteLine(players[currentPlayer] + " was sent to the penalty box");
            inPenaltyBox[currentPlayer] = true;

            currentPlayer++;
            if (currentPlayer == players.Count) currentPlayer = 0;
            return true;
        }

        /// <summary>
        /// Dids the player win.
        /// </summary>
        /// <returns></returns>
        private bool didPlayerWin()
        {
            return purses[currentPlayer] != 6;
        }
    }
}
