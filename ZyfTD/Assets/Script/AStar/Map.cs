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
        for (int z = 0; z < mapSizeY; z++)
        {
            for (int x = 0; x < mapSizeX; x++)
            {
                Vector3 pos = new Vector3(x, 0, z);
                //生成节点
                nodes[x, z] = new Node(x, z);
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
        //搜索周围是否有可通行节点
        /*周围九格
        for (int i = -1; i <= 1; i++)
        {
            for (int j = -1; j <= 1; j++)
            {
                if (i == 0 && j == 0)
                    continue;
                int x = _node.x + i;
                int z = _node.z + j;
                if (x < mapSizeX && x >= 0 && z < mapSizeY && z >= 0)
                    list.Add(nodes[x, z]);
            }
        }
        */
        //不能斜着走（只判断上下左右四格）
        for (int i = -1; i <= 1; i++)
        {
            for (int j = -1; j <= 1; j++)
            {
                if (Mathf.Abs(i + j) != 1)
                    continue;
                int x = _node.x + i;
                int z = _node.z + j;
                if (x < mapSizeX && x >= 0 && z < mapSizeY && z >= 0)
                    list.Add(nodes[x, z]);
            }
        }
        return list;
    }
    //根据所给Vector3获取相应node
    public Node GetNode(Vector3 _pos)
    {
        int x = Mathf.Clamp((int)_pos.x, 0, mapSizeX - 1);
        int z = Mathf.Clamp((int)_pos.z, 0, mapSizeY - 1);

        return nodes[x, z];
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
                pathObj[i].transform.position = lines[i].pos;
                pathObj[i].SetActive(true);
            }
            else
            {
                GameObject obj = GameObject.Instantiate(pathPrefab, lines[i].pos, Quaternion.identity, pathParent.transform) as GameObject;
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
