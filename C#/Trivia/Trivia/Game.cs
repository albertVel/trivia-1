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
        Pop,
        Science,
        Sports,
        Rock
    }

    public class Game
    {
        
        private QuestionHandler questionHandler;

        private GameHandler gameHandler;

        private bool diceHaveBeenRolled = false;

        /// <summary>
        /// Initializes a new instance of the <see cref="Game"/> class.
        /// </summary>
        public Game()
        {
            questionHandler = new QuestionHandler();
            questionHandler.GenerateQuestions();
            gameHandler = new GameHandler();
        }

        /// <summary>
        /// Adds the player.
        /// </summary>
        /// <param name="playerName">Name of the player.</param>
        public void AddPlayer(string playerName)
        {
            gameHandler.AddPlayer(playerName);   
        }

        /// <summary>
        /// Rolls the dice.
        /// </summary>
        /// <param name="dice">The dice.</param>
        /// <param name="luckyNumber">The lucky number.</param>
        public PlayerStatus RollTheDice(int dice, int luckyNumber)
        {
            diceHaveBeenRolled = true;
            this.CheckPreconditionsBeforeRollingTheDice("The game can't start without players");

            Console.WriteLine(gameHandler.PlayerName + " is the current player");
            Console.WriteLine("They have rolled a " + dice);

            if (gameHandler.InPenaltyBox)
            {
                if (dice % 2 != 0)
                {
                    Console.WriteLine(gameHandler.PlayerName + " is getting out of the penalty box");
                    gameHandler.correctAnswer = this.ProcessDice(dice, luckyNumber);
                }
                else
                {
                    Console.WriteLine(gameHandler.PlayerName + " is not getting out of the penalty box");
                }
            }
            else
            {
                gameHandler.correctAnswer = this.ProcessDice(dice, luckyNumber);
            }

            return gameHandler.PlayerStatus;
        }

        private void CheckPreconditionsBeforeRollingTheDice(string exceptionMessage)
        {
            if (gameHandler.NumberOfPlayers == 0)
            {
                throw new Exception(exceptionMessage);
            }
        }

        private void CheckPreconditionsBeforeNextPlayer(string exceptionMessageNoPlayers, string exceptionMessageNotRolledDice)
        {
            if (gameHandler.NumberOfPlayers == 0)
            {
                throw new Exception(exceptionMessageNoPlayers);
            }

            if (!this.diceHaveBeenRolled)
            {
                throw new Exception(exceptionMessageNotRolledDice);

            }
        }

        /// <summary>
        /// Processes the answer.
        /// </summary>
        /// <param name="luckyNumber">The lucky number.</param>
        /// <returns>Returns true if the answer was correct, false otherwise</returns>
        private bool ProcessAnswer(int luckyNumber)
        {
            var correctAnswer = false;
            if (luckyNumber == gameHandler.MagicalNumber)
            {
                this.ProcessWrongAnswers();
            }
            else
            {

                this.ProcessCorrectAnswers();
                correctAnswer = true;

            }

            return correctAnswer;
        }

        /// <summary>
        /// Processes the dice.
        /// </summary>
        /// <param name="roll">The roll.</param>
        /// <param name="luckyNumber">The lucky number.</param>
        /// <returns>Returns true if the answer was correct, false otherwise</returns>
        private bool ProcessDice(int roll, int luckyNumber)
        {
            gameHandler.MovePlayer(roll);
            
            Console.WriteLine(gameHandler.PlayerName + "'s new location is " + gameHandler.PlayerPosition);

            questionHandler.AskQuestion();

            return this.ProcessAnswer(luckyNumber);
        }

        /// <summary>
        /// Performs the change of player.
        /// </summary>
        public void NextPlayer()
        {
            this.CheckPreconditionsBeforeNextPlayer("There are no players.","The dice should have rolled before switching players");
            gameHandler.NextPlayer();     
        }

        /// <summary>
        /// Processes the correct answers.
        /// </summary>
        /// <returns></returns>
        private void ProcessCorrectAnswers()
        {
            Console.WriteLine("Answer was correct!!!!");

            gameHandler.IncreasePursues();
            Console.WriteLine(gameHandler.PlayerName + " now has " + gameHandler.PlayerPursues + " Gold Coins.");

            gameHandler.InPenaltyBox = false;

        }

        /// <summary>
        /// Processes the wrong answers.
        /// </summary>
        private void ProcessWrongAnswers()
        {
            Console.WriteLine("Question was incorrectly answered");
            Console.WriteLine(gameHandler.PlayerName + " was sent to the penalty box");
            gameHandler.InPenaltyBox = true;
        }

        /// <summary>
        /// Players the won.
        /// </summary>
        /// <returns>True if the player won, false otherwise.</returns>
        private bool PlayerWon()
        {
            return gameHandler.PlayerWon;
        }
    }
}
