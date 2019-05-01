using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp3
{
    class Program
    {
        static void Main(string[] args)
        {
            // Internal state variables
            Int32 _rolls = 0;
            Random _random = new Random();
            List<IDie> _dice = new List<IDie>();            
            Dictionary<String, Func<List<IDie>, Int32>> _functionDictionary = new Dictionary<string, Func<List<IDie>, int>>()
            {
                { "ones", Ones },
                { "twos", Twos },
                { "fullhouse", FullHouse }
            };
            Dictionary<String, Int32> _scoreDictionary = new Dictionary<string, int>()
            {
                { "ones", -1 },
                { "twos", -1 },
                { "fullhouse", -1 }
            };

            // Instantiate 6 dice
            for (int i = 0; i < 6; i++)
            {
                _dice.Add(new Die());
            }
            

            // This loop goes on until a score has been set for every score there is
            while (_scoreDictionary.ContainsValue(-1))
            {
                Console.WriteLine("Score:");

                foreach (KeyValuePair<String, Int32> pair in _scoreDictionary)
                {
                    Console.WriteLine($"{pair.Key}: {pair.Value}");
                }

                Console.WriteLine();

                // This loop goes on until 3 rolls have been made
                while (_rolls < 3)
                {
                    foreach (IDie die in _dice)
                    {
                        if (!die.Locked)
                            die.Roll(_random);
                        die.Unlock();
                    }

                    _rolls++;
                    _dice.Sort((x, y) => x.Value.CompareTo(y.Value));
                    foreach (IDie die in _dice)                        
                    {               
                                                
                        Console.Write($"{die.Value} ");
                                                
                    }
                  
                    Console.Write('\n');

                    String lockedDiceLine = Console.ReadLine();

                    String[] lockedDice = lockedDiceLine.Split(' ');

                                        
                    // Check if locked dice are either 1, 2, 3, 4, 5 or 6
                    if (lockedDice[0] != "")
                    {
                        foreach (String lockedDie in lockedDice)
                        {
                            Int32 lockedDieIndex = Int32.Parse(lockedDie) - 1;
                            _dice[lockedDieIndex].Lock();
                        }
                    }                    
                }

                String[] scoreLine = Console.ReadLine().Split(' ');
                               
                bool notValidInput = true;

                while (notValidInput)
                {
                    if (scoreLine.Length == 1)
                    {
                        if (!_functionDictionary.ContainsKey(scoreLine[0]))
                        {
                            Console.WriteLine("The entered score does not exist. Try again.");
                            scoreLine = Console.ReadLine().Split(' ');
                        }
                        else
                        {
                            String scoreKey = scoreLine[0];
                            Int32 score = _functionDictionary[scoreKey](_dice.Where((die) => die.Locked).ToList());
                            _scoreDictionary[scoreKey] = score;
                            notValidInput = false;
                        }

                    } else if (scoreLine.Length == 2)
                    {
                        if (scoreLine[0] != "d" || !_functionDictionary.ContainsKey(scoreLine[1]))
                        {
                            Console.WriteLine("Write 'd' followed by the score you wish to delete. Try again.");
                            scoreLine = Console.ReadLine().Split(' ');
                        }
                        else
                        {
                            String scoreKey = scoreLine[1];
                            _scoreDictionary[scoreKey] = 0;
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

                _rolls = 0;
            }

            // Draw the score dictionary and prompt user with game finished
            Console.ReadKey();
        }

        private static Int32 Ones(List<IDie> dice)
        {
            return dice.Where((die) => die.Value == 1).Select((die) => die.Value).Sum();
        }

        private static Int32 Twos(List<IDie> dice)
        {
            return dice.Where((die) => die.Value == 2).Select((die) => die.Value).Sum();
        }

        private static Int32 FullHouse(List<IDie> dice)
        {
            if (dice.Count != 5)
                throw new Exception("Full house requires 5 dice.");

            // Check that 3 dice are equal and 2 other are equal and not the same as first 3
            // Sort dice by value, check first 2 or 3 are equal and do the opposite for the rest, then sum
            return -1;
        }
    }
}
