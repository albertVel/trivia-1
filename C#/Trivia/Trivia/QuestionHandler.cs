using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Trivia
{
    class QuestionHandler
    {
        List<string> popQuestions;
        List<string> scienceQuestions;
        List<string> sportsQuestions;
        List<string> rockQuestions;

        public QuestionHandler()
        {
            popQuestions= new List<string>();
            scienceQuestions = new List<string>();
            sportsQuestions = new List<string>();
            rockQuestions = new List<string>();
        }

        internal void GenerateQuestions()
        {
            for (int i = 0; i < 50; i++)
            {
                popQuestions.Add("Pop Question " + i);
                scienceQuestions.Add("Science Question " + i);
                sportsQuestions.Add("Sports Question " + i);
                rockQuestions.Add("Rock Question " + i);
            }
        }

        internal void AskQuestion(int placeCurrentPlayer)
        {
            var currentCategory = this.CurrentCategory(placeCurrentPlayer);

            if (currentCategory == Category.Pop)
            {
                Console.WriteLine(popQuestions.First());
                popQuestions.RemoveAt(0);
            }
            if (currentCategory == Category.Science)
            {
                Console.WriteLine(scienceQuestions.First());
                scienceQuestions.RemoveAt(0);
            }
            if (currentCategory == Category.Sports)
            {
                Console.WriteLine(sportsQuestions.First());
                sportsQuestions.RemoveAt(0);
            }
            if (currentCategory == Category.Rock)
            {
                Console.WriteLine(rockQuestions.First());
                rockQuestions.RemoveAt(0);
            }
        }

        internal Category CurrentCategory(int placeCurrentPlayer)
        {
            
            Category category = Category.Unknown;

            if ((placeCurrentPlayer == 0) || (placeCurrentPlayer == 4) || (placeCurrentPlayer == 8))
            {
                category = Category.Pop;
            }

            if ((placeCurrentPlayer == 1) || (placeCurrentPlayer == 5) || (placeCurrentPlayer == 9))
            {
                category = Category.Science;
            }

            if ((placeCurrentPlayer == 2) || (placeCurrentPlayer == 6) || (placeCurrentPlayer == 10))
            {
                category = Category.Sports;
            }

            if (category == Category.Unknown)
            {
                category = Category.Rock;
            }

            Console.WriteLine("The category is " + category);

            return category;
        }
    }
}
