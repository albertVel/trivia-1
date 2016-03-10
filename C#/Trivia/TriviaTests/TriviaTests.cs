using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TriviaTests
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Security.Cryptography;

    using Trivia;

    [TestClass]
    public class TriviaTests
    {
        [TestMethod]
        public void EntersAndLeavesPenaltyBox()
        {
            Game game = new Game();

            game.AddPlayer("Alf");

            PlayerStatus playerStatus;

            //Wrong answer enters the penalty box
            playerStatus = game.RollTheDice(4, 7);
            CompareActualPlayerStatusWithExpectedOne(playerStatus, correctAnswer: false, inPenaltyBox: true, won: false, name: "Alf");
            game.NextPlayer();

            //In penalty box, and doesn't leave penalty box
            playerStatus = game.RollTheDice(2, 3);
            CompareActualPlayerStatusWithExpectedOne(playerStatus, correctAnswer: false, inPenaltyBox: true, won: false, name: "Alf");
            game.NextPlayer();

            //Leaves penalty box, answer correct
            playerStatus = game.RollTheDice(1, 2);
            CompareActualPlayerStatusWithExpectedOne(playerStatus, correctAnswer: true, inPenaltyBox: false, won: false, name: "Alf");
            game.NextPlayer();

        }

        [TestMethod]
        public void CorrectAnswerNoPenaltyBox()
        {
            Game game = new Game();

            game.AddPlayer("Alf");

            PlayerStatus playerStatus;

            //Correct answer do not enter the penalty box
            playerStatus = game.RollTheDice(4, 3);
            CompareActualPlayerStatusWithExpectedOne(playerStatus, correctAnswer: true, inPenaltyBox: false, won: false, name: "Alf");

        }


        [TestMethod]
        public void SixCorrectAnswerNoPenaltyBoxAndWin()
        {
            Game game = new Game();

            game.AddPlayer("Alf");
            PlayerStatus playerStatus;

            for (int i = 0; i < 5; i++)
            {
                //Correct answer do not enter the penalty box
                playerStatus = game.RollTheDice(4, 3);
                game.NextPlayer();
                CompareActualPlayerStatusWithExpectedOne(playerStatus, correctAnswer: true, inPenaltyBox: false, won: false, name: "Alf");

                game.ShowGameData();


            }

            //Correct answer wins
            playerStatus = game.RollTheDice(4, 3);
            CompareActualPlayerStatusWithExpectedOne(playerStatus, correctAnswer: true, inPenaltyBox: false, won: true, name: "Alf");


        }


        [TestMethod]
        public void TwoPlayersOneEntersPenaltyBoxOtherDont()
        {
            Game game = new Game();

            game.AddPlayer("Alf");
            game.AddPlayer("Spiderman");

            PlayerStatus playerStatus;

            //Alf wrong answer enters the penalty box
            playerStatus = game.RollTheDice(4, 7);
            CompareActualPlayerStatusWithExpectedOne(playerStatus, correctAnswer: false, inPenaltyBox: true, won: false, name: "Alf");

            game.NextPlayer();

            //Spiderman Correct answer do not enter the penalty box
            playerStatus = game.RollTheDice(4, 3);
            CompareActualPlayerStatusWithExpectedOne(playerStatus, correctAnswer: true, inPenaltyBox: false, won: false, name: "Spiderman");

        }

        [ExpectedException(typeof(Exception))]
        [TestMethod]
        public void RollTheDiceNoPlayersRaiseException()
        {
            Game game = new Game();

            game.RollTheDice(4, 7);
        }

        [ExpectedException(typeof(Exception))]
        [TestMethod]
        public void NextPlayerNoPlayersRaiseException()
        {
            Game game = new Game();

            game.NextPlayer();
        }

        [ExpectedException(typeof(Exception))]
        [TestMethod]
        public void NextPlayerWithoutRollingDiceRaiseException()
        {
            Game game = new Game();
            game.AddPlayer("Alf");

            game.NextPlayer();
        }

        /// <summary>
        /// Compares the actual player status with expected one.
        /// </summary>
        /// <param name="playerStatus">The player status.</param>
        /// <param name="correctAnswer">if set to <c>true</c> [correct answer].</param>
        /// <param name="inPenaltyBox">if set to <c>true</c> [in penalty box].</param>
        /// <param name="won">if set to <c>true</c> [won].</param>
        /// <param name="name">The name.</param>
        private void CompareActualPlayerStatusWithExpectedOne(PlayerStatus playerStatus, bool correctAnswer, bool inPenaltyBox, bool won, string name)
        {
            Assert.AreEqual(correctAnswer, playerStatus.correctAnswer, "The expected correctAnswer is: {0}, and the real one is {1}", correctAnswer, playerStatus.correctAnswer);
            Assert.AreEqual(inPenaltyBox, playerStatus.inPenaltyBox, "The expected inPenaltyBox is: {0}, and the real one is {1}", inPenaltyBox, playerStatus.inPenaltyBox);
            Assert.AreEqual(won, playerStatus.won, "The expected won is: {0}, and the real one is {1}", won, playerStatus.won);
            Assert.AreEqual(
                name,
                playerStatus.name,
                "The expected name is: {0}, and the real one is {1}",
                name,
                playerStatus.name);
        }

    }
}
