using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp3
{
    class Die : IDie
    {
        public int Value { get; set; }
        public bool Locked { get; set; }
        
        public void Roll(Random random)
        {
            Value = random.Next(1, 7);
        }

        public void Lock()
        {
            Locked = true;
        }

        public void Unlock()
        {
            Locked = false;
        }

        public int CompareTo(IDie other)
        {
            if (Value < other.Value)
                return -1;
            else if (Value == other.Value)
                return 0;
            else
                return 1;
        }

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}
