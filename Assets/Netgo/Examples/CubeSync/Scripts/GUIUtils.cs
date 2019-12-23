using UnityEngine;
public class GUIUtils
{
    static public Rect ProcessRect(Rect rect, float distance)
    {
        Rect r = rect;
        r.xMin -= distance;
        r.xMax += distance;
        r.yMin -= distance;
        r.yMax += distance;
        return r;
    }
}