using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour {
    //节点和墙节点
    public GameObject Node;
    public GameObject NodeWall;

    public float NodeRadius = 0.5f;
    //墙体所在层
    public LayerMask layer;
    //玩家和目的地
    public Transform player;
    public Transform destination;

    private NodeItem[,] grid;
    //地图尺寸
    public int w = 5;
    public int h = 5;

    //用于描绘区域边界
    private GameObject wallRange, pathRange;
    //路径
    private List<GameObject> pathObj = new List<GameObject>();

    private void Awake()
    {
        grid = new NodeItem[w, h];

        wallRange = new GameObject("WallRange");
        pathRange = new GameObject("PathRange");

        //生成网格
        for (int i = 0; i < w; i++)
        {
            //Debug.Log(i);
            for (int j = 0; j < h; j++)
            {
                Vector3 pos = new Vector3(i * 0.5f, j * 0.5f, -0.25f);
                bool walkable = !Physics.CheckSphere(pos, NodeRadius, layer);
                grid[i, j] = new NodeItem(walkable, pos, i, j);
                //加入不可通行边界
                if (!walkable)
                {
                    Debug.Log("不可通行");
                    GameObject obj = GameObject.Instantiate(NodeWall, pos, Quaternion.identity) as GameObject;
                    obj.transform.SetParent(wallRange.transform);
                }
            }
        }
    }
    //根据坐标取得节点
    public NodeItem GetNode(Vector3 pos)
    {
        int x = Mathf.RoundToInt(pos.x) * 2;
        int y = Mathf.RoundToInt(pos.y) * 2;
        x = Mathf.Clamp(x, 0, w - 1);
        y = Mathf.Clamp(y, 0, h - 1);
        return grid[x, y];
    }
    //获取周围节点
    public List<NodeItem> GetNeighbourhood(NodeItem node)
    {
        List<NodeItem> list = new List<NodeItem>();
        for(int i = -1; i <= 1; i++)
        {
            for(int j = -1; j <= 1; j++)
            {
                if (i == 0 && j == 0)
                    continue;
                int x = node.x + i;
                int y = node.y + j;
                if (x < w && x >= 0 && y < h && y >= 0)
                    list.Add(grid[x, y]);
            }
        }
        return list;
    }
    //绘制路径
    public void updatePath(List<NodeItem> lines)
    {
        int curListSize = pathObj.Count;
        for (int i = 0; i < lines.Count; i++)
        {
            if(i < curListSize)
            {
                pathObj[i].transform.position = lines[i].pos;
                pathObj[i].SetActive(true);
            }
            else
            {
                GameObject obj = GameObject.Instantiate(Node, lines[i].pos, Quaternion.identity) as GameObject;
                obj.transform.SetParent(pathRange.transform);
                pathObj.Add(obj);
            }
        }
        for(int i = lines.Count; i < curListSize; i++)
        {
            pathObj[i].SetActive(false);
        }
    }

}
