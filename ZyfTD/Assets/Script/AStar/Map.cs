using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour {

    public int mapSizeX = 5;
    public int mapSizeY = 5;

    public GameObject nodePrefab;

    //节点
    public Node[,] nodes;
    //节点单位
    public GameObject[,] nodeUnits;
    //节点文件夹
    GameObject nodeParent;

    public Color[] pathColor;

    //在搜索开始前执行
    private void Awake()
    {
        nodes = new Node[mapSizeX, mapSizeY];
        nodeUnits = new GameObject[mapSizeX, mapSizeY];
        nodeParent = new GameObject("nodes");
        //根据地图尺寸生成节点
        for (int z = 0; z < mapSizeY; z++)
        {
            for (int x = 0; x < mapSizeX; x++)
            {
                Vector3 pos = new Vector3(x, 0, z);
                //生成节点
                Node node = new Node(x, z);
                nodes[x, z] = node;
                //生成墙
                GameObject obj = Instantiate(nodePrefab, pos, Quaternion.identity);
                obj.transform.SetParent(nodeParent.transform);
                nodeUnits[x, z] = obj;
                
            }
        }
    }
    //获取邻近节点
    public List<Node> GetNeighbourNode(Node _node)
    {
        List<Node> list = new List<Node>();
        //搜索周围是否有可通行节点
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
    //根据所给Vector3获取相应nodeUnit
    public GameObject GetNodeUnit(Vector3 _pos)
    {
        int x = Mathf.Clamp((int)_pos.x, 0, mapSizeX - 1);
        int z = Mathf.Clamp((int)_pos.z, 0, mapSizeY - 1);

        return nodeUnits[x, z];
    }
    #region 路径显示
    public LineRenderer line;
    //显示路径
    public void PathShow(List<Node> lines)
    {
        //显示路径物体
        if(!line.enabled)
            line.enabled = true;

        line.positionCount = lines.Count + 1;
        //设置路径点
        line.SetPosition(0, GameManager.instance.start.transform.position);
        for (int i = 0; i < lines.Count; i++)
        {
            line.SetPosition(i + 1, lines[i].pos);
        }
        //设置路径颜色按长度变化
        if(lines.Count < 13)
        {
            line.startColor = pathColor[0];
            line.endColor = pathColor[0];
        }
        else if (lines.Count == 13)
        {
            line.startColor = pathColor[1];
            line.endColor = pathColor[1];
        }
        else if (lines.Count == 14)
        {
            line.startColor = pathColor[2];
            line.endColor = pathColor[2];
        }
        else if (lines.Count == 15)
        {
            line.startColor = pathColor[3];
            line.endColor = pathColor[3];
        }
        else if (lines.Count == 16)
        {
            line.startColor = pathColor[4];
            line.endColor = pathColor[4];
        }



    }

    //隐藏路径
    public void PathHide()
    {
        line.enabled = false;
    }
    #endregion

}
