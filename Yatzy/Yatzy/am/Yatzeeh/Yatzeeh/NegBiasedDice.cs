using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yatzeeh
{
    public class NegBiasedDice : Dice
    {
        public NegBiasedDice(bool isFair)
        {

        }

        public int NegBiasedRoll()
        {

            var diceroll = Roll();
            if (diceroll == 2)
            {
                return diceroll - 1;

            }
            if (diceroll >= 4)
            {
                return diceroll - 2;

            }
            return diceroll;
        }
    }
}