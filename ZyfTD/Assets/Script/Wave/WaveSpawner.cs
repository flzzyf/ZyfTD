using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    #region Singleton
    public static WaveSpawner instance;

    private void Awake()
    {
        if (instance != null)
            Destroy(this);
        instance = this;
    }
    #endregion

    public Wave[] wave;

    //回合中
    bool spawningWave = false;

    Vector3 startPoint;
    //当前波次
    public int currentWaveIndex = 0;
    //剩余敌人
    public static int enemiesAlive = 0;

    public void StartSpawn()
    {
        //设定起点
        startPoint = GameManager.instance.start.transform.position;

        StartCoroutine(SpawnWave());

    }

    //回合开始
    IEnumerator SpawnWave()
    {
        spawningWave = true;

        int waveIndex = currentWaveIndex;
        Debug.Log("回合" + waveIndex + "开始");

        Wave currentWave = wave[waveIndex];

        for (int i = 0; i < currentWave.waveUnits.Length; i++)
            enemiesAlive += currentWave.waveUnits[i].num;


        for (int i = 0; i < currentWave.waveUnits.Length; i++)
        {
            for (int j = 0; j < currentWave.waveUnits[i].num; j++)
            {
                GameObject unit = Instantiate(currentWave.waveUnits[i].unit, startPoint, Quaternion.identity);

                yield return new WaitForSeconds(currentWave.waveUnits[i].rate);
            }
        }

    }

}
