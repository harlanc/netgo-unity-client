namespace Netgo.Library
{
    using System;
    public class NGBuffer
    {
        private byte[] mBuffer;

        public byte[] Bytes
        {
            get { return mBuffer; }
        }

        public NGBuffer(byte[] buffer,int size)
        {
            mBuffer = new byte[size];
            Buffer.BlockCopy(buffer, 0, mBuffer, 0, size);
        }
    }
}