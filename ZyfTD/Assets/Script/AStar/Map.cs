using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour {

    public int mapSizeX = 5;
    public int mapSizeY = 5;

    public GameObject nodePrefab;

    public Node[,] nodes;
    //节点文件夹
    GameObject nodeParent;

    //在搜索开始前执行
    private void Awake()
    {
        nodes = new Node[mapSizeX, mapSizeY];
        nodeParent = new GameObject("nodes");
        pathParent = new GameObject("path");
        //根据地图尺寸生成节点
        for (int y = 0; y < mapSizeY; y++)
        {
            for (int x = 0; x < mapSizeX; x++)
            {
                Vector3 pos = new Vector3(x, y, 0);
                //生成节点
                nodes[x, y] = new Node(x, y);
                //生成墙
                GameObject obj = Instantiate(nodePrefab, pos, Quaternion.identity);
                obj.transform.SetParent(nodeParent.transform);
                
            }
        }
    }
    //获取邻近节点
    public List<Node> GetNeighbourNode(Node _node)
    {
        List<Node> list = new List<Node>();
        //搜索周围九个格子是否有节点
        for (int i = -1; i <= 1; i++)
        {
            for (int j = -1; j <= 1; j++)
            {
                if (i == 0 && j == 0)
                    continue;
                int x = _node.x + i;
                int y = _node.y + j;
                if (x < mapSizeX && x >= 0 && y < mapSizeY && y >= 0)
                    list.Add(nodes[x, y]);
            }
        }
        return list;
    }
    //根据所给Vector3获取相应node
    public Node GetNode(Vector3 _pos)
    {
        int x = Mathf.Clamp((int)_pos.x, 0, mapSizeX - 1);
        int y = Mathf.Clamp((int)_pos.y, 0, mapSizeY - 1);

        return nodes[x, y];
    }
#region 路径显示
    //路径节点预设
    public GameObject pathPrefab;
    //路径节点文件夹
    GameObject pathParent;
    //路径物体
    private List<GameObject> pathObj = new List<GameObject>();

    //绘制路径
    public void updatePath(List<Node> lines)
    {
        int curListSize = pathObj.Count;
        for (int i = 0; i < lines.Count; i++)
        {
            if (i < curListSize)
            {
                pathObj[i].transform.position = lines[i].pos + -Vector3.forward;
                pathObj[i].SetActive(true);
            }
            else
            {
                GameObject obj = GameObject.Instantiate(pathPrefab, lines[i].pos + -Vector3.forward, Quaternion.identity, pathParent.transform) as GameObject;
                pathObj.Add(obj);
            }
        }
        for (int i = lines.Count; i < curListSize; i++)
        {
            pathObj[i].SetActive(false);
        }
    }
    //隐藏路径
    public void clearPath()
    {
        for (int i = 0; i < pathObj.Count; i++)
        {
            pathObj[i].SetActive(false);
        }
    }
#endregion
}
