using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour {

    #region Singleton
    public static PlayerStats instance;

    private void Awake()
    {
        if (instance != null)
            Destroy(this);
        instance = this;
    }
    #endregion

    public int maxHp = 3;
    int currentHp;

	void Start () {
        currentHp = maxHp;
	}

    public void TakeDamage(int _amount)
    {
        currentHp -= _amount;

        if(currentHp <= 0)
        {
            Death();
        }
    }

    void Death()
    {
        Debug.Log("游戏失败");
        GameManager.instance.gaming = false;
    }
	
	
}
