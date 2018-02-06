using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeUnit : MonoBehaviour {

    bool walkable = true;

    IEnumerator OnMouseDown()
    {
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
        Debug.Log("qwe");
        //设置节点可通行属性
        GameManager.instance.map.GetNode(transform.position).walkable = _walkable;
        //变色
        if (_walkable)
        {
            //可通行
            GetComponent<Renderer>().material.color = Color.white;
            GetComponent<Renderer>().sortingOrder = 2;
        }
        else
        {
            //不可通行
            GetComponent<Renderer>().material.color = Color.red;
            GetComponent<Renderer>().sortingOrder = 4;

        }
    }
}
