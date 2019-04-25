using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yatzy_spil;

namespace Yatzy_Mini_projekt
{
    class Program
    {
        static void Main(string[] args)
        {
            Spilleklasse yatzy = new Spilleklasse(3);

            yatzy.playRound();

            Console.Write("Vælg dine terninger. OBS: VÆLGER DU EKSEMPELVIS TERNING 1, SÅ TAST TERNINGENS NUMMER OG VÆLGER DU FLERE SÅ ADSKIL VED KOMMA" );

            char kast1 = (char)Console.Read();
            if (Char.IsLetter(kast1))

            }


        }


}