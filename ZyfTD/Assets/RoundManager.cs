using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundManager : MonoBehaviour {

    #region Singleton
    public static RoundManager instance;

    private void Awake()
    {
        if (instance != null)
            Destroy(this);
        instance = this;
    }
    #endregion

    //还在生成敌人
    bool spawningWave = false;

    void Update()
    {
        //未在游戏中
        if (!GameManager.instance.gaming)
            return;

        //仍在波次中
        if (WaveSpawner.enemiesAlive > 0)
            return;

        RoundWin();
    }

    //回合开始
    public void RoundStart()
    {
        //Debug.Log("回合开始");
        GameManager.instance.gaming = true;

        WaveSpawner.instance.StartSpawn();

    }

    //回合结束
    public void RoundEnd()
    {
        Debug.Log("回合结束");
        GameManager.instance.gaming = false;

    }

    //回合胜利
    public void RoundWin()
    {
        //回合结束
        RoundEnd();
        Debug.Log("回合胜利");
    }


}
