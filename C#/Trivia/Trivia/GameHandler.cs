using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Trivia
{
    public class GameHandler
    {
        const int MaxPlayers = 6;
        public int MagicalNumber = 7;
        private List<string> players = null;
        private int[] places = null;
        private int[] purses = null;
        bool[] inPenaltyBox = null;
        int currentPlayer = -1;


        public GameHandler()
        {
            players = new List<string>();
            places = new int[MaxPlayers];
            purses = new int[MaxPlayers];
            inPenaltyBox = new bool[MaxPlayers];
            currentPlayer = 0;
            this.GameData = new List<PlayerStatus>();
            this.OnePlayerWon = false;

        }

        internal void AddPlayer(string playerName)
        {
            players.Add(playerName);
            places[players.Count] = 0;
            purses[players.Count] = 0;
            inPenaltyBox[players.Count] = false;
            

            Console.WriteLine(playerName + " was added");
            Console.WriteLine(playerName + " is the player " + players.Count);
        }

        public List<PlayerStatus> GameData { get; private set; }

        internal string PlayerName
        {
            get
            {
                return this.players[currentPlayer];
            }
        }

        internal bool InPenaltyBox
        {
            get
            {
                return this.inPenaltyBox[currentPlayer];
            }
            set
            {
                this.inPenaltyBox[currentPlayer] = value;
            }
        }

        internal int NumberOfPlayers
        {
            get
            {
                return this.players.Count;
            }
        }

        internal int PlayerPosition
        {
            get
            {
                return this.places[currentPlayer];
            }
        }

        internal void MovePlayer(int roll)
        {
            places[currentPlayer] = places[currentPlayer] + roll;
            if (places[currentPlayer] > 11) places[currentPlayer] = places[currentPlayer] - 12;
        }

        internal void NextPlayer()
        {
            currentPlayer++;
            if (currentPlayer == players.Count)
            {
                currentPlayer = 0;
            }
        }

        internal void IncreasePursues()
        {
            purses[currentPlayer]++;
        }

        public int PlayerPursues {
            get
            {
                return this.purses[currentPlayer];
            }
        }

        public bool PlayerWon {
            get
            {
                return purses[currentPlayer] == 6;
            }
        }

        public PlayerStatus PlayerStatus {
            
            get
            {
                PlayerStatus playerStatus = new PlayerStatus();
                playerStatus.name = this.PlayerName;
                playerStatus.correctAnswer = this.correctAnswer;
                playerStatus.inPenaltyBox = this.InPenaltyBox;
                playerStatus.won = this.PlayerWon;
                playerStatus.pursues = this.PlayerPursues;
                playerStatus.dice = this.dice;
                
                
                return playerStatus;
            }
        }

        public bool OnePlayerWon { get; private set;}

        public bool correctAnswer { get; set; }

        internal void StorePlayerStatus()
        {
            if (this.PlayerWon)
            {
                this.OnePlayerWon = true;
            }
            GameData.Add(this.PlayerStatus);
        }

        public int dice { get; set; }
    }
}
