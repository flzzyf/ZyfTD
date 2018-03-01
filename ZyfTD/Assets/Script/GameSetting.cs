using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSetting : MonoBehaviour {

    #region Singleton
    public static GameSetting instance;

    private void Awake()
    {
        if (instance != null)
            Destroy(this);
        instance = this;

        Init();
    }
    #endregion

    [HideInInspector]
    public Map map;
    //路径
    public List<Node> path;

    //敌人Tag
    public static string enemyTag = "Enemy";

    //路径长度文本
    public Text pathLengthText;
    //回合文本
    public Text roundText;

    //所有炮塔
    public GameObject turrets;
    //所有敌人
    [HideInInspector]
    public GameObject enemies;

    //信息面板
    public GameObject infoUI;
    public Text infoUI_title;
    public Text infoUI_desc;

    //颜色
    public Color[] color = new Color[5];

    //无视鼠标层
    public LayerMask ignoreRaycast;
    
    //炮塔种类
    public List<GameObject> turretPrefab;

    void Init()
    {
        enemies = new GameObject("Enemies");
        turrets = new GameObject("Turrets");

    }
}
