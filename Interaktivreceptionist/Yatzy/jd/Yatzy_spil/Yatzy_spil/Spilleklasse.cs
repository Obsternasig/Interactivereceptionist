using System;
using System.Collections.Generic;
using Yatzy_Mini_projekt;
namespace Yatzy_spil
{
    public class Spilleklasse
    {

        List<Dice> activeDices;
        List<Dice> inactiveDices = new List<Dice>();
        int diceThrows;        
        public Spilleklasse(int diceThrows)
        {
            activeDices = new List<Dice>();
            this.diceThrows = diceThrows;
            for(int i = 0; i < 6; i++)
            {   if (i < 3)
                    activeDices.Add(new Dice());
                else
                    activeDices.Add(new BiasedDice(false));
            }
            Console.WriteLine("Der er lige nu " + activeDices.Count + " aktive dices");
        }

        public void playRound()
        {
            //For loop lige her på alle spillere
            for(int i = 0; i < diceThrows; i++)

            {
                for(int j = 0; j < activeDices.Count; j++)
                {
                    activeDices[j].Roll();
                    Console.WriteLine("Terning: " + (j + 1) + " slår " + activeDices[j].getValue());
                }

                // check er alle 1'ere
                // check alle muligheder
                //

                Console.ReadLine();

            }
        }

    }

}
