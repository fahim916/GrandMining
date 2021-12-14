using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class positionPoint
{
    public int x, y;

    public positionPoint(int nx, int ny)
    {
        x = nx;
        y = ny;
    }

    public void mulTipication(int m)
    {
        x *= m;
        y *= m;
    }

    public void Addition(positionPoint p)
    {
        x += p.x;
        y += p.y;
    }

    public Vector2 ToVector()
    {
        return new Vector2(x, y);
    }

    public bool Equals(positionPoint P)
    {
        return (x == P.x && y == P.y);
    }

    public static positionPoint FromVector(Vector2 v)
    {
        return new positionPoint((int) v.x, (int) v.y);
    }

    public static positionPoint FromVector(Vector3 v)
    {
        return new positionPoint((int)v.x, (int)v.y);
    }

    public static positionPoint mulTipication(positionPoint p, int m)
    {
        return new positionPoint(p.x * m, p.y * m);
    }
    public static positionPoint Addition(positionPoint p, positionPoint o)
    {
        return new positionPoint(p.x + o.x, p.y + o.y);
    }
    public static positionPoint clone(positionPoint p)
    {
        return new positionPoint(p.x, p.y);
    }
    public static  positionPoint zero
    {
        get { return new positionPoint(0, 0); }
    }
    public static positionPoint one
    {
        get { return new positionPoint(1, 1); }
    }
    public static positionPoint up
    {
        get { return new positionPoint(0, 1); }
    }
    public static positionPoint down
    {
        get { return new positionPoint(0, -1); }
    }
    public static positionPoint right
    {
        get { return new positionPoint(1, 0); }
    }
    public static positionPoint left
    {
        get { return new positionPoint(-1, 0); }
    }
}
