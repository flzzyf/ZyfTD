using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    public Wave[] wave;

    public float timeBetweenWaves = 3f;

    int currentWaveIndex = 0;

    float spawnCountDown = 0;

    //回合中
    bool spawningWave = false;

    public static int enemiesAlive = 0;

    Vector3 startPoint;

    void Update()
    {
        if (!GameManager.instance.gaming)
        {
            return;
        }

        //仍在波次中
        if (enemiesAlive > 0)
            return;

        //完成全部波数
        if (currentWaveIndex == wave.Length)
        {
            Debug.Log("胜利");
            //胜利
            this.enabled = false;
        }

        if (spawnCountDown <= 0)
        {
            spawnCountDown = timeBetweenWaves;
            StartCoroutine(SpawnWave());

        }
        else
        {
            spawnCountDown -= Time.deltaTime;
        }
    }
    //回合开始
    IEnumerator SpawnWave()
    {
        spawningWave = true;

        Debug.Log("回合" + currentWaveIndex + "开始");

        Wave currentWave = wave[currentWaveIndex];

        for (int i = 0; i < currentWave.waveUnits.Length; i++)
            enemiesAlive += currentWave.waveUnits[i].num;

        startPoint = GameManager.instance.start.transform.position;

        for (int i = 0; i < currentWave.waveUnits.Length; i++)
        {
            for (int j = 0; j < currentWave.waveUnits[i].num; j++)
            {
                GameObject unit = Instantiate(currentWave.waveUnits[i].unit, startPoint, Quaternion.identity);
                //unit.GetComponent<Unit>().maxHp = currentWave.waveUnits[i].hp;

                yield return new WaitForSeconds(currentWave.waveUnits[i].rate);
            }
        }

        TurnEnd();
    }

    //回合结束
    void TurnEnd()
    {
        currentWaveIndex++;

        spawningWave = false;

        //GameManager.instance.TurretMVP();

        //随机送个塔
        //GameManager.instance.RandomBuildTurret();

    }

}
