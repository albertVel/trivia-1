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

        private Random rand;

        public QuestionHandler()
        {
            popQuestions= new List<string>();
            scienceQuestions = new List<string>();
            sportsQuestions = new List<string>();
            rockQuestions = new List<string>();
            rand = new Random(3);

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

        internal void AskQuestion()
        {
            var currentCategory = this.CurrentCategory();

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

        internal Category CurrentCategory()
        {
            // Set a specific seed
            var luckyNumber = rand.Next(1000);

            Category category = (Category)(luckyNumber%3);

            Console.WriteLine("The category is " + category);

            return category;
        }
    }
}
