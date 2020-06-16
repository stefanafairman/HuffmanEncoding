using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Huffman
{
    public class SortedLinkedList<E> : LinkedList<E> where E : IComparable
    {
        public void Add(E element)
        {
            if (element == null)
            {
                return; //ABORT MISSION!!!!
            }
            //if the list is empty, add the element at the beginning of the list
            if (this.Count < 1)
            {
                this.AddFirst(element);
            }
            //otherwise, if element is less than value of first node, add the element at the beginning of the list
            else if (this.First.Value.CompareTo(element) >= 0)
            {
                this.AddFirst(element);
            }
            //otherwise, if element is greater than value of last node, add the element at the end of the list
            else if (this.Last.Value.CompareTo(element) <= 0)
            {
                this.AddLast(element);
            }
            else
            {
                // if the element has not been inserted in the list, loop through each node and compare it to the value of each node
                // if the element is less than the value of the node, add it before the node, otherwise move to the next node in the list
                bool inserted = false;
                LinkedListNode<E> node = this.First;
                while (!inserted)
                {
                    if (node.Value.CompareTo(element) >= 0)
                    {
                        this.AddBefore(node, element);
                        inserted = true;
                    }
                    else
                    {
                        node = node.Next;
                    }
                }
            }
        }
    }
}
