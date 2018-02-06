using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node{

    public bool walkable = true;
    //xy坐标
    public int x;
    public int y;
    //g：和起点距离
    public int g;
    //h：和终点距离
    public int h;

    public int f
    {
        get { return g + h; }
    }

    public Vector3 pos
    {
        get { return new Vector3(x, y, 0); }
    }

    //上一节点
    public Node parentNode;

    public Node(int x, int y)
    {
        this.x = x;
        this.y = y;
    }

}
