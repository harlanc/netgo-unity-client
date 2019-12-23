using System;
using UnityEngine;
namespace Netgo.Client
{
    public class NGPlayer
    {
        public uint mPeerId;
        public bool IsLocal;
        public GameObject GO;
   
        public NGPlayer(bool islocal = false,uint peerid = 0)
        {
            GO = new GameObject();
            mPeerId = peerid;
            IsLocal = islocal;
        }

   
    }
}
