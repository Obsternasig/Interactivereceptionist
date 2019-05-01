using System;

namespace Yatzy_Mini_projekt
{
    public class Dice
    {
        private int value;
        public int Current { get; private set; }
        
        private Random _diceRoll; //private instance variables har _ foran, for at indikere at de er private

        public Dice()
        {
            _diceRoll = new Random();
        }

        // This is a method.
        //public is the access modifier, return type is the int. RollTheDice is the name, that takes no parameters
        public void Roll()
        {
            value = _diceRoll.Next(1, 7);
        }

        public int getValue() { return value; }
        public override string ToString()
        {
            return "This dice is a " + Current;
        }
                
    }
    
}