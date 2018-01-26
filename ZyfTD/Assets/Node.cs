using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node{

    public bool walkable = true;

    public int x;
    public int y;

    public Node(int x, int y)
    {
        this.x = x;
        this.y = y;
    }

}
