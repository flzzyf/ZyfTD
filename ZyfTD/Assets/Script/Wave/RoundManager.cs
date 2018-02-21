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

    public GameObject turretPrefab;
    public GameObject newTurret;

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

        //补充弹药
        foreach (Transform item in GameSetting.instance.turrets.transform)
        {
            item.gameObject.GetComponent<Turret>().Init();

        }
        //设置波次文本
        GameSetting.instance.roundText.text = "回合:" + (WaveSpawner.currentWaveIndex + 1);

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

        //完成所有波次
        if(WaveSpawner.currentWaveIndex + 1 == WaveSpawner.instance.wave.Length)
        {
            Debug.Log("已经完成所有波次！");

        }
        else
        {
            WaveSpawner.currentWaveIndex++;

            Instantiate(turretPrefab, newTurret.transform.position, Quaternion.identity, GameSetting.instance.turrets.transform);
        }

        
    }

    public void EndTheRound()
    {
        RoundEnd();

        WaveSpawner.instance.StopAllCoroutines();

        WaveSpawner.enemiesAlive = 0;

        foreach (Transform item in GameSetting.instance.enemies.transform)
        {
            Destroy(item.gameObject);
        }
    }


}
