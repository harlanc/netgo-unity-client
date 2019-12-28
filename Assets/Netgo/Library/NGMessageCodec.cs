using System;
using System.IO;
using System.Collections.Generic;
namespace Netgo.Library
{
    //https://blog.csdn.net/qq_29639547/article/details/76212128
    public class NGMessageCodec
    {
        public static byte[] Encode(byte[] data)
        {
            byte[] result = new byte[data.Length + 4];

            MemoryStream ms = new MemoryStream();
            //little end
            //https://stackoverflow.com/questions/1540251/binarywriter-endian-issue
            BinaryWriter br = new BinaryWriter(ms);
            br.Write(data.Length);
            br.Write(data);

            Buffer.BlockCopy(ms.ToArray(), 0, result, 0, (int)ms.Length);
            br.Close();
            ms.Close();
            return result;
        }

        public static byte[] Decode(ref List<byte> cache)
        {
            if (cache.Count < 4)
            {
                return null;
            }

            MemoryStream ms = new MemoryStream(cache.ToArray());
            BinaryReader br = new BinaryReader(ms);
            UInt32 len = br.ReadUInt32();
            if (len > ms.Length - ms.Position)
            {
                return null;
            }

            byte[] result = br.ReadBytes((int)len);
            cache.Clear();
            cache.AddRange(br.ReadBytes((int)ms.Length - (int)ms.Position));

            return result;
        }
    }
}