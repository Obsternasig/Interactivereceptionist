﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace ConsoleApp3
{
    class Program
    {
        private static readonly int[] POSSIBLE_DICE_VALUES = {1, 2, 3, 4, 5, 6};

        private static readonly string[] UPPER_SECTION_SCORES = {"ones", "twos", "threes", "fours", "fives", "sixes"};
        private static readonly string[] LOWER_SECTION_SCORES = {"pair1", "pair2", "threealike", "fouralike", "smallstraight", "largestraight", "fullhouse", "chance", "yatzy"};
        
        private static readonly int[] SMALL_STRAIGHT = {1, 2, 3, 4, 5};
        private static readonly int[] LARGE_STRAIGHT = {2, 3, 4, 5, 6};

        static void Main(string[] args)
        {
            // Set console output encoding to UTF8 to support UTF8 characters.
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            // Config variables
            int rolls = 3;
            int biasedDice = 0;
            
            // Game state variables
            int currentRoll = 0;
            Random random = new Random();
            Roll roll;
            int totalScore = 0;
            Dictionary<string, Func<Dictionary<string, int>, List<IDie>, bool>> possibleAllDiceDictionary = new Dictionary<string, Func<Dictionary<string, int>, List<IDie>, bool>>
            {
                { "ones", OnesPossibleAllDice },
                { "twos", TwosPossibleAllDice },
                { "threes", ThreesPossibleAllDice },
                { "fours", FoursPossibleAllDice },
                { "fives", FivesPossibleAllDice },
                { "sixes", SixesPossibleAllDice },
                { "pair1", Pair1PossibleAllDice },
                { "pair2", Pair2PossibleAllDice },
                { "threealike", ThreeAlikePossibleAllDice },
                { "fouralike", FourAlikePossibleAllDice },
                { "smallstraight", SmallStraightPossibleAllDice },
                { "largestraight", LargeStraightPossibleAllDice },
                { "fullhouse", FullHousePossibleAllDice },
                { "chance", ChancePossibleAllDice },
                { "yatzy", YatzyPossibleAllDice }
            };
            Dictionary<string, Func<List<IDie>, string>> possibleLockedDiceDictionary = new Dictionary<string, Func<List<IDie>, string>>
            {
                { "ones", OnesPossibleLockedDice },
                { "twos", TwosPossibleLockedDice },
                { "threes", ThreesPossibleLockedDice },
                { "fours", FoursPossibleLockedDice },
                { "fives", FivesPossibleLockedDice },
                { "sixes", SixesPossibleLockedDice },
                { "pair1", Pair1PossibleLockedDice },
                { "pair2", Pair2PossibleLockedDice },
                { "threealike", ThreeAlikePossibleLockedDice },
                { "fouralike", FourAlikePossibleLockedDice },
                { "smallstraight", SmallStraightPossibleLockedDice },
                { "largestraight", LargeStraightPossibleLockedDice },
                { "fullhouse", FullHousePossibleLockedDice },
                { "chance", ChancePossibleLockedDice },
                { "yatzy", YatzyPossibleLockedDice }
            };
            Dictionary<string, Func<List<IDie>, int>> scoreFunctionsDictionary = new Dictionary<string, Func<List<IDie>, int>>
            {
                { "ones", SumScore },
                { "twos", SumScore },
                { "threes", SumScore },
                { "fours", SumScore },
                { "fives", SumScore },
                { "sixes", SumScore },
                { "pair1", SumScore },
                { "pair2", SumScore },
                { "threealike", SumScore },
                { "fouralike", SumScore },
                { "smallstraight", SumScore },
                { "largestraight", SumScore },
                { "fullhouse", SumScore },
                { "chance", SumScore },
                { "yatzy", YatzyScore }
            };
            Dictionary<string, int> scoreDictionary = new Dictionary<string, int>
            {
                { "ones", 0 },
                { "twos", 0 },
                { "threes", 0 },
                { "fours", 0 },
                { "fives", 0 },
                { "sixes", 63 },
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

            Console.WriteLine("How many rolls between 2 and 10 per turn would you like?");
            string rollsLine = Console.ReadLine();

            int rollsLineParsed = int.Parse(rollsLine);

            while (rollsLineParsed < 2 || rollsLineParsed > 10)
            {
                Console.WriteLine("How many rolls between 2 and 10 per turn would you like?");
                rollsLine = Console.ReadLine();
                rollsLineParsed = int.Parse(rollsLine);
            }

            rolls = rollsLineParsed;

            Console.WriteLine("How many biased dice between 1 and 6 would you like?");
            string biasedDiceLine = Console.ReadLine();
            int biasedDiceLineParsed = int.Parse(biasedDiceLine);

            while (biasedDiceLineParsed < 1 || biasedDiceLineParsed > 6)
            {
                Console.WriteLine("How many biased dice between 1 and 6 would you like?");
                biasedDiceLine = Console.ReadLine();
                biasedDiceLineParsed = int.Parse(biasedDiceLine);
            }

            biasedDice = biasedDiceLineParsed;

            int normalDice = 6 - biasedDice;

            roll = new Roll(normalDice, biasedDice);

            Console.Clear();

            // This loop goes on until a score has been set for every score there is
            while (scoreDictionary.ContainsValue(-1))
            {
                Console.WriteLine("Score:");
                DrawScores(scoreDictionary, totalScore);
                Console.WriteLine();

                // This loop goes on until all rolls have been made
                while (currentRoll < rolls)
                {
                    roll.RollDice(random);

                    roll.Dice.Sort();
                    currentRoll++;

                    Console.WriteLine($"Roll {currentRoll}:");
                    DrawDice(roll.Dice);

                    if (currentRoll == 3)
                    {
                        DrawPossibleScores(roll.Dice, possibleAllDiceDictionary, scoreDictionary);
                    }

                    Console.WriteLine("Lock die 1 to 6 using die numbers and space as separator.");
                    string lockedDiceLine = Console.ReadLine();
                    Console.WriteLine();
                    string[] lockedDice = lockedDiceLine.Split(' ');

                    // Check if locked dice are either 1, 2, 3, 4, 5 or 6
                    while (lockedDice[0] != "" &&
                           lockedDice.Any((lockedDie) => !POSSIBLE_DICE_VALUES.Contains(int.Parse(lockedDie))))
                    {
                        Console.WriteLine("Lock die 1 to 6 using die numbers and space as separator.");
                        lockedDiceLine = Console.ReadLine();
                        lockedDice = lockedDiceLine.Split(' ');
                    }

                    if (lockedDice[0] != "")
                    {
                        foreach (string lockedDie in lockedDice)
                        {
                            int lockedDieIndex = int.Parse(lockedDie) - 1;
                            roll.Dice[lockedDieIndex].Lock();
                        }
                    }
                }

                Console.WriteLine("Choose your score.");
                string[] scoreLine = Console.ReadLine().Split(' ');

                bool notValidInput = true;

                while (notValidInput)
                {
                    // User wants to get a score
                    if (scoreLine.Length == 1)
                    {
                        List<string> possibleScores = CalculatePossibleScores(roll.Dice, possibleAllDiceDictionary, scoreDictionary);

                        string scoreKey = scoreLine[0];

                        if (!possibleScores.Contains(scoreKey))
                        {
                            Console.WriteLine("The entered score is not possible. Try again.");
                            scoreLine = Console.ReadLine().Split(' ');
                        } else
                        {
                            List<IDie> lockedDice = roll.Dice.Where((die) => die.Locked).ToList();

                            string possibleLockedDice = possibleLockedDiceDictionary[scoreKey](lockedDice);

                            if (possibleLockedDice != "")
                            {
                                Console.WriteLine(possibleLockedDice);
                                scoreLine = Console.ReadLine().Split(' ');
                            }
                            else
                            {
                                int score = scoreFunctionsDictionary[scoreKey](lockedDice);
                                scoreDictionary[scoreKey] = score;
                                totalScore = scoreDictionary.Values.Where((s) => s >= 0).Sum();
                                scoreDictionary = CalculateBonus(scoreDictionary);
                                notValidInput = false;
                            }
                        }

                    }
                    // User wants to delete a score
                    else if (scoreLine.Length == 2)
                    {
                        if (scoreLine[0] != "d" || !scoreFunctionsDictionary.ContainsKey(scoreLine[1]))
                        {
                            Console.WriteLine("Write 'd' followed by the score you wish to delete. Try again.");
                            scoreLine = Console.ReadLine().Split(' ');
                        }
                        else
                        {
                            string scoreKey = scoreLine[1];
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

                Console.WriteLine("If you wish to change bias write 'high' or 'low'. Press enter to continue.");
                string biasLine = Console.ReadLine();

                if (biasLine != "")
                {
                    bool biasHigh = true;

                    if (biasLine == "high")
                        biasHigh = true;
                    else if (biasLine == "low")
                        biasHigh = false;

                    foreach (IDie die in roll.Dice)
                    {
                        if (die is BiasedDie biasedDie)
                            biasedDie.BiasHigh = biasHigh;
                    }
                }

                Console.Clear();
                
                foreach (IDie die in roll.Dice)
                    die.Unlock();

                currentRoll = 0;
            }

            DrawScores(scoreDictionary, totalScore);

            Console.WriteLine("\nGame finished. Press any key to exit...");

            // Draw the score dictionary and prompt user with game finished
            Console.ReadKey();
        }

        private static void DrawScores(Dictionary<string, int> dictionary, int totalScore)
        {
            foreach (KeyValuePair<string, int> pair in dictionary)
            {
                string value = pair.Value.ToString();

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

        private static void DrawPossibleScores(List<IDie> dice, Dictionary<string, Func<Dictionary<string, int>, List<IDie>, bool>> possibleAllDiceDictionary, Dictionary<string, int> scoreDictionary)
        {
            List<string> possibleScores = CalculatePossibleScores(dice, possibleAllDiceDictionary, scoreDictionary);

            Console.WriteLine("Possible scores:");

            foreach (string score in possibleScores)
            {
                Console.WriteLine(score);
            }

            Console.WriteLine();
        }

        private static List<string> CalculatePossibleScores(List<IDie> dice, Dictionary<string, Func<Dictionary<string, int>, List<IDie>, bool>> possibleAllDiceDictionary, Dictionary<string, int> scoreDictionary)
        {
            Dictionary<string, bool> possibleDictionary = new Dictionary<string, bool>();

            foreach (KeyValuePair<string, Func<Dictionary<string, int>, List<IDie>, bool>> pair in possibleAllDiceDictionary)
            {
                bool possible = pair.Value(scoreDictionary, dice);
                possibleDictionary.Add(pair.Key, possible);
            }

            List<string> possibleScores = possibleDictionary.Where((pair) => pair.Value).Select((pair) => pair.Key).ToList();

            return possibleScores;
        }

        private static bool CheckUpperSectionFinished(Dictionary<string, int> dictionary)
        {
            return !dictionary.Where((pair) => UPPER_SECTION_SCORES.Contains(pair.Key)).Select((pair) => pair.Value)
                .Contains(-1);
        }

        private static Dictionary<string, int> CalculateBonus(Dictionary<string, int> dictionary)
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
        
        private static bool OnesPossibleAllDice(Dictionary<string, int> dictionary, List<IDie> dice)
        {

            return dice.Any((die) => die.Value == 1);
        }

        private static bool TwosPossibleAllDice(Dictionary<string, int> dictionary, List<IDie> dice)
        {
            return dice.Any((die) => die.Value == 2);
        }

        private static bool ThreesPossibleAllDice(Dictionary<string, int> dictionary, List<IDie> dice)
        {
            return dice.Any((die) => die.Value == 3);
        }

        private static bool FoursPossibleAllDice(Dictionary<string, int> dictionary, List<IDie> dice)
        {
            return dice.Any((die) => die.Value == 4);
        }

        private static bool FivesPossibleAllDice(Dictionary<string, int> dictionary, List<IDie> dice)
        {
            return dice.Any((die) => die.Value == 5);
        }

        private static bool SixesPossibleAllDice(Dictionary<string, int> dictionary, List<IDie> dice)
        {
            return dice.Any((die) => die.Value == 6);
        }

        private static bool Pair1PossibleAllDice(Dictionary<string, int> dictionary, List<IDie> dice)
        {
            if (!CheckUpperSectionFinished(dictionary) || dictionary["pair1"] != -1)
                return false;

            List<List<IDie>> sublists = GenerateDiceSublists(dice, 2);
            return sublists.Any((sublist) => AllDiceEqual(sublist));
        }

        // Does not work
        private static bool Pair2PossibleAllDice(Dictionary<string, int> dictionary, List<IDie> dice)
        {
            if (!CheckUpperSectionFinished(dictionary) || dictionary["pair2"] != -1)
                return false;

            List<List<IDie>> sublistsDistinct = GenerateDiceSublists(dice, 2)
                .Where((sublist) => AllDiceEqual(sublist))
                .Distinct()
                .ToList();
            return sublistsDistinct.Count >= 2;
        }

        private static bool ThreeAlikePossibleAllDice(Dictionary<string, int> dictionary, List<IDie> dice)
        {
            if (!CheckUpperSectionFinished(dictionary) || dictionary["threealike"] != -1)
                return false;

            List<List<IDie>> sublists = GenerateDiceSublists(dice, 3);
            return sublists.Any((sublist) => AllDiceEqual(sublist));
        }

        private static bool FourAlikePossibleAllDice(Dictionary<string, int> dictionary, List<IDie> dice)
        {
            if (!CheckUpperSectionFinished(dictionary) || dictionary["fouralike"] != -1)
                return false;

            List<List<IDie>> sublists = GenerateDiceSublists(dice, 4);
            return sublists.Any((sublist) => AllDiceEqual(sublist));
        }

        private static bool SmallStraightPossibleAllDice(Dictionary<string, int> dictionary, List<IDie> dice)
        {
            if (!CheckUpperSectionFinished(dictionary) || dictionary["smallstraight"] != -1)
                return false;

            List<List<IDie>> sublists = GenerateDiceSublists(dice, 5);
            return sublists.Any((sublist) => sublist.Select((die) => die.Value) == SMALL_STRAIGHT);
        }

        private static bool LargeStraightPossibleAllDice(Dictionary<string, int> dictionary, List<IDie> dice)
        {
            if (!CheckUpperSectionFinished(dictionary) || dictionary["largestraight"] != -1)
                return false;

            List<List<IDie>> sublists = GenerateDiceSublists(dice, 5);
            return sublists.Any((sublist) => sublist.Select((die) => die.Value) == LARGE_STRAIGHT);
        }

        // TO BE FIXED
        private static bool FullHousePossibleAllDice(Dictionary<string, int> dictionary, List<IDie> dice)
        {
            List<List<IDie>> sublists2 = GenerateDiceSublists(dice, 2);
            List<List<IDie>> sublists3 = GenerateDiceSublists(dice, 3);
            return true;
        }

        private static bool ChancePossibleAllDice(Dictionary<string, int> dictionary, List<IDie> dice)
        {
            return CheckUpperSectionFinished(dictionary) && dictionary["chance"] == -1;
        }

        private static bool YatzyPossibleAllDice(Dictionary<string, int> dictionary, List<IDie> dice)
        {
            return CheckUpperSectionFinished(dictionary) && dictionary["yatzy"] == -1 && AllDiceEqual(dice);
        }

        // Generates sublists of {dice} of length {sublistLength}.
        private static List<List<IDie>> GenerateDiceSublists(List<IDie> dice, int sublistLength)
        {
            List<List<IDie>> sublists = new List<List<IDie>>();

            for (int i = 0; i < dice.Count - sublistLength + 1; i++)
            {
                sublists.Add(dice.GetRange(i, sublistLength));
            }

            return sublists;
        }

        private static bool AllDiceEqual(List<IDie> dice)
        {
            return dice.All((die) => die.Value == dice.First().Value);
        }

        // PossibleLockedDice functions

        private static string OnesPossibleLockedDice(List<IDie> dice)
        {
            string error = "";

            if (dice.Any(die => die.Value != 1))
                error = "All dice must be ones.";

            return error;
        }

        private static string TwosPossibleLockedDice(List<IDie> dice)
        {
            string error = "";

            if (dice.Any(die => die.Value != 2))
                error = "All dice must be twos.";

            return error;
        }

        private static string ThreesPossibleLockedDice(List<IDie> dice)
        {
            string error = "";

            if (dice.Any(die => die.Value != 3))
                error = "All dice must be threes.";

            return error;
        }

        private static string FoursPossibleLockedDice(List<IDie> dice)
        {
            string error = "";

            if (dice.Any(die => die.Value != 4))
                error = "All dice must be fours.";

            return error;
        }

        private static string FivesPossibleLockedDice(List<IDie> dice)
        {
            string error = "";

            if (dice.Any(die => die.Value != 5))
                error = "All dice must be fives.";

            return error;
        }

        private static string SixesPossibleLockedDice(List<IDie> dice)
        {
            string error = "";

            if (dice.Any(die => die.Value != 6))
                error = "All dice must be sixes.";

            return error;
        }

        private static string Pair1PossibleLockedDice(List<IDie> dice)
        {
            string error = "";

            if (dice.Count != 2)
                error = "1 pair requires 2 dice.";
            if (!AllDiceEqual(dice))
                error = "Dice must be equal.";

            return error;
        }

        private static string Pair2PossibleLockedDice(List<IDie> dice)
        {
            string error = "";

            if (dice.Count != 4)
                error = "2 pair requires 4 dice.";
            if (!AllDiceEqual(dice.GetRange(0, 2)) ||
                !AllDiceEqual(dice.GetRange(2, 2)) ||
                dice[0] == dice[3])
                error = "Both pairs must be equal and not the same.";

            return error;
        }

        private static string ThreeAlikePossibleLockedDice(List<IDie> dice)
        {
            string error = "";

            if (dice.Count != 3)
                error = "Three alike requires 3 dice.";
            if (!AllDiceEqual(dice))
                error = "Dice must be equal.";

            return error;
        }

        private static string FourAlikePossibleLockedDice(List<IDie> dice)
        {
            string error = "";

            if (dice.Count != 4)
                error = "Four alike requires 4 dice.";
            if (!AllDiceEqual(dice))
                error = "Dice must be equal.";

            return error;
        }

        private static string SmallStraightPossibleLockedDice(List<IDie> dice)
        {
            string error = "";

            if (dice.Count != 5)
                error = "Small straight requires 5 dice.";
            if (dice.Select((die) => die.Value) != SMALL_STRAIGHT)
                error = "Dice do not match a small straight.";

            return error;
        }

        private static string LargeStraightPossibleLockedDice(List<IDie> dice)
        {
            string error = "";

            if (dice.Count != 5)
                error = "Large straight requires 5 dice.";
            if (dice.Select((die) => die.Value) != LARGE_STRAIGHT)
                error = "Dice do not match a large straight.";

            return error;
        }

        //broken
        private static string FullHousePossibleLockedDice(List<IDie> dice)
        {
            string error = "";

            if (dice.Count != 5)
                error = "Full house requires 5 dice.";
            if ((!AllDiceEqual(dice.GetRange(0, 2)) || !AllDiceEqual(dice.GetRange(2, 3))) ||
                (!AllDiceEqual(dice.GetRange(0, 3)) || !AllDiceEqual(dice.GetRange(3, 2))) ||
                dice[0].Value == dice[4].Value)
                error = "Dice do not match a full house.";

            return error;
        }

        private static string ChancePossibleLockedDice(List<IDie> dice)
        {
            string error = "";

            return error;
        }

        private static string YatzyPossibleLockedDice(List<IDie> dice)
        {
            string error = "";

            if (dice.Count != 6)
                error = "Yatzy requires 6 dice.";
            if (!AllDiceEqual(dice))
                error = "Dice do not match a yatzy.";

            return error;
        }

        // Score functions

        private static int SumScore(List<IDie> dice)
        {
            return dice.Select((die) => die.Value).Sum();
        }

        private static int YatzyScore(List<IDie> dice)
        {
            return 50;
        }
    }
}
