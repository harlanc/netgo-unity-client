using UnityEngine;

namespace Netgo.Client
{
    public class NGViewUtils
    {
        public static NGView[] GetViews(GameObject go)
        {
            return go.GetComponentsInChildren<NGView>();
        }
    }
}