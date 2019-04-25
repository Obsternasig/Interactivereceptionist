using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yatzeeh
{
    public class Dice
    {
        public int Current { get; private set; }

        private Random _diceRoll; 

        public Dice()

        {
            _diceRoll = new Random();
        }


        public int Roll()
        {
            Current = _diceRoll.Next(1, 7);
            return Current;
            
        }
        public override string ToString()
        {
            return "This dice is a " + Current;
        }
    }

}   
