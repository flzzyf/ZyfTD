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


    //回合初始化
    public void Init()
    {
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
            if (item.gameObject.tag == GameSetting.enemyTag)
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

    //被鼠标按下
    IEnumerator OnMouseDown()
    {
        //获取物体在屏幕坐标
        Vector3 ScreenSpace = Camera.main.WorldToScreenPoint(transform.position);
        //获取物体与鼠标的位移
        Vector3 offset = transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, ScreenSpace.z));

        LayerMask formerLayer = gameObject.layer;
        gameObject.layer = GameSetting.instance.ignoreRaycast;

        //while还按着鼠标
        while (Input.GetMouseButton(0))
        {
            //获取鼠标屏幕位置
            Vector3 curScreenSpace = new Vector3(Input.mousePosition.x, Input.mousePosition.y, ScreenSpace.z);
            //获取鼠标三维位置
            Vector3 CurPosition = Camera.main.ScreenToWorldPoint(curScreenSpace) + offset;
            //将物体移动到鼠标三维位置
            transform.position = CurPosition;

            yield return new WaitForFixedUpdate();
        }

        //鼠标起
        gameObject.layer = formerLayer;
    }

}
