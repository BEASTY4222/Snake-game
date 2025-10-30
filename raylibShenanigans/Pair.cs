using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace raylibShenanigans
{
    internal class Pair<T1,T2>
    {
        private T1 first;
        private T2 second;

        public Pair(T1 first, T2 second)
        {
            this.first = first;
            this.second = second;
        }

        public T1 First
        {
            get { return first; }
            set { first = value; }
        }
        public T2 Second
        {
            get { return second; }
            set { second = value; }
        }
    }
}
