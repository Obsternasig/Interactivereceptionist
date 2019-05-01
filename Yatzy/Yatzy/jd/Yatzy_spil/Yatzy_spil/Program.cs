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

            
            
            //Lav i main en metode der husker på hvad der er rollet med hvilket terninger. Efter et bruger har sagt hvilket terninger 
            //de vil bruge, benyt en string split.
        }

    }
}