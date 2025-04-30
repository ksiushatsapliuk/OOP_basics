using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkedListLib
{
    internal class Node
    {
        public double Value { get; set; }
        public Node Next { get; set; }
        public Node(double value)
        {
            Value = value;
            Next = null;
        }
    }
}

