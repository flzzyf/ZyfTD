using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour {
    [Header("常规塔属性")]
    public float range = 3;

    public float fireRate = 1f;
    protected float fireCountdown = 0f;

    public float rotSpeed = 300f;

    protected GameObject target;

    public int maxAmmoCount = 1;
    protected int currentAmmoCount;

    private void Start()
    {
        Init();
    }

    //回合初始化
    virtual public void Init()
    {
        currentAmmoCount = maxAmmoCount;
    }

    public void Update ()
    {
        if (currentAmmoCount <= 0)
            return;

        ColdDown();

        if (GetTargets().Count > 0)
        {
            if (target == null)
                target = GetTargets()[0];
            else
            {
                AimTarget(target.transform.position);

                if (canAttack())
                {
                    currentAmmoCount--;
                    fireCountdown = fireRate;

                    Attack();
                }
            }

        }

    }

    void ColdDown()
    {
        //武器冷却
        if (fireCountdown > 0)
        {
            fireCountdown -= Time.deltaTime;
        }
    }

    public virtual void Attack()
    {
        Debug.Log("att1");

        //创建特效
        //GameObject fx = Instantiate(effect_Launch, transform.position, Quaternion.identity);
        //Destroy(fx, 0.5f);

        //_target.GetComponent<Unit>().TakeDamage(1);

        //回合伤害量增加
        //roundDamage++;
    }

    protected List<GameObject> GetTargets()
    {
        List<GameObject> targets = new List<GameObject>();

        Collider[] cols = Physics.OverlapSphere(transform.position, range);
        foreach (var item in cols)
        {
            if (item.gameObject.tag == GameSetting.enemyTag)
            {
                targets.Add(item.gameObject);
            }
        }

        return targets;
    }
    //朝向目标点
    protected void AimTarget(Vector3 _target)
    {
        //方向
        Vector3 direction = _target - transform.position;
        //长度设为1
        direction.Normalize();
        direction.y = 0;

        transform.forward = direction;
    }

    protected bool canAttack()
    {
        return fireCountdown <= 0;
    }


}
