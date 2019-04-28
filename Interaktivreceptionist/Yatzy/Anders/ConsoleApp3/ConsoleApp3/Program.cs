using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp3
{
    class Program
    {
        private static readonly String[] UPPER_SECTION_SCORES = {"ones", "twos", "threes", "fours", "fives", "sixes"};
        private static readonly String[] LOWER_SECTION_SCORES = {"pair1", "pair2", "threealike", "fouralike", "smallstraight", "largestraight", "fullhouse", "chance", "yatzy"};
        
        private static readonly int[] SMALL_STRAIGHT = {1, 2, 3, 4, 5};
        private static readonly int[] LARGE_STRAIGHT = {2, 3, 4, 5, 6};

        static void Main(string[] args)
        {
            // Set console output encoding to UTF8 to support UTF8 characters.
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            int rolls = 0;
            Random random = new Random();
            List<IDie> dice = new List<IDie>();
            int totalScore = 0;
            Dictionary<String, Func<List<IDie>, int>> functionDictionary = new Dictionary<string, Func<List<IDie>, int>>
            {
                { "ones", Ones },
                { "twos", Twos },
                { "threes", Threes },
                { "fours", Fours },
                { "fives", Fives },
                { "sixes", Sixes },
                { "pair1", Pair1 },
                { "pair2", Pair2 },
                { "threealike", ThreeAlike },
                { "fouralike", FourAlike },
                { "smallstraight", SmallStraight },
                { "largestraight", LargeStraight },
                { "fullhouse", FullHouse },
                { "chance", Chance },
                { "yatzy", Yatzy }
            };
            Dictionary<String, int> scoreDictionary = new Dictionary<string, int>
            {
                { "ones", -1 },
                { "twos", -1 },
                { "threes", -1 },
                { "fours", -1 },
                { "fives", -1 },
                { "sixes", -1 },
                { "bonus", -1 },
                { "pair1", -1 },
                { "pair2", -1 },
                { "threealike", -1 },
                { "fouralike", -1 },
                { "smallstraight", -1 },
                { "largestraight", -1 },
                { "fullhouse", -1 },
                { "chance", -1 },
                { "yatzy", -1 }
            };

            // Instantiate 5 dice
            for (int i = 0; i < 10; i++)
            {
                dice.Add(new Die());
            }

            // This loop goes on until a score has been set for every score there is
            while (scoreDictionary.ContainsValue(-1))
            {
                Console.WriteLine("Score:");
                DrawScores(scoreDictionary, totalScore);
                Console.WriteLine();

                // This loop goes on until 3 rolls have been made
                while (rolls < 3)
                {
                    foreach (IDie die in dice)
                    {
                        if (!die.Locked)
                            die.Roll(random);
                        die.Unlock();
                    }

                    dice.Sort();
                    rolls++;

                    Console.WriteLine($"Roll {rolls}:");
                    DrawDice(dice);

                    String lockedDiceLine = Console.ReadLine();
                    String[] lockedDice = lockedDiceLine.Split(' ');

                    // Check if locked dice are either 1, 2, 3, 4, or 5
                    if (lockedDice[0] != "")
                    {
                        foreach (String lockedDie in lockedDice)
                        {
                            int lockedDieIndex = int.Parse(lockedDie) - 1;
                            dice[lockedDieIndex].Lock();
                        }
                    }
                }

                String[] scoreLine = Console.ReadLine().Split(' ');

                bool notValidInput = true;

                while (notValidInput)
                {
                    if (scoreLine.Length == 1)
                    {
                        if (!functionDictionary.ContainsKey(scoreLine[0]))
                        {
                            Console.WriteLine("The entered score does not exist. Try again.");
                            scoreLine = Console.ReadLine().Split(' ');
                        }
                        else
                        {
                            String scoreKey = scoreLine[0];

                            if (LOWER_SECTION_SCORES.Contains(scoreKey) && !CheckUpperSectionFinished(scoreDictionary))
                            {
                                Console.WriteLine("Upper section not finished. Try again.");
                                scoreLine = Console.ReadLine().Split(' ');
                            }
                            else
                            {

                                try
                                {
                                    int score = functionDictionary[scoreKey](dice.Where((die) => die.Locked).ToList());
                                }
                                catch (Exception e)
                                {
                                    Console.WriteLine($"{e.Message} Try again.");
                                    scoreLine = Console.ReadLine().Split(' ');
                                }

                                scoreDictionary[scoreKey] = score;

                                totalScore = scoreDictionary.Values.Where((s) => s >= 0).Sum();

                                scoreDictionary = CalculateBonus(scoreDictionary);

                                notValidInput = false;
                            }
                        }

                    } else if (scoreLine.Length == 2)
                    {
                        if (scoreLine[0] != "d" || !functionDictionary.ContainsKey(scoreLine[1]))
                        {
                            Console.WriteLine("Write 'd' followed by the score you wish to delete. Try again.");
                            scoreLine = Console.ReadLine().Split(' ');
                        }
                        else
                        {
                            String scoreKey = scoreLine[1];
                            scoreDictionary[scoreKey] = 0;
                            notValidInput = false;
                        }
                    }
                    else
                    {
                        Console.WriteLine("The entered command does not exist. Try again.");
                        scoreLine = Console.ReadLine().Split(' ');
                    }
                }

                Console.Clear();
                
                foreach (IDie die in dice)
                    die.Unlock();

                rolls = 0;
            }

            DrawScores(scoreDictionary, totalScore);

            Console.WriteLine("\nGame finished. Press any key to exit...");

            // Draw the score dictionary and prompt user with game finished
            Console.ReadKey();
        }

        private static void DrawScores(Dictionary<String, int> dictionary, int totalScore)
        {
            foreach (KeyValuePair<String, int> pair in dictionary)
            {
                String value = pair.Value.ToString();

                if (pair.Value == -1)
                    value = "";
                if (pair.Value == 0)
                    value = "-";

                Console.WriteLine($"{pair.Key}: {value}");
            }

            Console.WriteLine($"Total: {totalScore}");
        }

        private static void DrawDice(List<IDie> dice)
        {
            for (int i = 1; i <= dice.Count; i++)
            {
                Console.Write($" {i} \t");
            }

            Console.WriteLine();

            foreach (IDie die in dice)
            {
                Console.Write(" _ \t");
            }

            Console.WriteLine();

            foreach (IDie die in dice)
            {
                Console.Write($"|{die.Value}|\t");
            }

            Console.WriteLine();

            foreach (IDie die in dice)
            {
                Console.Write(" ‾ \t");
            }

            Console.WriteLine();
        }

        private static bool CheckUpperSectionFinished(Dictionary<String, int> dictionary)
        {
            return !dictionary.Where((pair) => UPPER_SECTION_SCORES.Contains(pair.Key)).Select((pair) => pair.Value)
                .Contains(-1);
        }

        private static Dictionary<String, int> CalculateBonus(Dictionary<String, int> dictionary)
        {
            if (dictionary["bonus"] == -1 && CheckUpperSectionFinished(dictionary))
            {
                int bonus = 0;

                if (dictionary.Where((pair) => UPPER_SECTION_SCORES.Contains(pair.Key))
                        .Select((pair) => pair.Value).Sum() >= 63)
                    bonus = 50;

                dictionary["bonus"] = bonus;
            }

            return dictionary;
        }

        // PossibleAllDice functions
        
        private static bool OnesPossibleAllDice(List<IDie> dice)
        {
            return dice.Any((die) => die.Value == 1);
        }

        private static bool TwosPossibleAllDice(List<IDie> dice)
        {
            return dice.Any((die) => die.Value == 2);
        }

        private static bool ThreesPossibleAllDice(List<IDie> dice)
        {
            return dice.Any((die) => die.Value == 3);
        }

        private static bool FoursPossibleAllDice(List<IDie> dice)
        {
            return dice.Any((die) => die.Value == 4);
        }

        private static bool FivesPossibleAllDice(List<IDie> dice)
        {
            return dice.Any((die) => die.Value == 5);
        }

        private static bool SixesPossibleAllDice(List<IDie> dice)
        {
            return dice.Any((die) => die.Value == 6);
        }

        private static bool Pair1PossibleAllDice(List<IDie> dice)
        {
            List<List<IDie>> sublists = GenerateDiceSublists(dice, 2);
            return sublists.Any((sublist) => AllDiceEqual(sublist));
        }

        // Generates sublists of {dice} of length {sublistLength}.
        private static List<List<IDie>> GenerateDiceSublists(List<IDie> dice, int sublistLength)
        {
            List<List<IDie>> sublists = new List<List<IDie>>();

            for (int i = 0; i < dice.Count - sublistLength + 1; i++)
            {
                sublists.Add(dice.GetRange(i, (sublistLength - 1) + i));
            }

            return sublists;
        }

        private static bool AllDiceEqual(List<IDie> dice)
        {
            return dice.All((die) => die.Value == dice.First().Value);
        }

        // PossibleLockedDice functions

        private static String OnesPossibleLockedDice(List<IDie> dice)
        {
            String error = "";

            if (dice.Any(die => die.Value != 1))
                error = "All dice must be ones.";

            return error;
        }

        private static String TwosPossibleLockedDice(List<IDie> dice)
        {
            String error = "";

            if (dice.Any(die => die.Value != 2))
                error = "All dice must be twos.";

            return error;
        }

        private static String ThreesPossibleLockedDice(List<IDie> dice)
        {
            String error = "";

            if (dice.Any(die => die.Value != 3))
                error = "All dice must be threes.";

            return error;
        }

        private static String FoursPossibleLockedDice(List<IDie> dice)
        {
            String error = "";

            if (dice.Any(die => die.Value != 4))
                error = "All dice must be fours.";

            return error;
        }

        private static String FivesPossibleLockedDice(List<IDie> dice)
        {
            String error = "";

            if (dice.Any(die => die.Value != 5))
                error = "All dice must be fives.";

            return error;
        }

        private static String SixesPossibleLockedDice(List<IDie> dice)
        {
            String error = "";

            if (dice.Any(die => die.Value != 6))
                error = "All dice must be sixes.";

            return error;
        }

        private static String Pair1PossibleLockedDice(List<IDie> dice)
        {
            String error = "";

            if (dice.Count != 2)
                error = "1 pair requires 2 dice.";
            if (dice[0].Value != dice[1].Value)
                error = "Dice must be equal.";

            return error;
        }

        private static String Pair2PossibleLockedDice(List<IDie> dice)
        {
            String error = "";

            if (dice.Count != 4)
                error = "2 pair requires 4 dice.";
            if (dice[0].Value != dice[1].Value ||
                dice[2].Value != dice[3].Value ||
                dice[0].Value == dice[3].Value)
                error = "Both pairs must be equal and not the same.";

            return error;
        }

        private static String ThreeAlikePossibleLockedDice(List<IDie> dice)
        {
            String error = "";

            if (dice.Count != 3)
                error = "Three alike requires 3 dice.";
            if (dice[0].Value != dice[1].Value ||
                dice[1].Value != dice[2].Value)
                error = "Dice must be equal.";

            return error;
        }

        private static String FourAlikePossibleLockedDice(List<IDie> dice)
        {
            String error = "";

            if (dice.Count != 4)
                error = "Four alike requires 4 dice.";
            if (dice[0].Value != dice[1].Value ||
                dice[1].Value != dice[2].Value ||
                dice[2].Value != dice[3].Value)
                error = "Dice must be equal.";

            return error;
        }

        private static String SmallStraightPossibleLockedDice(List<IDie> dice)
        {
            String error = "";

            if (dice.Count != 5)
                error = "Small straight requires 5 dice.";
            if (dice.Select((die) => die.Value) != SMALL_STRAIGHT)
                error = "Dice do not match a small straight.";

            return error;
        }

        private static String LargeStraightPossibleLockedDice(List<IDie> dice)
        {
            String error = "";

            if (dice.Count != 5)
                error = "Large straight requires 5 dice.";
            if (dice.Select((die) => die.Value) != LARGE_STRAIGHT)
                error = "Dice do not match a large straight.";

            return error;
        }
        private static String FullHousePossibleLockedDice(List<IDie> dice)
        {
            String error = "";

            if (dice.Count != 5)
                error = "Full house requires 5 dice.";
            if ((dice[0].Value != dice[1].Value || dice[2].Value != dice[3].Value || dice[3].Value != dice[4].Value) ||
                (dice[0].Value != dice[1].Value || dice[1].Value != dice[2].Value || dice[3].Value != dice[4].Value) ||
                (dice[0].Value) == dice[4].Value)
                error = "Dice do not match a full house.";

            return error;
        }

        // Score functions

        private static int SumScore(List<IDie> dice)
        {
            return dice.Select((die) => die.Value).Sum();
        }

        private static int FullHouse(List<IDie> dice)
        {
            if (dice.Count != 5)
                throw new Exception("Full house requires 5 dice.");

            // Checks whether 2 first are equal and 3 last are equal, or that 3 first are equal and 2 last are equal, and that first and last are not equal
            if ((dice[0].Value != dice[1].Value || dice[2].Value != dice[3].Value || dice[3].Value != dice[4].Value) ||
                (dice[0].Value != dice[1].Value || dice[1].Value != dice[2].Value || dice[3].Value != dice[4].Value) ||
                (dice[0].Value) == dice[4].Value)
                throw new Exception("Dice do not match a full house.");

            return dice.Select((die) => die.Value).Sum();
        }

        private static int Chance(List<IDie> dice)
        {
            return dice.Select((die) => die.Value).Sum();
        }

        private static int Yatzy(List<IDie> dice)
        {
            if (dice.Count != 5)
                throw new Exception("Yatzy requires 5 dice.");

            if (dice[0].Value != dice[1].Value || dice[1].Value != dice[2].Value || dice[2].Value != dice[3].Value || dice[3].Value != dice[4].Value)
                throw new Exception("All dice must be equal.");

            return 50;
        }
    }
}
