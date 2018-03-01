using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret_Missile : Turret {

    [Header("其他")]
    public GameObject rocketPrefab;
    GameObject rocket;

    void Start ()
    {
        Init();
	}
	
	void Update ()
    {
        base.Update();

    }

    //回合初始化
    public override void Init()
    {
        base.Init();

        rocket = Instantiate(rocketPrefab, transform.position, transform.rotation, transform);

    }

    public override void Attack()
    {
        rocket.GetComponent<Rocket>().Trigger();

    }
}
