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


        }


    }
}