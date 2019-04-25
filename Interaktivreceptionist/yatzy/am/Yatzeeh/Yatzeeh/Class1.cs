using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yatzeeh
{
    public class BiasedDice : Dice
    {
        public BiasedDice(bool isFair)
        {

        }

        public int BiasedRoll()
        {

            var diceroll = Roll();
            if (diceroll == 5)
            {
                return diceroll + 1;

            }
            if (diceroll <= 4)
            {
                return diceroll + 2;

            }
            return diceroll;
        }
    }
}
