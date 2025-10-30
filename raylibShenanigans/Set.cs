using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;

namespace raylibShenanigans
{
    internal class Set 
    {
        private const int SAME_NUMBER_MAX = 2;
        private List<Pair<Vector2,int>> container;

        public Set(){
            container = new List<Pair<Vector2, int>>(10) { new Pair<Vector2,int>(new Vector2(700,500),1) };        
        }

        public void add(Vector2 elem)
        {
            for (int i = 0; i < container.Capacity;i++)
            {
                if (container[i].First == elem && container[i].Second == SAME_NUMBER_MAX)
                {
                    return;
                }
                else if (container[i].First == elem && container[i].Second != SAME_NUMBER_MAX)
                {
                    container[i].Second++;
                }
                else
                {
                    container.Add(new Pair<Vector2, int>(new Vector2(elem.X, elem.Y), 1));
                    return;
                }
            }
        }

        
        public void remove(Vector2 elem) { 
            int left = 0;
            int right = container.Count - 1;
            Vector2 target = elem;

            container.Sort();

            while (left <= right) { 
                int middle = (left + right) / 2;
                if (container[middle].First == elem) { 
                    container.RemoveAt(middle);
                    return;
                }
                else if (container[middle].First.X < elem.X && container[middle].First.Y < elem.Y) {
                    left = middle + 1; 
                }else if (container[middle].First.X > elem.X && container[middle].First.Y > elem.Y){
                    right = middle - 1;
                }
            }

        }
        public int count() { return container.Count(); }
        public void removeAt(int index) { container.RemoveAt(index); }

        public Vector2 this[int index]
        {
            get => container[index].First;   // getter
        }

    }
}
