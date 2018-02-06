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
    }

    void setWalkable(bool _walkable)
    {
        //设置节点可通行属性
        GameManager.instance.map.GetNode(transform.position).walkable = _walkable;
        //变色
        if (_walkable)
        {
            //可通行
            transform.Translate(Vector3.forward * 2);
            GetComponent<Renderer>().material.color = Color.white;
        }
        else
        {
            //不可通行
            transform.Translate(-Vector3.forward * 2);

            GetComponent<Renderer>().material.color = Color.red;
        }
    }
}
