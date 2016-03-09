using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Trivia
{
    public class GameRunner
    {
        public static void Main(String[] args)
        {
            PlayGame(new List<string>());
        }

        public static void PlayGame(List<string> players)
        {
            var winner = false;
            Game game = new Game();

            if (players.Count == 0)
            {
                game.add("Chet");
                game.add("Pat");
                game.add("Sue");
            }
            else
            {
                foreach (var player in players)
                {
                    game.add(player);

                }
            }


            // Set a specific seed
            Random rand = new Random(1);

            do
            {
                var dice = rand.Next(5) + 1;
                var luckyNumber = rand.Next(9);

                winner = game.rollTheDice(dice, luckyNumber);

                if (!winner)
                {
                    game.nextPlayer();
                }
            }
            while (!winner);
        }
    }

}

