using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp3
{
    class BiasedDie : Die
    {
        public bool BiasHigh { get; set; }

        public BiasedDie(bool biasHigh)
        {
            BiasHigh = biasHigh;
        }

        public override void Roll(Random random)
        {
            if (BiasHigh)
                Value = random.Next(4, 7);
            else
                Value = random.Next(1, 4);
        }
    }
}
