using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace GameProg_TriviaGame_Tweedale
{
    internal class TriviaQuestion
    {
        private static Random rand;
        public string Question;
        public string Answer;
        public string FalseAnswer1;
        public string FalseAnswer2;
        public string FalseAnswer3;
        public string[] ShuffledAnswers = new string[4];

        // Did not know before this project that you can create a separate static constructor for static properties
        // Found this out because, since all class instances were being created at the same time, their Random all had the same time seed
        // So the correct answer would always be the exact same number on all of them
        // By doing this, we're only using one static Random object instead of 10+ instances of Random with the exact same seed, giving us different answer indexes
        static TriviaQuestion()
        {
            rand = new Random();
        }

        public TriviaQuestion(string question, string answer, string false1, string false2, string false3) 
        {
            Question = question;
            Answer = answer;
            FalseAnswer1 = false1;
            FalseAnswer2 = false2;
            FalseAnswer3 = false3;

            ShuffledAnswers = RandomlySortAnswers();
        }

        private string[] RandomlySortAnswers() 
        {
            string[] inOrderAnswers = new string[4];
            string[] randomlySortedAnswers = new string[4];

            inOrderAnswers[0] = Answer;
            inOrderAnswers[1] = FalseAnswer1;
            inOrderAnswers[2] = FalseAnswer2;
            inOrderAnswers[3] = FalseAnswer3;

            bool shuffleComplete = false;
            int answersAdded = 0;

            while (!shuffleComplete) 
            {
                int i = rand.Next(0, 4);

                if (!randomlySortedAnswers.Contains(inOrderAnswers[i])) 
                {
                    randomlySortedAnswers[answersAdded] = inOrderAnswers[i];

                    answersAdded++;
                }

                if (answersAdded == 4) 
                {
                    shuffleComplete = true;
                }
            }

            return randomlySortedAnswers;
        }
    }

    internal class Program
    {
        static TriviaQuestion[] triviaQuestions =
        {
            new TriviaQuestion("test question1?", "true answer", "false1", "false2", "false3"),
            new TriviaQuestion("test question2", "true answer2", "not me", "not me either", "not me either either"),
            new TriviaQuestion("test question3?", "true answer", "false1", "false2", "false3"),
            new TriviaQuestion("test question4?", "true answer", "false1", "false2", "false3"),
            new TriviaQuestion("test question5?", "true answer", "false1", "false2", "false3"),
            new TriviaQuestion("test question6?", "true answer", "false1", "false2", "false3"),
            new TriviaQuestion("test question7?", "true answer", "false1", "false2", "false3"),
            new TriviaQuestion("test question8?", "true answer", "false1", "false2", "false3"),
            new TriviaQuestion("test question9?", "true answer", "false1", "false2", "false3"),
            new TriviaQuestion("test question10?", "true answer", "false1", "false2", "false3"),
            new TriviaQuestion("test question11?", "true answer", "false1", "false2", "false3"),
            new TriviaQuestion("test question12?", "true answer", "false1", "false2", "false3"),
            new TriviaQuestion("test question13?", "true answer", "false1", "false2", "false3"),
            new TriviaQuestion("test question14?", "true answer", "false1", "false2", "false3"),
            new TriviaQuestion("test question15?", "true answer", "false1", "false2", "false3")
        };

        static string name;

        static void Main(string[] args)
        {
            name = EnterName();

            GameLoop();
        }

        static void GameLoop() 
        {
            Console.Clear();

            int totalQuestions = 10;
            int score = 0;
            TriviaQuestion[] questions = GenerateQuestionList(totalQuestions);

            for (int q = 0; q < totalQuestions; q++)
            {
                DisplayHUD(totalQuestions, score);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write($"Question {q + 1}. ");

                bool correctlyAnswered = AskQuestion(questions[q]);

                if (correctlyAnswered)
                {
                    score++;
                }

                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                Console.Clear();
            }

            Results(score, totalQuestions);
        }

        static int GetPercentage(int numerator, int denominator)
        {
            return (int)Math.Round( ((double)numerator / (double)denominator) * 100);
        }

        static void Results(int score, int totalQuestions) 
        {
            Console.Clear();

            DisplayHUD(totalQuestions, score);

            int percent = GetPercentage(score, totalQuestions);

            Console.ForegroundColor = ConsoleColor.White;

            if (percent == 100)
            {
                Console.Write("Wow! Good job! You got ");

                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("100%");

                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("!!! You know your stuff!");
            }
            else if (percent >= 70)
            {
                Console.Write("Okay, okay. You got ");

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write($"{percent}%");

                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("! Not bad, but you can do better than that!");
            }
            else if (percent >= 40)
            {
                Console.Write("Well, you got some right. ");

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write($"{percent}%");

                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine(" in fact! You should study a bit more.");
                Console.WriteLine("Come to my drop-in sessions more often and ask for help!");
            }
            else if (percent > 0)
            {
                Console.Write("Look, you only got ");

                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write($"{percent}%");

                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("... Are you doing okay? Do you need help?");
                Console.WriteLine("Come to my drop-in sessions more often and ask for help!");
            }
            else 
            {
                Console.Write("Buddy... You got ");

                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("NOTHING");

                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("??!? You gotta be trolling...");
            }

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\nDo you want to try again? \nQuestions are randomized and you'll likely see new ones!\n");
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("Y = Yes | N = No");

            bool readingInput = true;
            ConsoleKey input = ConsoleKey.Z;

            while (readingInput) 
            {
                input = Console.ReadKey(true).Key;

                if (input == ConsoleKey.N || input == ConsoleKey.Y) 
                {
                    readingInput = false;
                }
            }

            if (input == ConsoleKey.Y) 
            {
                GameLoop();
            }
            
        }

        static TriviaQuestion[] GenerateQuestionList(int count) 
        {
            TriviaQuestion[] questionList = new TriviaQuestion[count];

            Random rand = new Random();

            for (int i = 0; i < count; i++) 
            {
                bool added = false;

                while (!added) 
                {
                    int q = rand.Next(0, triviaQuestions.Length);

                    if (!questionList.Contains(triviaQuestions[q])) 
                    {
                        questionList[i] = triviaQuestions[q];
                        added = true;
                    }
                }
            }

            return questionList;
        }

        static void DisplayHUD(int totalQuestions, int score) 
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write($"{name}");
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write("   |   ");

            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write($"{GetPercentage(score, totalQuestions)}%");
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write("   |   ");

            Console.ForegroundColor = ConsoleColor.Green;
            for (int i = 0; i < score; i++) 
            {
                Console.Write("▒");
            }

            Console.ForegroundColor = ConsoleColor.DarkGray;
            for (int i = 0; i < totalQuestions - score; i++)
            {
                Console.Write("▒");
            }

            Console.WriteLine("\n");
        }

        static bool AskQuestion(TriviaQuestion question) 
        {
            bool correctlyAnswered = false;

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(question.Question);
            Console.WriteLine();

            for (int a = 0; a < 4; a++)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write($"{a + 1}. ");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine(question.ShuffledAnswers[a]);
            }

            char input = Console.ReadKey(true).KeyChar;
            int inputInt = (int)Char.GetNumericValue(input) - 1;

            Console.WriteLine();

            if (question.ShuffledAnswers[inputInt] == question.Answer)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("That's the right answer! Good job!");
                correctlyAnswered = true;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("No you doof!!!");
            }

            return correctlyAnswered;
        }

        static string EnterName() 
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("Enter your name: ");
            Console.ForegroundColor = ConsoleColor.White;

            string name = "";
            bool validEntry = false;

            while (!validEntry) 
            {
                name = Console.ReadLine();

                if (name.Length > 12)
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Too long!!! Gimme a name that's 12 characters or less!");
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("Enter your name: ");
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else if (name.Length == 0)
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("You didn't even enter a name!");
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("Enter your name: ");
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else 
                {
                    validEntry = true;
                    return name;
                }
            }

            
            return "John Trivia";
        }
    }
}
