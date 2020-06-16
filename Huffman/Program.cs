using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Huffman
{
    class Program
    {
        static void Main(string[] args)
        {
            //class variables
            #region Variables
            StreamReader txtIn;
            StreamWriter txtOut = new StreamWriter(String.Format("encoding{0}", args[1]));
            byte b = 0;
            int pow = 7;
            char ch;
            string encoding;
            string line;
            char delimiter = '»';
            #endregion

            //check for proper usage
            #region Proper Usage
            if (args.Length != 2)
            {
                Console.WriteLine("Proper usage is: program.exe inFile outFile");
                Console.ReadKey();
                return;
            }
            //
            try
            {
                txtIn = new StreamReader(args[0]);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadLine();
                return;
            }
            #endregion

            //read the file and count the character frequencies
            #region List of Character Frequencies
            LinkedList<CharacterFrequency> list = new LinkedList<CharacterFrequency>();
            char c = (char)txtIn.Read();

            while (!txtIn.EndOfStream)
            {
                findFrequency(c, list);
                c = (char)txtIn.Read();
            }
            findFrequency(c, list);

            txtIn.Close();
            #endregion

            //create a sorted linked list of binary tree nodes
            #region Sorted List of binary Tree Nodes of Character Frequencies
            SortedLinkedList<BinaryTreeNode<CharacterFrequency>> sll = new SortedLinkedList<BinaryTreeNode<CharacterFrequency>>();
            foreach (CharacterFrequency charf in list)
            {
                sll.Add(new BinaryTreeNode<CharacterFrequency>(charf, charf.Frequency));
            }
            #endregion

            //while the list is not empty, build the binary tree nodes, adding the two smallest values
            #region Building the Tree
            while (sll.Count > 1)
            {
                BinaryTreeNode<CharacterFrequency> left;
                BinaryTreeNode<CharacterFrequency> right;
                BinaryTreeNode<CharacterFrequency> phNode;
                left = sll.First.Value;
                sll.RemoveFirst();
                right = sll.First.Value;
                sll.RemoveFirst();
                phNode = new BinaryTreeNode<CharacterFrequency>(null, (left.Value + right.Value));
                phNode.Left = left;
                phNode.Right = right;
                sll.Add(phNode);

            }
            BinaryTree tree = new BinaryTree(sll.First.Value);
            tree.inOrder(tree.Root, "");
            
            //write the encoding table to a new file
            foreach (EncodingData d in tree.EncodingTable)
            {
                    txtOut.WriteLine(String.Format("{0}", d.ToString()));
            }
            #endregion

            //compress the file
            #region Compression
            txtIn = new StreamReader(args[0]);
            
            StreamWriter compressedFile = new StreamWriter(args[1]);
            while (!txtIn.EndOfStream)
            {
                ch = (char)txtIn.Read();
                encoding = findEncoding(ch, tree.EncodingTable);

                foreach (char character in encoding)
                {
                    if (character == '1')
                    {
                        b = (byte)(b | (byte)Math.Pow(2, pow));
                    }
                    pow--;
                    if (pow < 0)
                    {
                        pow = 7;
                        compressedFile.Write((char)b);
                        b = 0;
                    }
                }
            }
            if (pow != 7)
            {
                compressedFile.Write((char)b);
            }
            txtOut.Close();
            txtIn.Close();
            compressedFile.Close();
            #endregion

            //decompress the file
            #region Decompression
            txtIn = new StreamReader(String.Format("encoding{0}", args[1]));
            //decompression
            
            BinaryTree charTree = new BinaryTree(new BinaryTreeNode<CharacterFrequency>(new CharacterFrequency((char)0, 0), 0));
            BinaryTreeNode<CharacterFrequency> node = charTree.Root;
            while (!txtIn.EndOfStream)
            {
                line = txtIn.ReadLine();
                string[] holder;
                holder = line.Split(delimiter);
                    
                if (holder.Length > 1)
                {
                        foreach (char letter2 in holder[1])
                        {
                            if (letter2 == '0')
                            {
                                if (node.Left == null)
                                {
                                    node.Left = new BinaryTreeNode<CharacterFrequency>(new CharacterFrequency((char)0, 0), 0);
                                }
                                node = node.Left;
                            }
                            else
                            {
                                if (node.Right == null)
                                {
                                    node.Right = new BinaryTreeNode<CharacterFrequency>(new CharacterFrequency((char)0, 0), 0);
                                }
                                node = node.Right;
                            }
                        }
                    
                    byte holdingByte = 0;
                        Byte.TryParse(holder[0], out holdingByte);
                    node.Data.Ch = (char)holdingByte;
                        node = charTree.Root;
                    
                }
            }

            txtIn.Close();

            txtIn = new StreamReader(args[1]);
            StreamWriter decompressedFile = new StreamWriter(String.Format("decompressed{0}", args[0]));
            byte decomp;
            byte decompVar;
            pow = 7;
            node = charTree.Root;
            while (!txtIn.EndOfStream)
            {
                decomp = (byte)txtIn.Read();
                while (pow >= 0)
                {
                    decompVar = (byte)(decomp & (byte)(Math.Pow(2, pow)));

                    if (decompVar > 0)
                    {
                        //if the variable is greater than 0, go to the right
                        node = node.Right;
                    }

                    else
                    {
                        node = node.Left;
                    }

                    if (node.isLeaf())
                    {
                        decompressedFile.Write(node.Data);
                        node = charTree.Root;
                    }
                    pow--;
                }
                pow = 7;

            }

            decompressedFile.Close();
            txtIn.Close();
            txtOut.Close();
            #endregion

            Console.ReadKey();
        }
        
        private static void findFrequency(char c, LinkedList<CharacterFrequency> list)
        {
            //find the frequency
            #region Find Frequency
            LinkedListNode<CharacterFrequency> node = list.First;
            bool found = false;

            while (found == false && node != null)
            {
                if (node.Value.Equals(c))
                {
                    found = true;
                    node.Value.Increment();
                }
                else
                {
                    node = node.Next;
                }
            }

            if (found == false)
            {
                list.AddLast(new CharacterFrequency(c, 1));
            }
            #endregion
        }
        
        private static string findEncoding(char ch, LinkedList<EncodingData> nlist)
        {
            //find the encoding
            #region Find Encoding
            LinkedListNode<EncodingData> node = nlist.First;
            bool found = false;
            string chEncoding = null;
            while (found == false && node != null)
            {

                if (node.Value.Value.Equals((byte)ch))
                {
                    found = true;
                    chEncoding = node.Value.Encoding;
                }
                else
                {
                    node = node.Next;
                }
            }
            return chEncoding;
            #endregion
        }
    }
    
}
