using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindPath : MonoBehaviour {

    private Grid grid;

    void Start()
    {
        grid = GetComponent<Grid>();
        
    }

    void Update()
    {
        FindingPath(grid.player.position, grid.destination.position);
    }

    void FindingPath(Vector3 start, Vector3 end)
    {
        NodeItem startNode = grid.GetNode(start);
        NodeItem endNode = grid.GetNode(end);

        List<NodeItem> openSet = new List<NodeItem>();
        HashSet<NodeItem> closeSet = new HashSet<NodeItem>();
        openSet.Add(startNode);

        while(openSet.Count > 0)
        {
            NodeItem curNode = openSet[0];
            for(int i = 0; i < openSet.Count; i++)
            {
                if(openSet[i].f <= curNode.f && openSet[i].h < curNode.h)
                {
                    curNode = openSet[i];
                }
            }
            //把当前节点移出开集，加入闭集
            openSet.Remove(curNode);
            closeSet.Add(curNode);

            //当前节点就是终点，结束
            if(curNode == endNode)
            {
                GeneratePath(startNode, endNode);
                return;
            }

            //判断周围节点
            foreach(var item in grid.GetNeighbourhood(curNode))
            {
                //忽视不可通行或已在闭集中的节点
                if (!item.walkable || closeSet.Contains(item))
                    continue;

                int newCost = curNode.g + GetNodeDistance(curNode, item);
                if(newCost < item.g || !openSet.Contains(item))
                {
                    //更新
                    item.g = newCost;
                    item.h = GetNodeDistance(item, endNode);
                    item.parent = curNode;
                    if (!openSet.Contains(item))
                    {
                        openSet.Add(item);
                    }
                }

            }
        }
        GeneratePath(startNode, null);

    }

    //生成路径
    void GeneratePath(NodeItem start,NodeItem end)
    {
        //Debug.Log("生成路径");
        List<NodeItem> path = new List<NodeItem>();
        if(end != null)
        {
            NodeItem temp = end;
            while(temp != start)
            {
                path.Add(temp);
                temp = temp.parent;
            }
            path.Reverse();
        }

        grid.updatePath(path);
    }

    //节点路径距离估计
    int GetNodeDistance(NodeItem a, NodeItem b)
    {
        //先斜着走然后直走
        int X = Mathf.Abs(a.x - b.x);
        int Y = Mathf.Abs(a.y - b.y);

        if(X > Y)
            return 14 * Y + 10 * (X - Y);
        else
            return 14 * X + 10 * (Y - X);
    }


}
