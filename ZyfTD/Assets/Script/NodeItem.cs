using UnityEngine;

public class NodeItem{

    public bool walkable;
    public Vector3 pos;
    public int x, y;
    public int g, h;

    public int f
    {
        get { return g + h; }
    }

    public NodeItem parent;

    public NodeItem(bool walkable, Vector3 pos, int x, int y)
    {
        this.walkable = walkable;
        this.pos = pos;
        this.x = x;
        this.y = y;
    }
}
