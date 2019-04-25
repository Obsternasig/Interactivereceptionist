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
            for (int i = 0; i < 6; i++) //Vi kaster med 6 terninger
            {
                if (i < 3) //Så længe vi kaster under 3 terninger (0 - 2) vil vi kaste med normale terninger. Over 3 kaster vi med biased dice
                    activeDices.Add(new Dice());
                else
                    activeDices.Add(new BiasedDice(false)); //BiasedDice er falsk, så vi bruger kun den normale terning lige nu.
            }
            Console.WriteLine("Der er lige nu " + activeDices.Count + " aktive terninger");
        }


        public void playRound()
        {
            //For loop lige her på alle spillere
            for (int i = 0; i < diceThrows; i++) //I = 0, og så længe den er mindre end det antal dice throws some spiller laver, vil den loop

            {
                for (int j = 0; j < activeDices.Count; j++) //Her vil vi lave et for loop, som er lig vores antive terninger.
                {
                    activeDices[j].Roll();
                    Console.WriteLine("Terning: " + (j + 1) + " slår " + activeDices[j].getValue()); //Her udskriver vi terning nummer (starter fra 1 da vi lægger 1 til.
                                                                                                     //og viser hvad de har slået via activeDices[j].getValue
                }

                // check er alle 1'ere
                // check alle muligheder
                // 
                
                Console.ReadLine();
                
            }
        }

        /* // returns sum for dice showing faceValue
         public int valueSpecificFace(int faceValue)
         {
             int count = 0;
             foreach (int n in dice)
                 if (n == faceValue) count++;

             return count * faceValue;
         }
         // returns value for dice showing count dice with same faceValue
         // valueSameOdAKind(3) returns the value for 3 of-a-kind
         // valueSameOdAKind(4) returns the value for 4 of-a-kind
         public int valueSameOfAKind(int count)
         {
             int value = 0;
             for (int i = 1; i <= 6; i++)
                 if (stat[i] >= count) value = count * i;

             return value;
         }


         // returns the value of yatzy
         public int valueYatzy()
         {
             if (valueSameOfAKind(5) > 0)
                 return 50;
             else
                 return 0;
         }

         //returns the value of Chance
         public int valueChance()
         {
             int sum = 0;
             foreach (int n in dice)
                 sum + n;

             return sum;
         }
         // returns value for one pair
         public int valueOnePair()
         {
             if (valueSpecificFace(6) >= 12)
                 return 12;
             else if (valueSpecificFace(5) >= 10)
                 return 10;
             else if (valueSpecificFace(4) >= 8)
                 return 8;
             else if (valueSpecificFace(3) >= 6)
                 return 6;
             else if (valueSpecificFace(2) >= 4)
                 return 4;
             else if (valueSpecificFace(1) >= 2)
                 return 2;
             else
                 return 0;
         }
         // returns value of TwoPair
         public int valueTwoPair()
         {
             int firstPair = valueOnePair();
             if (firstPair > 10 && valueSpecificFace(5) >= 10)
                 return 10 + firstPair;
             else if (firstPair > 8 && valueSpecificFace(4) >= 8)
                 return 8 + firstPair;
             else if (firstPair > 6 && valueSpecificFace(3) >= 6)
                 return 6 + firstPair;
             else if (firstPair > 4 && valueSpecificFace(2) >= 4)
                 return 4 + firstPair;
             else if (firstPair > 2 && valueSpecificFace(1) >= 2)
                 return 2 + firstPair;
             else
                 return 0;
         }
         // returns the value of SmallStraight
         public int valueSmallStraight()
         {
             int count, countDies = 0;
             for (int i = 1; i <= 5; i++)
             {
                 count = 0;
                 foreach (int n in dies)
                     if (n == i) count++;
                 if (count > 0) countDies++;
             }
             if (countDies == 5)
                 return 15;
             return 0;
         }
         // returns the value of LargeStraight
         public int valueLargeStraight()
         {
             int count, countDies = 0;
             for (int i = 2; i <= 6; i++)
             {
                 count = 0;
                 foreach (int n in dies)
                     if (n == i) count++;
                 if (count > 0) countDies++;
             }
             if (countDies == 5)
                 return 20;
             return 0;
         }
         // returns the value of FullHouse
         public int valueFullHouse()
         {
             int firstThree = valueOf3OfAKind();
             if (firstThree != 18 && firstThree != 0 && valueSpecificFace(6) >= 12)
                 return 12 + firstThree;
             else if (firstThree != 15 && firstThree != 0 && valueSpecificFace(5) >= 10)
                 return 10 + firstThree;
             else if (firstThree != 12 && firstThree != 0 && valueSpecificFace(4) >= 8)
                 return 8 + firstThree;
             else if (firstThree != 9 && firstThree != 0 && valueSpecificFace(3) >= 6)
                 return 6 + firstThree;
             else if (firstThree != 6 && firstThree != 0 && valueSpecificFace(2) >= 4)
                 return 4 + firstThree;
             else if (firstThree != 3 && firstThree != 0 && valueSpecificFace(1) >= 2)
                 return 2 + firstThree;
             else
                 return 0;
         }
         // help function to full house
         int valueOf3OfAKind()
         {
             if (valueSpecificFace(6) >= 18)
                 return 18;
             else if (valueSpecificFace(5) >= 15)
                 return 15;
             else if (valueSpecificFace(4) >= 12)
                 return 12;
             else if (valueSpecificFace(3) >= 9)
                 return 9;
             else if (valueSpecificFace(2) >= 6)
                 return 6;
             else if (valueSpecificFace(1) >= 3)
                 return 3;
             else
                 return 0;
         } */
    }
}
