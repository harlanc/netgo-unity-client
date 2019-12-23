using System;
using System.Collections.Generic;
using UnityEngine;
namespace Netgo.Client
{
    public class NGResourceManager
    {
        public Dictionary<string, GameObject> ResourceBuffer = new Dictionary<string, GameObject>();
        public NGResourceManager()
        {
        }

        public GameObject InstantiatePrefab(string prefabname,Vector3 position,Quaternion rotation)
        {
            GameObject prefabresouce = null;
            if(ResourceBuffer.ContainsKey(prefabname))
            {
                prefabresouce = ResourceBuffer[prefabname];
            }
            else
            {
                prefabresouce = (GameObject)Resources.Load(prefabname, typeof(GameObject));
                ResourceBuffer.Add(prefabname, prefabresouce);
            }

            GameObject rv = GameObject.Instantiate(prefabresouce,position, rotation);
            return rv;
        }

        public void Destory(GameObject go)
        {
            GameObject.Destroy(go);
        }
    }
}
