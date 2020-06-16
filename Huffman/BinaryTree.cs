using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Huffman
{
    public class BinaryTree 
    {
        private BinaryTreeNode<CharacterFrequency> root;
        private BinaryTreeNode<CharacterFrequency> current;
        private int size;
        private LinkedList<EncodingData> encodingTable;
        private String encoding;
        
        public BinaryTree()
        {
            Root = null;
            Current = null;
            size = 0;
        }

        public BinaryTree(CharacterFrequency data)
        {
            Root = new BinaryTreeNode<CharacterFrequency>(data, 0);
            Current = Root;
            size = 0;
        }

        public BinaryTree(BinaryTreeNode<CharacterFrequency> root)
        {
            Root = root;
            Current = Root;
            size = 0;
        }
        
        public Boolean isEmpty()
        {
            return root == null;
        }

        public int Size
        {
            get
            {
                return size;
            }
        }

        public BinaryTreeNode<CharacterFrequency> Current
        {
            get
            {
                return current;
            }
            set
            {
                current = value;
            }
        }
        public BinaryTreeNode<CharacterFrequency> Root
        {
            get
            {
                return root;
            }
            set
            {
                root = value;
            }
        }

        public LinkedList<EncodingData> EncodingTable
        {
            get
            {
                return encodingTable;
            }

            private set
            {
                encodingTable = value;
            }
        }

        public void inOrder(BinaryTreeNode<CharacterFrequency> p, string encoding)
        {
            if (p != null)
            {
                if (p.Left != null)
                {
                    inOrder(p.Left, encoding + "0");
                }

                if (p.Right != null)
                {
                    inOrder(p.Right, encoding + "1");
                }

                if (p.isLeaf())
                {
                    
                    if (encodingTable == null)
                    {
                        encodingTable = new LinkedList<EncodingData>();
                    }
                    
                    encodingTable.AddLast(new EncodingData((byte)p.Data.Ch, encoding));
                }
            }
        }
    }
}
