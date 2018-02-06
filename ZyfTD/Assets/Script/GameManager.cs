using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    [HideInInspector]
    public Map map;

    public List<Node> path;

    public GameObject start;
    public GameObject end;
    //游戏中
    [HideInInspector]
    public bool gaming = false;

    private void Start()
    {
        //寻路
        AStar.instance.FindPath(start.transform.position, end.transform.position);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            gaming = true;
        }
            

    }
}
