using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour {

    public int mapSizeX = 5;
    public int mapSizeY = 5;

    public GameObject nodePrefab;

    public Node[,] nodes;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log(GetNeighbourNode(nodes[1, 1]).Count);
        }
    }

    private void Start()
    {
        nodes = new Node[mapSizeX, mapSizeY];
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
                obj.transform.SetParent(transform);
                
            }
        }
    }
    //获取邻近节点
    List<Node> GetNeighbourNode(Node _node)
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

}
