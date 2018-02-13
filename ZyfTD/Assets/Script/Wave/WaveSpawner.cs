using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    public Wave[] wave;

    public float timeBetweenWaves = 3f;


    //回合中
    bool spawningWave = false;


    Vector3 startPoint;

    void Update()
    {
        

    }
    //回合开始
    IEnumerator SpawnWave()
    {
        spawningWave = true;

        int waveIndex = RoundManager.instance.currentWaveIndex;
        Debug.Log("回合" + waveIndex + "开始");

        Wave currentWave = wave[waveIndex];

        for (int i = 0; i < currentWave.waveUnits.Length; i++)
            enemiesAlive += currentWave.waveUnits[i].num;

        startPoint = GameManager.instance.start.transform.position;

        for (int i = 0; i < currentWave.waveUnits.Length; i++)
        {
            for (int j = 0; j < currentWave.waveUnits[i].num; j++)
            {
                GameObject unit = Instantiate(currentWave.waveUnits[i].unit, startPoint, Quaternion.identity);

                yield return new WaitForSeconds(currentWave.waveUnits[i].rate);
            }
        }

        RoundManager.instance.RoundWin();
    }

}
