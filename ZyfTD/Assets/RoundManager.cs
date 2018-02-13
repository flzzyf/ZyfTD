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

    public Wave[] wave;
    //当前波次
    public int currentWaveIndex = 0;
    //剩余敌人
    public static int enemiesAlive = 0;
    //还在生成敌人
    bool spawningWave = false;

    void Update()
    {
        //未在游戏中
        if (!GameManager.instance.gaming)
            return;

        //仍在波次中
        if (enemiesAlive > 0)
            return;

        RoundWin();
    }

    //回合开始
    public void RoundStart()
    {
        GameManager.instance.gaming = true;

        StartCoroutine(SpawnWave());

    }

    //回合结束
    public void RoundEnd()
    {
        GameManager.instance.gaming = false;

    }

    //回合胜利
    public void RoundWin()
    {
        //回合结束
        RoundEnd();
        Debug.Log("回合胜利");
    }

    //生成敌人
    IEnumerator SpawnWave()
    {
        spawningWave = true;

        int waveIndex = RoundManager.instance.currentWaveIndex;
        Debug.Log("回合" + waveIndex + "开始");

        Wave currentWave = wave[waveIndex];

        for (int i = 0; i < currentWave.waveUnits.Length; i++)
            enemiesAlive += currentWave.waveUnits[i].num;

        Vector3 startPoint = GameManager.instance.start.transform.position;

        for (int i = 0; i < currentWave.waveUnits.Length; i++)
        {
            for (int j = 0; j < currentWave.waveUnits[i].num; j++)
            {
                Instantiate(currentWave.waveUnits[i].unit, startPoint, Quaternion.identity);

                yield return new WaitForSeconds(currentWave.waveUnits[i].rate);
            }
        }

        spawningWave = false;

    }

}
