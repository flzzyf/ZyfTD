using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour {
    //移动速度
    public float speed = 3;

    public int maxHp = 1;
    int currentHp;

    public GameObject gfx;

    bool isDead = false;

    private void Start()
    {
        currentHp = maxHp;
    }
    //受到伤害
    public void TakeDamage(int _amount)
    {
        //还没死
        if (!isDead)
        {
            currentHp -= _amount;
            //Debug.Log(currentHp);

            if (currentHp <= 0)
            {
                //死了
                Death();
            }
            else
            {
                //animator.Play("Enemy_Hit");
            }
        }
    }
    //死掉
    void Death()
    {
        if (isDead)
            return;

        isDead = true;

        Destroy(gameObject);

        WaveSpawner.enemiesAlive--;
        Debug.Log("目前存活敌人数：" + WaveSpawner.enemiesAlive);
    }
}
