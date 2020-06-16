using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Huffman
{
    //character frequency class
    public class CharacterFrequency: IComparable
    {
        //variables
        private char ch;
        private int frequency;

        //properties
        public char Ch
        {
            get
            {
                return ch;
            }

            set
            {
                ch = value;
            }
        }
        public int Frequency
        {
            get
            {
                return frequency;
            }

            set
            {
                if (value >= 0)
                    frequency = value;
            }
        }

        //constructors
        public CharacterFrequency()
        {
        }

        public CharacterFrequency(char c, int f)
        {
            ch = c;
            frequency = f;
        }

        //methods
        public void Increment()
        {
            Frequency++;
        }
        
        //overriden methods
        public int CompareTo(object obj)
        {
            CharacterFrequency CF = (CharacterFrequency)obj;
            if (this.Frequency < CF.Frequency)
            {
                return -1;
            }
            else if (this.Frequency == CF.Frequency)
            {
                return 0;
            }
            else
            {
                return 1;
            }
            
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            if (!(obj is CharacterFrequency || obj is Char))
                return false;
            if (this == obj)
                return true;
            if (obj is CharacterFrequency)
            {
                CharacterFrequency cf = (CharacterFrequency)obj;
                if (this.Frequency == cf.Frequency && this.Ch == cf.Ch)
                    return true;
                else
                    return false;
            }
            else
            {
                Char cf = (Char)obj;
                if (cf == Ch)
                    return true;
                else
                    return false;
            }
        }
        public override string ToString()
        {
            return string.Format("{0}", Ch.ToString());
        }
        


        
    }
}
