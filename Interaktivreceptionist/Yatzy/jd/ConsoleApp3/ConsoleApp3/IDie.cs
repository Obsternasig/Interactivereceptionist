using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp3
{
    interface IDie
    {
        Int32 Value { get; set; }
        bool Locked { get; set; }
        void Roll(Random random);
        void Lock();
        void Unlock();
    }
}
