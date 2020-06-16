using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Huffman
{
    public class EncodingData
    {
        private byte value;
        private string encoding;

        public byte Value
        {
            get
            {
                return value;
            }

            private set
            {
                this.value = value;
            }
        }
        public string Encoding
        {
            get
            {
                return encoding;
            }

            private set
            {
                encoding = value;
            }
        }

        public EncodingData(byte value, string encoding)
        {
            this.Value = value;
            this.Encoding = encoding;
        }

        public override string ToString()
        {
            return string.Format("{0}»{1}", value, encoding);
        }
    }
}
