using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AStar : MonoBehaviour {

    Map map;

    #region Singleton
    [HideInInspector]
    public static AStar instance;

    private void Awake()
    {
        if (instance != null)
            Destroy(this);
        instance = this;
    }
    #endregion

    private void Start()
    {
        map = GetComponent<Map>();
        GameManager.instance.map = map;

    }

    //寻找同路
    public void FindPath(Vector3 _startPos, Vector3 _endPos)
    {
        Node startNode = map.GetNode(_startPos);
        Node endNode = map.GetNode(_endPos);
        //开集和闭集
        List<Node> openSet = new List<Node>();
        List<Node> closeSet = new List<Node>();
        //将开始节点介入开集
        openSet.Add(startNode);
        //开始搜索
        while (openSet.Count > 0)
        {
            //当前所在节点
            Node curNode = openSet[0];
            //从开集中选出f和h最小的
            for (int i = 0; i < openSet.Count; i++)
            {
                if(openSet[i].f <= curNode.f && openSet[i].h <= curNode.h)
                {
                    curNode = openSet[i];
                }
            }
            //把当前所在节点加入闭集
            openSet.Remove(curNode);
            closeSet.Add(curNode);
            //如果就是终点
            if(curNode == endNode)
            {
                Debug.Log("到达终点");
                GeneratePath(startNode, endNode);

                return;
            }
            //判断周围节点
            foreach (var item in map.GetNeighbourNode(curNode))
            {
                //如果不可通行或在闭集中，则跳过
                if(!item.walkable || closeSet.Contains(item))
                {
                    continue;
                }
                //判断新节点耗费
                int newH = GetNodeDistance(curNode, item);
                int newCost = curNode.g + newH;
                //耗费更低或不在开集中
                if(newCost < item.g || !openSet.Contains(item))
                {
                    //
                    item.g = newCost;
                    item.h = newH;
                    item.parentNode = curNode;
                    if (!openSet.Contains(item))
                    {
                        openSet.Add(item);
                    }
                }
            }
        }
        Debug.Log("无法通行");
        map.PathHide();

    }
    //生成路径
    void GeneratePath(Node _startNode, Node _lastNode)
    {
        //Debug.Log("生成路径");
        Node curNode = _lastNode;

        List<Node> path = new List<Node>();

        while(curNode != _startNode)
        {
            path.Add(curNode);

            curNode = curNode.parentNode;
        }
        //反转路径然后生成显示路径
        path.Reverse();

        GameManager.instance.path = path;
        //map.updatePath(path);
        map.PathShow(path);
    }

    //节点间路径距离估计算法
    int GetNodeDistance(Node a, Node b)
    {
        //先斜着走然后直走
        int X = Mathf.Abs(a.x - b.x);
        int Z = Mathf.Abs(a.z - b.z);

        if (X > Z)
            return 14 * Z + 10 * (X - Z);
        else
            return 14 * X + 10 * (Z - X);
    }
}
