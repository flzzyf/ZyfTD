using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node{

    public bool walkable = true;
    //xy坐标
    public int x;
    public int z;
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
        get { return new Vector3(x, 0, z); }
    }

    //上一节点
    public Node parentNode;

    public Node(int x, int z)
    {
        this.x = x;
        this.z = z;
    }

}
