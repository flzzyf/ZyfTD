using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeUnit : MonoBehaviour {

    bool walkable = true;

    Renderer renderer;

    Color originalColor;

    Animator animator;

    public GameObject turret;

    private void Start()
    {
        renderer = GetComponentInChildren<Renderer>();
        originalColor = renderer.material.color;

        animator = GetComponent<Animator>();
    }
    //被点击
    IEnumerator OnMouseDown()
    {
        //非起点或终点
        if (GameSetting.instance.map.GetNode(transform.position).isStartOrEnd == 0)
            toggleWalkable();
        else
        {
            if (GameManager.instance.gaming)
            {
                //游戏中
                RoundManager.instance.EndTheRound();
            }
            else
            {
                //还没开始
                if (AStar.instance.walkable)
                {
                    //可通行
                    //终点则反转路径
                    if (GameSetting.instance.map.GetNode(transform.position).isStartOrEnd == 2)
                        GameSetting.instance.map.ReversePath();

                    RoundManager.instance.RoundStart();
                }
                else
                {
                    Debug.LogError("无法通行");
                }
                
            }
            
        }

        yield return new WaitForFixedUpdate();
    }
    //切换能否通行 
    void toggleWalkable()
    {
        walkable = !walkable;
        setWalkable(walkable);
        //更新路径
        GameManager.instance.UpdatePath();

    }

    void setWalkable(bool _walkable)
    {
        //设置节点可通行属性
        GameSetting.instance.map.GetNode(transform.position).walkable = _walkable;
        //变色
        if (_walkable)
        {
            //可通行
            renderer.material.color = originalColor;
        }
        else
        {
            //不可通行
            renderer.material.color = Color.black;

        }
    }
    //炮塔占位符
    GameObject placeholder;

    //鼠标进入
    private void OnMouseEnter()
    {
        animator.SetBool("hovered", true);

        GameManager.instance.hoveringNode = gameObject;

        if(turret == null)
        {
            //生成炮塔的放置符
            if (GameManager.instance.draggingTurret != null)
            {
                GenerateTurretPlaceholder(GameManager.instance.draggingTurret);
            }
        }

    }

    private void OnMouseExit()
    {
        animator.SetBool("hovered", false);

        GameManager.instance.hoveringNode = null;

        //清除放置符
        if (placeholder != null)
        {
            Destroy(placeholder);
        }
    }

    //生成炮塔的放置符
    void GenerateTurretPlaceholder(GameObject _turret)
    {
        placeholder = Instantiate(_turret, transform.position, Quaternion.identity);

        placeholder.transform.Translate(0, -0.15f, 0);
        //改颜色
        foreach (var item in placeholder.GetComponentsInChildren<Renderer>())
        {
            item.material.color = Color.gray;
        }


        //禁用炮塔脚本
        placeholder.GetComponent<Turret>().enabled = false;
    }

    //生成炮塔
    public void GenerateTurret(GameObject _turret)
    {
        turret = Instantiate(_turret, transform.position, Quaternion.identity);
        turret.transform.parent = GameSetting.instance.turrets.transform;

        SetTurret(turret);

        //层级重置
        if (_turret.layer != 0)
            turret.layer = 0;
    }
    //设置炮塔
    public void SetTurret(GameObject _turret)
    {
        turret = _turret;

        turret.GetComponent<PlaceableObject>().SetNode(gameObject);

    }

    //移除炮塔
    public void RemoveTurret()
    {
        //turret.GetComponent<PlaceableObject>().RemoveNode();

        turret = null;
    }
}
