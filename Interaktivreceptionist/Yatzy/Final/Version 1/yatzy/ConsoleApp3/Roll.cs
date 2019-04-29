using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp3
{
    class Roll
    {
        public List<IDie> Dice { get; set; } = new List<IDie>();

        public Roll(int normalDice, int biasedDice)
        {
            for (int i = 0; i < normalDice; i++)
            {
                Dice.Add(new Die());
            }
            
            for (int i = 0; i < biasedDice; i++)
            {
                Dice.Add(new BiasedDie(true));
            }
        }

        public void RollDice(Random random)
        {
            foreach (IDie die in Dice)
            {
                if (!die.Locked)
                    die.Roll(random);
                die.Unlock();
            }
        }
    }
}
