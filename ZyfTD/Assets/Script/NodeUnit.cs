using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeUnit : MonoBehaviour {

    bool walkable = true;

    Renderer renderer;

    Color originalColor;

    private void Start()
    {
        renderer = GetComponentInChildren<Renderer>();
        originalColor = renderer.material.color;
    }

    IEnumerator OnMouseDown()
    {
        //非起点或终点
        if (!GameManager.instance.map.GetNode(transform.position).isStartOrEnd)
            toggleWalkable();

        yield return new WaitForFixedUpdate();
    }

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
        GameManager.instance.map.GetNode(transform.position).walkable = _walkable;
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
}
