using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Huffman
{
    public class BinaryTreeNode<E> :IComparable
    {
        private E data;
        private int value;
        private BinaryTreeNode<E> left;
        private BinaryTreeNode<E> right;

        public BinaryTreeNode(E data, int value)
        {
            this.data = data;
            this.Value = value;
            left = null;
            right = null;
        }

        public E Data
        {
            get
            {
                return data;
            }

            set
            {
                data = value;
            }
        }

        public BinaryTreeNode<E> Left
        {
            get
            {
                return left;
            }
            set 
            {
                left = value;
            }
        }

        public BinaryTreeNode<E> Right
        {
            get
            {
                return right;
            }
            set 
            {
                right = value;
            }
        }

        public int Value
        {
            get
            {
                return value;
            }

            set
            {
                this.value = value;
            }
        }

        public Boolean isLeaf()
        {
            return (left == null && right == null);
        }

        public int CompareTo(object obj)
        {
            BinaryTreeNode<E> bt = (BinaryTreeNode<E>)obj;
            if (this.Value < bt.Value)
            {
                return -1;
            }
            else if (this.Value == bt.Value)
            {
                return 0;
            }
            else
            {
                return 1;
            }

        }
    }
}
