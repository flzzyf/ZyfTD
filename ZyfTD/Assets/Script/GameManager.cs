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

    public GameObject draggingTurret;

    public GameObject hoveringNode;

    public int turretIndex;

    GameSetting gameSetting;

    void Start()
    {
        start = new GameObject("Start");
        end = new GameObject("End");

        map = GameSetting.instance.map;

        gameSetting = GetComponent<GameSetting>();
        //随机设置起点和终点
        RandomStartAndEnd();
        //寻路
        UpdatePath();

        GenerateTurret(2, 2, gameSetting.turretPrefab[0]);
        //GenerateTurret(1, 4, gameSetting.turretPrefab[0]);

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GenerateTurretInRandomPos(Random.Range(0, gameSetting.turretPrefab.Count));
        }

        if (Input.GetMouseButtonUp(0))
        {
            //正在拖曳炮塔
            if(draggingTurret != null)
            {
                //高亮了一个节点
                if(hoveringNode != null)
                {
                    //该节点没有炮塔
                    if (hoveringNode.GetComponent<NodeUnit>().turret == null)
                    {
                        draggingTurret.GetComponent<PlaceableObject>().SetNode(hoveringNode);

                        hoveringNode.GetComponent<NodeUnit>().SetTurret(draggingTurret);
                    }
                   
                }
                draggingTurret.GetComponent<PlaceableObject>().ResetPos();

                draggingTurret = null;
            }

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

        SetDescText(map.GetNodeUnit(node.pos), "起始点", "点击此节点以将其设为起点，并开始该回合。");

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

        SetDescText(map.GetNodeUnit(node.pos), "起始点", "点击此节点以将其设为起点，并开始该回合。");

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

    //创建炮塔
    void GenerateTurret(int _x, int _z, GameObject _turretPrefab)
    {
        map.nodeUnits[_x, _z].GetComponent<NodeUnit>().GenerateTurret(_turretPrefab);
    }
    //创建炮塔（直接输入节点
    void GenerateTurret(GameObject _node, GameObject _turretPrefab)
    {
        _node.GetComponent<NodeUnit>().GenerateTurret(_turretPrefab);
    }

    //获取空节点
    List<GameObject> GetIdleNodes()
    {
        List<GameObject> nodes = new List<GameObject>(map.nodeUnitList);

        for (int i = 0; i < nodes.Count; i++)
        {
            if (nodes[i].GetComponent<NodeUnit>().turret != null)
            {
                //已有炮塔，移出list
                nodes.Remove(nodes[i]);
                //计数减1
                i--;
            }

        }

        return nodes;
    }

    //在随机空位置生成一个炮塔
    public void GenerateTurretInRandomPos(int _turretType)
    {
        List<GameObject> idleNodes = GetIdleNodes();
        GenerateTurret(idleNodes[Random.Range(0, idleNodes.Count - 1)], gameSetting.turretPrefab[_turretType]);
    }

    //设置提示文本
    void SetDescText(GameObject _go, string _title, string _desc)
    {
        _go.AddComponent(typeof(DescribableObject));
        DescribableObject describable = _go.GetComponent<DescribableObject>();

        describable.title = _title;
        describable.desc = _desc;
    }
}
