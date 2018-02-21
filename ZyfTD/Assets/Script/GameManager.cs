using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    #region Singleton
    public static GameManager instance;

    private void Awake()
    {
        if (instance != null)
            Destroy(this);
        instance = this;
    }
    #endregion
    
    //起点和终点
    public GameObject start;
    public GameObject end;
    //游戏中
    [HideInInspector]
    public bool gaming = false;

    Map map;

    void Start()
    {
        start = new GameObject("Start");
        end = new GameObject("End");

        map = GameSetting.instance.map;   
        //随机设置起点和终点
        RandomStartAndEnd();
        //寻路
        UpdatePath();

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //UpdatePath();
        }
    }

    //随机设置起点和终点
    void RandomStartAndEnd()
    {
        List<Node> nodes = GetPlainNode();
        //设置起点
        Node node = nodes[Random.Range(0, nodes.Count)];
        start.transform.position = node.pos;
        SetStartOrEndNode(start, 1, Color.red);

        //设置终点
        nodes = GetPlainNode();
        //不符合条件的节点列表
        List<Node> nodesToRemove = new List<Node>();
        foreach (var item in nodes)
        {
            //如果是起点的相邻节点则去除
            if (map.GetNeighbourNode(item).Contains(node))
            {
                nodesToRemove.Add(item);
            }
        }
        //移除不符合条件的节点
        foreach (var item in nodesToRemove)
        {
            nodes.Remove(item);
        }
        node = nodes[Random.Range(0, nodes.Count)];
        end.transform.position = node.pos;
        SetStartOrEndNode(end, 2, Color.cyan);

    }

    void SetStartOrEndNode(GameObject _go, int _a, Color _color)
    {
        Node node = map.GetNode(_go.transform.position);
        node.isStartOrEnd = _a;
        GameObject nodeUnit = map.GetNodeUnit(_go.transform.position);
        nodeUnit.GetComponentInChildren<Renderer>().material.color = _color;
    }

    //获取非起点或终点的边缘节点列表
    List<Node> GetPlainNode()
    {
        List<Node> nodes = new List<Node>();
        //获得可用节点列表
        for (int i = 0; i < map.mapSizeY; i++)
        {
            for (int j = 0; j < map.mapSizeX; j++)
            {
                //非边缘节点
                if (!(i == 0 || i == 5 || j == 0 || j == 5))
                    continue;
                //非起点或终点
                if (map.nodes[i, j].isStartOrEnd == 0)
                {
                    nodes.Add(map.nodes[i, j]);
                }
            }
        }
        return nodes;
    }
    public void UpdatePath()
    {
        AStar.instance.FindPath(start.transform.position, end.transform.position);

    }
    //重新加载本场景
    public void Restart()
    {
        //Debug.Log("restart");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
