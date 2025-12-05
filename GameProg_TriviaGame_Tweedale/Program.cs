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
            new TriviaQuestion("Which of these data types DOESN'T inherently represent a number?",
                "bool", "int", "double", "float"),
            new TriviaQuestion("What do you call a method that calls itself?",
                "Recursive", "Repeating", "Self-Calling", "Skibidi"),
            new TriviaQuestion("In a 2D array called 'map', how would you access the top left value?",
                "map[0,0]", "map(0,0)", "map[1][1]", "map[1,1]"),
            new TriviaQuestion("What is the difference between '=' and '==' in programming?", 
                "'=' sets the left value to the right value, '==' checks if two values are the same", 
                "'=' checks if two values are the same, '==' sets the left value to the right value", 
                "They do the same thing", 
                "I honestly don't remember"),
            new TriviaQuestion("What does putting a $ before a string do?", 
                "Let's you put a variable directly in the string", 
                "Tells the user that this string represents money", 
                "Sets the string to static", 
                "Converts the string to uppercase"),
            new TriviaQuestion("How do you format a 'for loop' to loop 10 times in C#?", 
                "for(int i=0; i<10; i++)", 
                "foreach(i<10)", 
                "for(i = 0; i>11; i++)", 
                "for int i = 10; i < 10"),
            new TriviaQuestion("In a list called 'names', how do you get the 10th item?",
                "names[9]", "names[10]", "names[11]", "names.Find(10)"),
            new TriviaQuestion("How do you write a comment in C#?",
                "// This is a comment", "# This is a comment", "-- This is a comment", "/ This is a comment"),
            new TriviaQuestion("How do you say 'AND' in an if statement?",
                "&&", "||", "and", "++"),
            new TriviaQuestion("How do you print a string to the console?",
                "Console.Write()", "Console.Print()", "WriteLine()", "Console.Output()"),
            new TriviaQuestion("What does the error 'System.IndexOutOfRangeException' tell you?", 
                "I'm trying to access a position in an array that's greater than it's size", 
                "I'm trying to use a decimal number on an integer", 
                "I'm setting an invalid range in the Random.Range() method", 
                "This isn't a real error message. You're trying to trick me and I won't fall for it."),
            new TriviaQuestion("What is the difference between the data types 'int' and 'bigint' in C#?", 
                "bigint isn't a real data type in C#", 
                "bigint has a larger max limit than int", 
                "bigint uses 8 bytes while int uses 4", 
                "None of the other answers are true"),
            new TriviaQuestion("Which word is used to return a value inside a method?",
                "return", "send", "continue", "output"),
            new TriviaQuestion("If I'm just comparing one variable, instead of a bunch of 'if' statements, I can use a ____ statement.", 
                "switch", "when", "default", "break"),
            new TriviaQuestion("Which statement is used to stop a loop?", "break",
                "exit", "continue", "stop"),
            new TriviaQuestion("What data type is used to store text?", "string",
                "str", "text", "char"),
            new TriviaQuestion("How do you declare an int array named 'myArray'?",
                "int[] myArray", "int array myArray", "int myArray[]", "array int myArray"),
            new TriviaQuestion("What is the name of the 'special' class that represents a group of constants?",
                "enum", "const", "special", "group"),
            new TriviaQuestion("What operator is used to get the remainder of one number divided by another? (AKA modulo)",
                "%", "/", "#", "$"),
            new TriviaQuestion("My function 'MyFloat' outputs a float, but I want to store it as an int! How do I cast it as an int?", 
                "(int) MyFloat()", "MyFloat().Cast(int)", "MyFloat(int)", "MyFloat().ToInt()"),
            new TriviaQuestion("How do you get the console input of a single key press?", 
                "Console.ReadKey()", "Console.ReadLine()", "Console.Key()", "Console.ReadSingleKey()"),
            new TriviaQuestion("Who is your favourite instructor?",
                "Simon", "Not Simon", "Anybody but Simon", "I don't have a favourite"),
            new TriviaQuestion("If a for loop could loop four, how much four would a for loop for?",
                "3", "4", "1", "2"),
            new TriviaQuestion("Using the following for loop through an array called 'names', how would you access each name?\n\nfor (int i = 0; i < names.Length; i++)",
                "names[i]", "names[name]", "names[0]", "i"),
            new TriviaQuestion("What operator is used to say 'NOT' when used before a boolean?",
                "!", "X", "~", "-"),
            new TriviaQuestion("What character do you put at the end of most statements in C#?",
                ";", ".", ",", ":"),
            new TriviaQuestion("What function is used to get the lower of two numbers?",
                "Math.Min()", "Math.Max()", "Math.Lower()", "Math.IWouldLikeTheLowestNumberOfTheseTwoPleaseIfYouDon'tMindThankYou()"),
            new TriviaQuestion("You have an array of strings called 'strings' and you want to get the 3rd char in the 2nd string.\nHow would you do that?",
                "strings[1][2]", "strings[1, 2]", "strings[2, 1]", "strings[2, 3]"),
            new TriviaQuestion("Using a path saved a string named 'path', how would you read a file to get a string array?", 
                "File.ReadAllLines(path)", "path.Read()", "File.Read(path)", "ReadFile(path)"),
            new TriviaQuestion("How do you set the console cursor to be on the 4th row, all the way to the left?",
                "Console.SetCursorPosition(0, 3)", "Console.SetCursorPosition(3, 0)", "SetCursorPosition(3, 0)", "Console.SetCursor(3, 0)")
        };

        static string name;
        static int startingQuestions = 10;
        static int nameMaxLength = 24;

        static void Main(string[] args)
        {
            name = EnterName();

            GameLoop(startingQuestions);
        }

        static void GameLoop(int totalQuestions) 
        {
            Console.Clear();
            Console.CursorVisible = false;

            int score = 0;
            TriviaQuestion[] questions = GenerateQuestionList(totalQuestions);

            for (int q = 0; q < totalQuestions; q++)
            {
                DisplayHUD(q+1, totalQuestions, score);
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

            DisplayHUD(totalQuestions, totalQuestions, score);

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
                int questions = startingQuestions;
                bool loopForInput = true;

                Console.WriteLine();

                while (loopForInput)
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("How many questions do you want to answer?");
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.WriteLine($"Enter a number between 1 and {triviaQuestions.Length}");

                    Console.CursorVisible = true;

                
                    if (int.TryParse(Console.ReadLine(), out questions))
                    {
                        if (questions > 0 && questions <= triviaQuestions.Length)
                        {
                            loopForInput = false;
                        }
                        else
                        {
                            Console.Clear();
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine($"I said between 1 and {triviaQuestions.Length}!");
                        }
                    }
                    else 
                    {
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("A number!! Enter a number!!!");
                    }
                }

                GameLoop(questions);
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

        static void DisplayHUD(int currentQuestion, int totalQuestions, int score) 
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write($"{name}");
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write("   |   ");

            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write($"Q {currentQuestion} / {totalQuestions}");

            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write("   |   ");

            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write($"{GetPercentage(score, totalQuestions)}%   ");

            Console.ForegroundColor = ConsoleColor.Green;
            for (int i = 0; i < score; i++) 
            {
                Console.Write("▒▒");
            }

            Console.ForegroundColor = ConsoleColor.DarkGray;
            for (int i = 0; i < totalQuestions - score; i++)
            {
                Console.Write("▒▒");
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

            char input = GetAnswerInput();
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
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("The correct answer is: ");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(question.Answer);
            }

            return correctlyAnswered;
        }

        static char GetAnswerInput() 
        {
            char[] validInputs = { '1', '2', '3', '4' };
            bool readingInput = true;
            bool sixEntered = false;
            bool sixSevenEntered = false;
            char input = '9';

            while (readingInput) 
            {
                input = Console.ReadKey(true).KeyChar;

                if (validInputs.Contains(input))
                {
                    readingInput = false;
                }
                else if (input == '6')
                {
                    sixEntered = true;
                }
                else if (!sixSevenEntered && sixEntered && input == '7')
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("\nSIX SEVEN!!!!!!!");
                    sixSevenEntered = true;
                }
                else 
                {
                    sixEntered = false;
                }
            }

            return input;
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

                if (name.Length > nameMaxLength)
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Too long!!! Gimme a name that's {nameMaxLength} characters or less!");
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
