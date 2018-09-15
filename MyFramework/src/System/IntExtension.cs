using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System
{
    public static class Int32Extension
    {
        public static byte[] ToLittleEndian(this int n)
        {
            byte[] b = new byte[4];
            b[0] = (byte)(n & 0xff);
            b[1] = (byte)(n >> 8 & 0xff);
            b[2] = (byte)(n >> 16 & 0xff);
            b[3] = (byte)(n >> 24 & 0xff);
            return b;
        }
    }
}
