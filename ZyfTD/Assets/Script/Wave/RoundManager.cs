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


        //设置波次文本
        GameSetting.instance.roundText.text = (WaveSpawner.currentWaveIndex + 1).ToString();

        WaveSpawner.instance.StartSpawn();

    }

    //回合结束
    public void RoundEnd()
    {
        Debug.Log("回合结束");
        GameManager.instance.gaming = false;

        //补充弹药
        foreach (Transform item in GameSetting.instance.turrets.transform)
        {
            item.gameObject.GetComponent<Turret>().Init();

        }

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

            GameSetting.instance.gameWinUI.SetActive(true);

        }
        else
        {
            WaveSpawner.currentWaveIndex++;

            //生成新炮塔
            GameManager.instance.GenerateTurretInRandomPos(Random.Range(0, GameSetting.instance.turretPrefab.Count));
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
