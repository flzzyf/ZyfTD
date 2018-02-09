using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    //敌人Tag
    public static string enemyTag = "Enemy";


    private void Start()
    {
        //寻路
        UpdatePath();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            UpdatePath();
            gaming = true;
        }
            
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
