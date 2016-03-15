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
        /// Shows the game data.
        /// </summary>
        /// <returns>The number of plays displayed.</returns>
        /// <exception cref="Exception">There's no game data to show.</exception>
        public int ShowGameData()
        {
            if (gameHandler.GameData.Count == 0)
            {
                throw new Exception("There's no game data to show.");
            }


            Console.WriteLine("--- This is the actual Game Data ---");
            foreach (var playerTurn in gameHandler.GameData)
            {
                    
                Console.WriteLine("Player {0} rolled a {1} and the answer was {2}, has {3} coins and he/she {4}", playerTurn.name, playerTurn.dice, playerTurn.correctAnswerString, playerTurn.pursues, playerTurn.wonString);
            }

            Console.WriteLine("--- End of Game Data ---");

            return gameHandler.GameData.Count;

        }

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
        /// <returns>Returns the Status of the current play.</returns>
        public PlayerStatus RollTheDice(int dice, int luckyNumber)
        {
            gameHandler.dice = dice;

            diceHaveBeenRolled = true;
            this.CheckPreconditionsBeforeRollingTheDice();

            Console.WriteLine(gameHandler.PlayerName + " is the current player");
            Console.WriteLine(gameHandler.PlayerName +" rolled a " + dice);

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

            gameHandler.StorePlayerStatus();

            return gameHandler.PlayerStatus;
        }

        private void CheckPreconditionsBeforeRollingTheDice()
        {
            if (gameHandler.NumberOfPlayers == 0)
            {
                throw new Exception("The game can't start without players");
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

            if (gameHandler.OnePlayerWon)
            {
                throw new Exception("The game can't go on as we have already a winner!");
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
    }
}
