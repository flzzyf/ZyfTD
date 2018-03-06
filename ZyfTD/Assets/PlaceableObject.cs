using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceableObject : MonoBehaviour {

    [HideInInspector]
    public GameObject currentNode;

    //被鼠标按下
    IEnumerator OnMouseDown()
    {
        //获取物体在屏幕坐标
        Vector3 ScreenSpace = Camera.main.WorldToScreenPoint(transform.position);
        //获取物体与鼠标的位移
        Vector3 offset = transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, ScreenSpace.z));

        //修改Layer来不被鼠标高亮
        LayerMask formerLayer = gameObject.layer;
        gameObject.layer = GameSetting.instance.ignoreRaycast;

        //保存为被拖曳物体
        GameManager.instance.draggingTurret = gameObject;

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
        yield return new WaitForFixedUpdate();

        //鼠标起
        //修改回原本Layer
        gameObject.layer = formerLayer;

        //清空被拖曳物体
        //GameManager.instance.draggingTurret = null;

    }

    //节点设置
    public void SetNode(GameObject _node)
    {
        if(currentNode != null)
            currentNode.GetComponent<NodeUnit>().RemoveTurret();

        currentNode = _node;
    }

    //将炮塔移动到父节点处
    public void ResetPos()
    {
        transform.position = currentNode.transform.position;
    }

}
