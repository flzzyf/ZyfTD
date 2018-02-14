using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour {
    [Header("攻击范围")]
    public float range = 3;

    public float fireRate = 100f;
    private float fireCountdown = 0f;

    public float rotSpeed = 300f;

    GameObject target;

    public GameObject rocketPrefab;

    public int maxXmmoCount = 1;
    int currentAmmoCount;

    void Start () {
        currentAmmoCount = maxXmmoCount;
	}
	
	void Update () {
        if (currentAmmoCount <= 0)
            return;

        if(GetTargets().Count > 0)
        {
            if(target == null)
                target = GetTargets()[0];
            else
            {
                AimTarget(target.transform.position);

                if (canAttack())
                {
                    currentAmmoCount--;
                    Attack(target);
                }
            }

        }
    }

    public void Attack(GameObject _target)
    {
        //创建特效
        //GameObject fx = Instantiate(effect_Launch, transform.position, Quaternion.identity);
        //Destroy(fx, 0.5f);

        //_target.GetComponent<Unit>().TakeDamage(1);
        GameObject rocket = Instantiate(rocketPrefab, transform.position, transform.rotation);

        //回合伤害量增加
        //roundDamage++;
    }

    List<GameObject> GetTargets()
    {
        List<GameObject> targets = new List<GameObject>();

        Collider[] cols = Physics.OverlapSphere(transform.position, range);
        foreach (var item in cols)
        {
            if (item.gameObject.tag == GameManager.enemyTag)
            {
                targets.Add(item.gameObject);
            }
        }

        return targets;
    }
    //朝向目标点
    void AimTarget(Vector3 _target)
    {
        //方向
        Vector3 direction = _target - transform.position;
        //长度设为1
        direction.Normalize();
        direction.y = 0;

        transform.forward = direction;
    }

    bool canAttack()
    {
        return fireCountdown <= 0;
    }
}
