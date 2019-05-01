using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yatzeeh
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Du har slået ");

            var dice = new BiasedDice(true);
            var negdice = new NegBiasedDice(true);

            var diceRoll = dice.Roll();
            var diceRoll2 = dice.Roll();
            var diceRoll3 = dice.Roll();
            var diceRoll4 = dice.Roll();
            var diceRoll5 = dice.Roll();
            var positiveDice = dice.BiasedRoll();
            var negativeDice = negdice.NegBiasedRoll();

            var sum = diceRoll + diceRoll2 + diceRoll3 + diceRoll4 + diceRoll5 + positiveDice + negativeDice;
            Console.WriteLine($"Terning 1: {diceRoll} \nTerning 2: {diceRoll2} \nTerning 3: {diceRoll3} \nTerning 4: {diceRoll4} \nTerning 5: {diceRoll5} \npositivedice: {positiveDice} \nnegativedice {negativeDice}");
            Console.WriteLine($"Den samlede sum af terningerne er {sum}");
            Console.Read();
        }
    }
}
