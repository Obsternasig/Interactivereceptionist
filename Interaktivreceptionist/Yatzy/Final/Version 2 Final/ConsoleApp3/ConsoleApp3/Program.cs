using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace ConsoleApp3
{
    class Program
    {
        private static readonly int[] POSSIBLE_DICE_VALUES = { 1, 2, 3, 4, 5, 6 };

        private static readonly string[] UPPER_SECTION_SCORES = { "ones", "twos", "threes", "fours", "fives", "sixes" };
        private static readonly string[] LOWER_SECTION_SCORES = { "pair1", "pair2", "threealike", "fouralike", "smallstraight", "largestraight", "fullhouse", "chance", "yatzy" };

        private static readonly int[] SMALL_STRAIGHT = { 1, 2, 3, 4, 5 };
        private static readonly int[] LARGE_STRAIGHT = { 2, 3, 4, 5, 6 };

        static void Main(string[] args)
        {
            // Set console output encoding to UTF8 to support UTF8 characters.
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            // Config variables
            var rolls = 3;
            var biasedDice = 0;

            // Game state variables
            var currentRoll = 0;
            var random = new Random();
            Roll roll;
            var totalScore = 0;
            var possibleAllDiceDictionary = new Dictionary<string, Func<Dictionary<string, int>, List<IDie>, bool, (bool _isPoosible, string _errorText)>>
            {
                { "ones", OnesPossibleDice },
                { "twos", TwosPossibleDice },
                { "threes", ThreesPossibleDice },
                { "fours", FoursPossibleDice },
                { "fives", FivesPossibleDice },
                { "sixes", SixesPossibleDice },
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
            var scoreFunctionsDictionary = new Dictionary<string, Func<List<IDie>, int>>
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
            var scoreDictionary = new Dictionary<string, int>
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

            Console.WriteLine("How many rolls between 2 and 10 per turn would you like?");
            //var rollsLine = Console.ReadLine();
            var rollsLineParsed = int.Parse(Console.ReadLine());

            while (rollsLineParsed < 2 || rollsLineParsed > 10)
            {
                Console.WriteLine("How many rolls between 2 and 10 per turn would you like?");
                //rollsLine = Console.ReadLine();
                rollsLineParsed = int.Parse(Console.ReadLine());
            }

            rolls = rollsLineParsed;

            Console.WriteLine("How many biased dice between 1 and 6 would you like?");
            var biasedDiceLine = Console.ReadLine();
            var biasedDiceLineParsed = int.Parse(biasedDiceLine);

            while (biasedDiceLineParsed < 1 || biasedDiceLineParsed > 6)
            {
                Console.WriteLine("How many biased dice between 1 and 6 would you like?");
                biasedDiceLine = Console.ReadLine();
                biasedDiceLineParsed = int.Parse(biasedDiceLine);
            }

            biasedDice = biasedDiceLineParsed;

            var normalDice = 6 - biasedDice;

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

                    if (currentRoll == rolls)
                    {
                        DrawPossibleScores(roll.Dice, possibleAllDiceDictionary, scoreDictionary);
                    }

                    Console.WriteLine("Lock die 1 to 6 using die numbers and space as separator.");
                    var lockedDiceLine = Console.ReadLine();
                    Console.WriteLine();
                    var lockedDice = lockedDiceLine.Split(' ');

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
                        foreach (var lockedDie in lockedDice)
                        {
                            var lockedDieIndex = int.Parse(lockedDie) - 1;
                            roll.Dice[lockedDieIndex].Lock();
                        }
                    }
                }

                Console.WriteLine("Choose your score.");
                var scoreLine = Console.ReadLine().Split(' ');

                var notValidInput = true;
                var possibleScores = CalculatePossibleScores(roll.Dice, possibleAllDiceDictionary, scoreDictionary);

                while (notValidInput)
                {
                    // User wants to get a score
                    if (scoreLine.Length == 1)
                    {
                        var scoreKey = scoreLine[0];

                        if (!possibleScores.Contains(scoreKey))
                        {
                            Console.WriteLine("The entered score is not possible. Try again.");
                            scoreLine = Console.ReadLine().Split(' ');
                        }
                        else
                        {

                            var lockedDice = roll.Dice.Where((die) => die.Locked).ToList();

                            var possibleLockedDice = possibleAllDiceDictionary[scoreKey](scoreDictionary, lockedDice, true);

                            if (!possibleLockedDice._isPoosible)
                            {
                                Console.WriteLine(possibleLockedDice._errorText);
                                scoreLine = Console.ReadLine().Split(' ');
                            }
                            else
                            {
                                notValidInput = false;
                                var score = scoreFunctionsDictionary[scoreKey](lockedDice);
                                scoreDictionary[scoreKey] = score;
                                totalScore = scoreDictionary.Values.Where((s) => s >= 0).Sum();
                                scoreDictionary = CalculateBonus(scoreDictionary);
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
                            var scoreKey = scoreLine[1];
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
                var biasLine = Console.ReadLine();

                if (biasLine != "")
                {
                    var biasHigh = true;

                    if (biasLine == "high")
                    {
                        biasHigh = true;
                    }
                    else if (biasLine == "low")
                    {
                        biasHigh = false;
                    }

                    foreach (var die in roll.Dice)
                    {
                        if (die is BiasedDie biasedDie)
                        {
                            biasedDie.BiasHigh = biasHigh;
                        }
                    }
                }

                Console.Clear();

                foreach (var die in roll.Dice)
                {
                    die.Unlock();
                }

                currentRoll = 0;
            }

            DrawScores(scoreDictionary, totalScore);

            //

            Console.WriteLine("\nGame finished. Press any key to exit...");

            // Draw the score dictionary and prompt user with game finished
            Console.ReadKey();
        }

        private static void DrawScores(Dictionary<string, int> dictionary, int totalScore)
        {
            foreach (var pair in dictionary)
            {
                var value = pair.Value.ToString();

                if (pair.Value == -1)
                {
                    value = "";
                }

                if (pair.Value == 0)
                {
                    value = "-";
                }

                Console.WriteLine($"{pair.Key}: {value}");
            }

            Console.WriteLine($"Total: {totalScore}");
        }

        private static void DrawDice(List<IDie> dice)
        {
            for (var i = 1; i <= dice.Count; i++)
            {
                Console.Write($" {i} \t");
            }

            Console.WriteLine();

            foreach (var die in dice)
            {
                Console.Write(" _ \t");
            }

            Console.WriteLine();

            foreach (var die in dice)
            {
                Console.Write($"|{die.Value}|\t");
            }

            Console.WriteLine();

            foreach (var die in dice)
            {
                Console.Write(" ‾ \t");
            }

            Console.WriteLine();
        }

        private static void DrawPossibleScores(List<IDie> dice, Dictionary<string, Func<Dictionary<string, int>, List<IDie>, bool, (bool _isPoosible, string _errorText)>> possibleAllDiceDictionary, Dictionary<string, int> scoreDictionary)
        {
            var possibleScores = CalculatePossibleScores(dice, possibleAllDiceDictionary, scoreDictionary);

            Console.WriteLine("Possible scores:");

            foreach (var score in possibleScores)
            {
                Console.WriteLine(score);
            }

            Console.WriteLine();
        }

        private static List<string> CalculatePossibleScores(List<IDie> dice, Dictionary<string, Func<Dictionary<string, int>, List<IDie>, bool, (bool _isPoosible, string _errorText)>> possibleAllDiceDictionary, Dictionary<string, int> scoreDictionary)
        {
            var possibleDictionary = new Dictionary<string, bool>();

            foreach (var pair in possibleAllDiceDictionary)
            {
                var possible = pair.Value(scoreDictionary, dice, false);
                possibleDictionary.Add(pair.Key, possible._isPoosible);
            }

            var possibleScores = possibleDictionary.Where((pair) => pair.Value).Select((pair) => pair.Key).ToList();

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
                var bonus = 0;

                if (dictionary.Where((pair) => UPPER_SECTION_SCORES.Contains(pair.Key))
                        .Select((pair) => pair.Value).Sum() >= 63)
                {
                    bonus = 50;
                }

                dictionary["bonus"] = bonus;
            }

            return dictionary;
        }

        // NEW

        private static (bool, string) PossibleFromDiceUpperSection(Dictionary<string, int> scoreDictionary, List<IDie> dice, int value, bool possibleLockedDice)
        {
            // Check if value is already present on upper_section element
            if (scoreDictionary[UPPER_SECTION_SCORES[value - 1]] != -1)
            {
                return (false, "Value already present.");
            }
            if (possibleLockedDice)
            {
                return dice.Any(die => die.Value != value) ? (false, "All dice must be " + UPPER_SECTION_SCORES[value - 1]) : (true, "");
            }
            else
            {
                return (dice.Any((die) => die.Value == value), "");
            }
        }

        private static (bool, string) OnesPossibleDice(Dictionary<string, int> scoreDictionary, List<IDie> dice, bool possibleLockedDice) =>
            PossibleFromDiceUpperSection(scoreDictionary, dice, 1, possibleLockedDice);

        private static (bool, string) TwosPossibleDice(Dictionary<string, int> scoreDictionary, List<IDie> dice, bool possibleLockedDice) =>
            PossibleFromDiceUpperSection(scoreDictionary, dice, 2, possibleLockedDice);

        private static (bool, string) ThreesPossibleDice(Dictionary<string, int> scoreDictionary, List<IDie> dice, bool possibleLockedDice) =>
            PossibleFromDiceUpperSection(scoreDictionary, dice, 3, possibleLockedDice);

        private static (bool, string) FoursPossibleDice(Dictionary<string, int> scoreDictionary, List<IDie> dice, bool possibleLockedDice) =>
            PossibleFromDiceUpperSection(scoreDictionary, dice, 4, possibleLockedDice);

        private static (bool, string) FivesPossibleDice(Dictionary<string, int> scoreDictionary, List<IDie> dice, bool possibleLockedDice) =>
            PossibleFromDiceUpperSection(scoreDictionary, dice, 5, possibleLockedDice);

        private static (bool, string) SixesPossibleDice(Dictionary<string, int> scoreDictionary, List<IDie> dice, bool possibleLockedDice) =>
            PossibleFromDiceUpperSection(scoreDictionary, dice, 6, possibleLockedDice);


        private static (bool, string) Pair1PossibleAllDice(Dictionary<string, int> scoreDictionary, List<IDie> dice, bool possibleLockedDice)
        {
            if (!CheckUpperSectionFinished(scoreDictionary) || scoreDictionary["pair1"] != -1)
            {
                return (false, "Value already present.");
            }

            if (possibleLockedDice)
            {
                if (dice.Count != 2)
                {
                    return (false, "1 pair requires 2 dice.");
                }
                if (!AllDiceEqual(dice))
                {
                    return (false, "Dice must be equal.");
                }
                return (true, "");
            }
            else
            {
                var sublists = GenerateDiceSublists(dice, 2);
                return (sublists.Any((sublist) => AllDiceEqual(sublist)), "");
            }
        }
   
        private static (bool, string) Pair2PossibleAllDice(Dictionary<string, int> scoreDictionary, List<IDie> dice, bool possibleLockedDice)
        {
            if (!CheckUpperSectionFinished(scoreDictionary) || scoreDictionary["pair2"] != -1)
            {
                return (false, "Value already present.");
            }

            if (possibleLockedDice)
            {
                var error = "";

                if (dice.Count != 4)
                {
                    return (false, "2 pair requires 4 dice.");
                }

                if (!AllDiceEqual(dice.GetRange(0, 2)) ||
                    !AllDiceEqual(dice.GetRange(2, 2)) ||
                    dice[0] == dice[3])
                {
                    error = "Both pairs must be equal and not the same.";
                }

                return (true, "");
            }
            else
            {
                var sublistsDistinct = GenerateDiceSublists(dice, 2)
                .Where((sublist) => AllDiceEqual(sublist))
                .Distinct()
                .ToList();
                return (sublistsDistinct.Count >= 2, "");
            }
        }

        private static (bool, string) ThreeAlikePossibleAllDice(Dictionary<string, int> scoreDictionary, List<IDie> dice, bool possibleLockedDice)
        {
            if (!CheckUpperSectionFinished(scoreDictionary) || scoreDictionary["threealike"] != -1)
            {
                return (false, "Value already present.");
            }

            if (possibleLockedDice)
            {
                if (dice.Count != 3)
                {
                    return (false, "Three alike requires 3 dice.");
                }

                if (!AllDiceEqual(dice))
                {
                    return (false, "Dice must be equal.");
                }
                return (true, "");
            }
            else
            {
                var sublists = GenerateDiceSublists(dice, 3);
                return (sublists.Any((sublist) => AllDiceEqual(sublist)), "");
            }
        }

        private static (bool, string) FourAlikePossibleAllDice(Dictionary<string, int> scoreDictionary, List<IDie> dice, bool possibleLockedDice)
        {
            if (!CheckUpperSectionFinished(scoreDictionary) || scoreDictionary["fouralike"] != -1)
            {
                return (false, "Value already present.");
            }

            if (possibleLockedDice)
            {
                if (dice.Count != 4)
                {
                    return (false, "Four alike requires 4 dice.");
                }

                if (!AllDiceEqual(dice))
                {
                    return (false, "Dice must be equal.");
                }
                return (true, "");
            }
            else
            {
                var sublists = GenerateDiceSublists(dice, 4);
                return (sublists.Any((sublist) => AllDiceEqual(sublist)), "");
            }
        }

        private static (bool, string) SmallStraightPossibleAllDice(Dictionary<string, int> scoreDictionary, List<IDie> dice, bool possibleLockedDice)
        {
            if (!CheckUpperSectionFinished(scoreDictionary) || scoreDictionary["smallstraight"] != -1)
            {
                return (false, "Value already present.");
            }

            if (possibleLockedDice)
            {
                if (dice.Count != 5)
                {
                    return (false, "Small straight requires 5 dice.");
                }

                if (dice.Select((die) => die.Value) != SMALL_STRAIGHT)
                {
                    return (false, "Dice do not match a small straight.");
                }
                return (true, "");
            }
            else
            {
                var sublists = GenerateDiceSublists(dice, 5);
                return (sublists.Any((sublist) => sublist.Select((die) => die.Value) == SMALL_STRAIGHT), "");
            }
        }

        private static (bool, string) LargeStraightPossibleAllDice(Dictionary<string, int> scoreDictionary, List<IDie> dice, bool possibleLockedDice)
        {
            if (!CheckUpperSectionFinished(scoreDictionary) || scoreDictionary["largestraight"] != -1)
            {
                return (false, "Value already present.");
            }

            if (possibleLockedDice)
            {
                if (dice.Count != 5)
                {
                    return (false, "Large straight requires 5 dice.");
                }

                if (dice.Select((die) => die.Value) != LARGE_STRAIGHT)
                {
                    return (false, "Dice do not match a large straight.");
                }
                return (true, "");
            }
            else
            {
                var sublists = GenerateDiceSublists(dice, 5);
                return (sublists.Any((sublist) => sublist.Select((die) => die.Value) == LARGE_STRAIGHT), "");
            }
        }

        // TO BE FIXED
        private static (bool, string) FullHousePossibleAllDice(Dictionary<string, int> scoreDictionary, List<IDie> dice, bool possibleLockedDice)
        {
            if (!CheckUpperSectionFinished(scoreDictionary) || scoreDictionary["fullhouse"] != -1)
            {
                return (false, "Value already present.");
            }
            if (possibleLockedDice)
            {
                if (dice.Count != 5)
                {
                    return (false, "Full house straight requires 5 dice.");
                }
                if (
                    (
                        (AllDiceEqual(dice.GetRange(0, 2)) && AllDiceEqual(dice.GetRange(2, 3)))
                        ||
                        (AllDiceEqual(dice.GetRange(0, 3)) && AllDiceEqual(dice.GetRange(3, 2)))
                    )
                    &&
                    dice[0].Value != dice[4].Value
                    )
                {
                    return (true, "");
                }
                else
                {
                    return (false, "Dice do not match a full house.");
                }
            }
            else
            {
                var sublists2 = GenerateDiceSublists(dice, 2);
                var pairList2 = sublists2.Where(e => AllDiceEqual(e));
                if (!pairList2.Any())
                {
                    return (false, "No 2 dice are equal");
                }

                var sublists3 = GenerateDiceSublists(dice, 3);
                var pairList3 = sublists3.Where(e => AllDiceEqual(e));
                if (!pairList3.Any())
                {
                    return (false, "No 3 dice are equal");
                }

                if (pairList3.Any(e => pairList2.Any(c => e[0].Value != c[0].Value)))
                {
                    return (true, "");
                }
                else
                {
                    return (false, "No full house");
                }
            }
        }

        private static (bool, string) ChancePossibleAllDice(Dictionary<string, int> scoreDictionary, List<IDie> dice, bool possibleLockedDice)
        {
            return (CheckUpperSectionFinished(scoreDictionary) && scoreDictionary["chance"] == -1, "");
        }

        private static (bool, string) YatzyPossibleAllDice(Dictionary<string, int> scoreDictionary, List<IDie> dice, bool possibleLockedDice)
        {
            if (possibleLockedDice)
            {
                if (dice.Count != 6)
                {
                    return (false, "Yatzy requires 6 dice.");
                }

                if (!AllDiceEqual(dice))
                {
                    return (false, "Dice do not match a yatzy.");
                }
                return (true, "");
            }
            else
            {
                return (CheckUpperSectionFinished(scoreDictionary) && scoreDictionary["yatzy"] == -1 && AllDiceEqual(dice), "");
            }
        }


        private static List<List<IDie>> GenerateDiceSublists(List<IDie> dice, int sublistLength)
        {
            var sublists = new List<List<IDie>>();

            for (var i = 0; i < dice.Count - sublistLength + 1; i++)
            {
                sublists.Add(dice.GetRange(i, sublistLength));
            }

            return sublists;
        }

        private static bool AllDiceEqual(List<IDie> dice)
        {
            return dice.All((die) => die.Value == dice.First().Value);
        }


        // Score functions

        private static int SumScore(List<IDie> dice) => dice.Select((die) => die.Value).Sum();

        private static int YatzyScore(List<IDie> dice) => 50;
    }
}
